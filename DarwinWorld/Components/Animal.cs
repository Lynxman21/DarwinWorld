using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinWorld.Components
{
    public class Animal
    {
        public int Id { get; }
        public Point Position { get; set; }
        public int Energy { get; set; }
        public List<MoveDirection> Genom { get; set; }


        public int Day { get; set; }

        public Animal(Point p, int e, int id)
        {
            this.Position = p;
            this.Energy = e;
            this.Genom = new List<MoveDirection>();
            this.Day = 0;
            this.Id = id;
        }

        public void Move(MoveDirection direction)
        {
            Point newPoint = Position.MoveForward(direction);
            this.Position = newPoint;
            this.DecreaseEnergy(1);
        }

        public void IncreaseEnergy(int e)
        {
            this.Energy += e;
        }

        public void DecreaseEnergy(int e)
        {
            this.Energy -= e;
        }

        public bool IsDead()
        {
            return !(Energy > 0);
        }
        
        //Map will decide how many energy will lose and will set energy for child
        public Animal Reproduct(Animal animal, int energy, int animalCount)
        {
            if (!this.Position.Equals(animal.Position))
            {
                throw new ArgumentException("Animals are not at the same point");
            }

            return new Animal(this.Position,energy, animalCount + 1);
        }

        public void Wrap(Map map)
        {
            if (this.Position.X < 0)
            {
                this.Position.X = map.Width - 1;
            }

            else if (this.Position.X >= map.Width)
            {
                this.Position.X = 0;
            }

            if (this.Position.Y < 0)
            {
                this.Position.Y = map.Height - 1;
            }

            else if (this.Position.Y >= map.Height)
            {
                this.Position.Y = 0;
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is Animal other && Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"({Position}), {Energy}, {Genom}";
        }
    }
}
