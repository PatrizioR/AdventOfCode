using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Shared
{
    public static class AnswerHelper
    {
        public static int CountAnyone(string input)
        {
            var groups = Group(Map(input));

            var count = 0;
            foreach (var group in groups)
            {
                count += DistinctChars(GroupAnswers(group)).Count();
            }

            return count;
        }

        public static int CountEveryone(string input)
        {
            var groups = Group(Map(input));

            var count = 0;
            foreach (var group in groups)
            {
                count += AllChars(GroupAnswers(group)).Count();
            }

            return count;
        }

        public static List<char> AllChars(List<List<char>> groupAnswers)
        {
            var result = groupAnswers.SelectMany(item => item);

            return result.Where(item => result.Count(c => c == item) == groupAnswers.Count()).Distinct().ToList();
        }

        public static List<char> DistinctChars(List<List<char>> groupAnswers)
        {
            var result = groupAnswers.SelectMany(item => item).Distinct();
            return result.ToList();
        }

        public static List<List<char>> GroupAnswers(List<string> group)
        {
            List<List<char>> result = new List<List<char>>();
            foreach (var item in group)
            {
                result.Add(item.ToList());
            }
            return result;
        }

        public static List<string> Map(string input)
        {
            return input.Split("\n").ToList();
        }

        public static List<List<string>> Group(List<string> map)
        {
            List<List<string>> result = new List<List<string>>();
            var curList = new List<string>();
            foreach (var item in map)
            {
                if (string.IsNullOrEmpty(item))
                {
                    result.Add(curList);
                    curList = new List<string>();
                }
                else
                {
                    curList.Add(item);
                }
            }
            if (curList.Any())
            {
                result.Add(curList);
            }

            return result;
        }
    }
}