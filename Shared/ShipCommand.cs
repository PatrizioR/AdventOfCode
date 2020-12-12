using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Shared
{
    public class ShipCommand
    {
        public ShipCommand(string input)
        {
            Command = input[0];
            Value = int.Parse(input.Substring(1));
        }

        public char Command { get; set; }
        public int Value { get; set; }

        public override string ToString()
        {
            return $"{Command}{Value}";
        }
    }
}