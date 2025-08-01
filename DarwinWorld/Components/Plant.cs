using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinWorld.Components
{
    public class Plant
    {
        public int Energy { get; }
        public Point Position { get; }

        public Plant(Point position, int energy) 
        {
            this.Position = position;
            this.Energy = energy;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Plant other)
            {
                return Position.Equals(other.Position) && Energy == other.Energy;
            }
            
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Energy, Position);
        }

        public static implicit operator List<object>(Plant v)
        {
            throw new NotImplementedException();
        }
    }
}
