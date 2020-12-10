using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Shared
{
    public static class XMASHelper
    {
        public static long FirstNotSumNumber(string input, int lastEntries)
        {
            var numberList = MapToNumber(Map(input));

            for(int i = lastEntries; i < numberList.Count(); i++)
            {
                if(!IsSumOf(numberList[i], numberList.Skip(i - lastEntries).Take(lastEntries).ToList()))
                {
                    return numberList[i];
                }
            }

            return -1;
        }

        public static long FindWeakness(string input, int lastEntries)
        {
            var invalidNumber = FirstNotSumNumber(input, lastEntries);

            var numberList = MapToNumber(Map(input));



            for (int i = 0; i < numberList.Count(); i++)
            {
               for(int j = 2; j < numberList.Count() - i; j++)
                {
                    var possibleRow = numberList.Skip(i).Take(j).ToList();
                    if(possibleRow.Sum() > invalidNumber)
                    {
                        break;
                    }
                    if(possibleRow.Sum() != invalidNumber)
                    {
                        continue;
                    }

                    return possibleRow.Min() + possibleRow.Max();
                }
            }
            return -1;
        }

        private static List<string> Map(string input)
        {
            return input.Split("\n").Select(item => item?.Trim()).Where(item => !string.IsNullOrWhiteSpace(item)).ToList();
        }

        private static bool IsSumOf(long number, List<long> preamble)
        {
            var found = false;
            foreach(var item in preamble)
            {
                if (found)
                {
                    break;
                }
                foreach(var innerItem in preamble)
                {
                    if(item == innerItem)
                    {
                        continue;
                    }

                    if(item + innerItem == number)
                    {
                        found = true;
                        break;
                    }
                }
            }

            return found;
        }

        private static List<long> MapToNumber(List<string> map)
        {
            return map.Select(item => long.Parse(item)).ToList();
        }

    }
}
