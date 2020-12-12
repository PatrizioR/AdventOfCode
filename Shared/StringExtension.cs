using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Shared
{
    public static class StringExtension
    {
        public static List<string> TrimmedNotEmptyMap(this string input)
        {
            return input.Split("\n").Where((item) => !string.IsNullOrWhiteSpace(item)).Select((item) => item.Trim()).ToList();
        }
    }
}