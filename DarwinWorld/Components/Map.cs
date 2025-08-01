using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinWorld.Components
{
    public class Map
    {
        public Dictionary<Point, List<Animal>> Animals {  get; set; }
        public Dictionary<Point, Plant> Plants { get; set; }
        private Random _random;
        public int Width { get; set; }
        public int Height { get; set; }
        public int AnimalStartEnergy { get; set; }
        public int PlantEnergy { get; set; }
        public int AnimalGenomLen {  get; set; }
        public int ReproductionCost { get; set; }

        private int _animalCount;

        public Map(int width, int height, int animalGenomLen, int animalStartEnergy, int plantEnergy, int reproductionCost, int animalCount)
        {
            this.Width = width;
            this.Height = height;
            this.Animals = new Dictionary<Point, List<Animal>>();
            this.Plants = new Dictionary<Point, Plant>();
            this.AnimalGenomLen = animalGenomLen;
            this.AnimalStartEnergy = animalStartEnergy;
            this.PlantEnergy = plantEnergy;
            this._random = new Random();
            this.ReproductionCost = reproductionCost;
            this._animalCount = animalCount;
        }

        public void AddAnimal(Animal animal)
        {
            if (animal.Position.X < 0 || animal.Position.X >= Width || animal.Position.Y < 0 || animal.Position.Y >= Height)
            {
                throw new ArgumentException("Position out of range");
            }

            if (!Animals.ContainsKey(animal.Position))
            {
                Animals.Add(animal.Position, new List<Animal>());
            }

            Animals[animal.Position].Add(animal);
        }

        public void RemoveAnimal(Animal animal)
        {
            Point oldPos = animal.Position;
            Animals[animal.Position].Remove(animal);

            if (Animals[animal.Position].Count == 0)
            {
                Animals.Remove(oldPos);
            }
        }

        public void AddPlant(Plant plant)
        {
            Plants.Add(plant.Position,plant);
        }

        public void RemovePlant(Point point)
        {
            if (Plants.ContainsKey(point))
            {
                Plants.Remove(point);
            }
        }

        public void EatPlant(Animal animal)
        {
            if (Plants.ContainsKey(animal.Position))
            {
                animal.IncreaseEnergy(Plants[animal.Position].Energy);
                Plants.Remove(animal.Position);
            }
        }

        public void Reproduct(Animal animal)
        {
            if (animal.Energy < ReproductionCost)
            {
                return;
            }

            List<Animal> animalsList = new List<Animal>(Animals[animal.Position]);
            Animal a2 = null;

            for(int i=0;i < animalsList.Count;i++)
            {
                if (animalsList[i].Equals(animal))
                {
                    continue;
                }

                if (animalsList[i].Energy >= ReproductionCost)
                {
                    a2 = animalsList[i];
                    break;
                }
            }

            Animal child = null;
            if (a2 != null)
            {
                child = animal.Reproduct(a2,AnimalStartEnergy,_animalCount);
                child.Genom = GenerateGenom(animal, a2);
                animal.DecreaseEnergy(ReproductionCost);
                a2.DecreaseEnergy(ReproductionCost);
            }

            if (child != null)
            {
                Animals[animal.Position].Add(child);
                _animalCount++;
            }
        }

        public bool MoveAnimal(Animal animal)
        {
            RemoveAnimal(animal);
            animal.Move(animal.Genom[animal.Day%animal.Genom.Count]);

            if (animal.Position.X < 0 || animal.Position.Y < 0 || animal.Position.X >= Width || animal.Position.Y >= Height)
            {
                animal.Wrap(this);
            }

            if (!animal.IsDead())
            {
                AddAnimal(animal);
            }
            else
            {
                return false;
            }

            EatPlant(animal);
            animal.Day++;

            return true;
        }

        public void NewPlant()
        {
            int x = _random.Next(0, Width);
            int y = _random.Next(0, Height);

            while (Plants.ContainsKey(new Point(x,y)))
            {
                x = _random.Next(0, Width);
                y = _random.Next(0, Height);
            }

            AddPlant(new Plant(new Point(x, y), PlantEnergy));
        }

        private List<MoveDirection> GenerateGenom(Animal a1, Animal a2)
        {
            int whoFirst = _random.Next(0, 2);
            List<MoveDirection> genom = new List<MoveDirection>();

            int len = Math.Min(a1.Genom.Count,a2.Genom.Count);
            int half = len / 2;

            if (len % 2 != 0)
            {
                genom.Add((MoveDirection)_random.Next(0, 4));
            }

            switch (whoFirst)
            {
                case 0:
                    genom.AddRange(a1.Genom.GetRange(0, half));
                    genom.AddRange(a2.Genom.GetRange(half, len-half));
                    break;
                case 1:
                    genom.AddRange(a2.Genom.GetRange(0, half));
                    genom.AddRange(a1.Genom.GetRange(half, len-half));
                    break;

            }

            return genom;
        }
    }
}
