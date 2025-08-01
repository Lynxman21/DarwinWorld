using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinWorld.Components
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public Point MoveForward(MoveDirection dir)
        {
            switch(dir)
            {
                case MoveDirection.North: 
                    return new Point(X, Y + 1);
                case MoveDirection.East: 
                    return new Point(X + 1, Y);
                case MoveDirection.South: 
                    return new Point(X, Y - 1);
                case MoveDirection.West: 
                    return new Point(X - 1, Y);
                default:
                    throw new ArgumentException("Wrong direction");
            }
        }

        public Point MoveBackward(MoveDirection dir)
        {
            switch (dir)
            {
                case MoveDirection.North:
                    return new Point(X, Y - 1);
                case MoveDirection.East:
                    return new Point(X - 1, Y);
                case MoveDirection.South:
                    return new Point(X, Y + 1);
                case MoveDirection.West:
                    return new Point(X + 1, Y);
                default:
                    throw new ArgumentException("Wrong direction");
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is Point p && this.X == p.X && this.Y == p.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X,Y);
        }

        public override string ToString()
        {
            return $"{X},{Y}";
        }

        public Point Clone()
        {
            return new Point(X,Y);
        }
    }
}
