using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Shared
{
    public class Ship
    {
        public Ship(Direction startDirection)
        {
            Direction = startDirection;
        }

        public Direction Direction { get; set; }
        public Point Position { get; set; } = new Point(0, 0);

        public int PosSum => Math.Abs(Position.X) + Math.Abs(Position.Y);

        public void Left(int degree = 90)
        {
            while (degree >= 90)
            {
                if (Direction == Direction.West)
                {
                    Direction = Direction.South;
                }
                else
                {
                    Direction--;
                }
                degree -= 90;
            }
        }

        public void Right(int degree = 90)
        {
            while (degree >= 90)
            {
                Left();
                Left();
                Left();
                degree -= 90;
            }
        }

        public void Forward(int steps)
        {
            ForwardInternal(steps, Direction);
        }

        public void ForwardInternal(int steps, Direction direction)
        {
            if (direction == Direction.East)
            {
                Position = new Point(Position.X + steps, Position.Y);
            }
            else if (direction == Direction.West)
            {
                Position = new Point(Position.X - steps, Position.Y);
            }
            else if (direction == Direction.North)
            {
                Position = new Point(Position.X, Position.Y - steps);
            }
            else if (direction == Direction.South)
            {
                Position = new Point(Position.X, Position.Y + steps);
            }
        }

        public void East(int steps)
        {
            ForwardInternal(steps, Direction.East);
        }

        public void North(int steps)
        {
            ForwardInternal(steps, Direction.North);
        }

        public void West(int steps)
        {
            ForwardInternal(steps, Direction.West);
        }

        public void South(int steps)
        {
            ForwardInternal(steps, Direction.South);
        }
    }

    public enum Direction
    {
        West,
        North,
        East,
        South
    }
}