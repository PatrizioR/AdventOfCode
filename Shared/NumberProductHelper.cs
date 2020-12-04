using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Shared
{
    public static class NumberProductHelper
    {
        public static Tuple<int, int, int> FindNumbers(string input, int num = 2)
        {
            var numbers = input.Split("\n").Where((item) => !string.IsNullOrEmpty(item)).Select((item) => int.Parse(item.Trim()));
            foreach (var number in numbers)
            {
                foreach (var innerNumber in numbers.Where((item) => item != number))
                {
                    foreach (var innerInnerNumber in numbers.Where((item) => item != number && item != innerNumber))
                    {
                        if (number + innerNumber + innerInnerNumber == 2020)
                        {
                            return new Tuple<int, int, int>(number, innerNumber, innerInnerNumber);
                        }
                    }
                }
            }
            return new Tuple<int, int, int>(-1, -1, -1);
        }
    }
}
