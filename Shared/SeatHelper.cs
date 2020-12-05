using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Shared
{
    public static class SeatHelper
    {
        public static int GetMaxSeatId(string input)
        {
            var list = Map(input);
            var max = 0;

            foreach (var item in list)
            {
                var rowsAndCols = GetRowsAndCols(item);
                var res = rowsAndCols.Item1 * 8 + rowsAndCols.Item2;
                max = Math.Max(res, max);
            }
            return max;
        }

        public static int GetMissingMiddleSeat(string input)
        {
            var list = Map(input);
            List<int> ids = new List<int>();
            foreach (var item in list)
            {
                var rowsAndCols = GetRowsAndCols(item);
                var res = rowsAndCols.Item1 * 8 + rowsAndCols.Item2;
                ids.Add(res);
            }
            List<int> possibleIds = new List<int>();
            foreach (var id in ids)
            {
                if (id > GetMaxSeatId("FFFFFFFRRR") && id < GetMaxSeatId("BBBBBBBLLL") && ids.Contains(id + 2) && !ids.Contains(id + 1))
                {
                    possibleIds.Add(id + 1);
                }
            }
            if (possibleIds.Count() != 1)
            {
                return possibleIds.Count();
            }
            return possibleIds.First();
        }

        private static List<string> Map(string input)
        {
            return input.Split("\n").Where((item) => !string.IsNullOrWhiteSpace(item)).Select((item) => item.Trim()).ToList();
        }

        public const int INPUTLENGTH = 10;

        private static string GetPart(string input, bool getRows)
        {
            if (input.Length != INPUTLENGTH)
            {
                throw new ArgumentException($"Input needs to have length {INPUTLENGTH}", nameof(input));
            }

            var rows = input.Substring(0, 7);
            var columns = input.Substring(7, 3);

            if (getRows)
            {
                return rows;
            }
            return columns;
        }

        private static string GetRowsPart(string input)
        {
            return GetPart(input, true);
        }

        private static string GetColumnsPart(string input)
        {
            return GetPart(input, false);
        }

        private static Tuple<int, int> GetRowsAndCols(string input)
        {
            var rowsPart = GetRowsPart(input);
            var colsPart = GetColumnsPart(input);
            var bitsAsRows = rowsPart.Select(item => GetBit(item));
            var bitsAsCols = colsPart.Select(item => GetBit(item));
            var rowsBitsAsString = string.Join("", bitsAsRows);
            var colsBitsAsString = string.Join("", bitsAsCols);
            var rows = Convert.ToInt32(rowsBitsAsString, 2);
            var cols = Convert.ToInt32(colsBitsAsString, 2);
            return new Tuple<int, int>(rows, cols);
        }

        private static int GetBit(char c)
        {
            return c == 'B' || c == 'R' ? 1 : 0;
        }
    }
}