using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Shared
{
    public static class JoltageAdapterHelper
    {
        public static Dictionary<int, int> JoltChainDiff(string input, int maxStep, int ownMaxAdd = 3)
        {
            var list = MapToSortedNumber(Map(input));
            list.Add(list.Max() + ownMaxAdd);
            return CountDiff(list, maxStep);
        }

        public static long JoltPerm(string input, int maxStep, int ownMaxAdd = 3)
        {
            var list = MapToSortedNumber(Map(input));
            list.Add(list.Max() + ownMaxAdd);
            return Dp(0, list, maxStep);
        }

        private static long Dp(int index, List<long> sortedList, int maxStep)
        {
            if (index == sortedList.Count() - 1)
            {
                return 1;
            }
            var ans = 0L;
            foreach (var j in Enumerable.Range(index + 1, sortedList.Count() - (index + 1)))
            {
                if (sortedList[j] - sortedList[index] <= maxStep)
                {
                    ans += Dp(j, sortedList, maxStep);
                }
            }
            return ans;
        }

        private static Dictionary<int, int> CountDiff(List<long> sortedList, int maxStep)
        {
            long last = 0;
            Dictionary<int, int> result = new Dictionary<int, int>();
            foreach (var item in sortedList)
            {
                var diff = (int)(item - last);

                if (diff > maxStep)
                {
                    throw new ArgumentException($"Diff is too high: {diff}/{maxStep} (from {last} to {item})");
                }

                if (result.ContainsKey(diff))
                {
                    result[diff]++;
                }
                else
                {
                    result.Add(diff, 1);
                }
                last = item;
            }
            return result;
        }

        private static List<string> Map(string input)
        {
            return input.Split("\n").Select(item => item?.Trim()).Where(item => !string.IsNullOrWhiteSpace(item)).ToList();
        }

        private static List<long> MapToSortedNumber(List<string> map)
        {
            return map.Select(item => long.Parse(item)).ToList().OrderBy(item => item).ToList();
        }

        public static int JoltMultChainDiff(string input, int maxStep, List<int> joltMults)
        {
            var chain = JoltChainDiff(input, maxStep);
            var result = 1;
            foreach (var mult in joltMults)
            {
                if (!chain.Keys.Contains(mult))
                {
                    continue;
                }

                result *= chain[mult];
            }

            return result;
        }
    }
}