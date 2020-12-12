using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Shared
{
    public static class ShipMovement
    {
        public static int Move(string input, string direction)
        {
            Enum.TryParse<Direction>(direction, out var curDirection);

            var shipCommands = input.TrimmedNotEmptyMap().Select(item => new ShipCommand(item));
            var ship = new Ship(curDirection);

            foreach (var command in shipCommands)
            {
                switch (command.Command)
                {
                    case 'N':
                        ship.North(command.Value);
                        break;

                    case 'S':
                        ship.South(command.Value);
                        break;

                    case 'W':
                        ship.West(command.Value);
                        break;

                    case 'E':
                        ship.East(command.Value);
                        break;

                    case 'F':
                        ship.Forward(command.Value);
                        break;

                    case 'L':
                        ship.Left(command.Value);
                        break;

                    case 'R':
                        ship.Right(command.Value);
                        break;
                }
            }

            return ship.PosSum;
        }
    }
}