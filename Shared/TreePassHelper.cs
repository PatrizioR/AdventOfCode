using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Shared
{
    public static class TreePassHelper
    {
        public static int CountTrees(string input, int jumpRight = 3, int jumpDown = 1)
        {
            Console.WriteLine($"Start with {jumpRight}, {jumpDown}");
            var map = GetLines(input);
            return Trees(map, 0, 0, 0, jumpRight, jumpDown);
        }

        private static List<string> GetLines(string input)
        {
            return input.Split("\n").Select(item => item.Trim()).ToList();
        }

        private static int Trees(List<string> map, int line, int pos, int currentCount, int jumpRight, int jumpDown)
        {
            if (line < map.Count() - jumpDown)
            {
                var nextLine = map[line + jumpDown];
                pos = (pos + jumpRight) % nextLine.Length;
                if (nextLine[pos] == '#')
                {
                    currentCount++;
                }
                return Trees(map, line + jumpDown, pos, currentCount, jumpRight,jumpDown );
            }
            else
            {
                return currentCount;
            }
        }
    }
}
