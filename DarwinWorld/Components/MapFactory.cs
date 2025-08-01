using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DarwinWorld.Components
{
    public class MapFactory
    {
        private Random _random;

        public MapFactory() 
        { 
            _random = new Random();
        }

        public Map createMap(int width, int height, int animalGenomLen, int animalStartEnergy, int plantEnergy, int reproductionCost,int animalsCount)
        {
            return new Map(width, height, animalGenomLen, animalStartEnergy, plantEnergy, reproductionCost,animalsCount);
        }

        public void InitMapeEnviroment(Map map, int animals, int plants)
        {
            Random random = new Random();

            for (int i=0;i<animals;i++)
            {
                int x = random.Next(0,map.Width);
                int y = random.Next(0,map.Height);

                Animal animal = new Animal(new Point(x, y), map.AnimalStartEnergy,i+1);
                animal.Genom = GenerateGenom(map);
                AddAnimal(map,animal);
            }

            for (int i = 0; i < plants; i++)
            {
                int x = random.Next(0, map.Width);
                int y = random.Next(0, map.Height);
                AddPlant(map, new Plant(new Point(x, y), map.AnimalStartEnergy));
            }
        }

        private List<MoveDirection> GenerateGenom(Map map)
        {
            Random random = new Random();
            List<MoveDirection> genom = new List<MoveDirection>();

            for (int i=0;i<map.AnimalGenomLen;i++)
            {
                genom.Add((MoveDirection) random.Next(0,4));
            }

            return genom;
        }

        private void AddAnimal(Map map, Animal animal)
        {
            map.AddAnimal(animal);
        }

        private void AddPlant(Map map, Plant plant)
        {
            map.AddPlant(plant);
        }
    }
}
