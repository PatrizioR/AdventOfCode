using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Shared
{
    public static class BagHelper
    {
        public static int CountColors(string input, string search)
        {
            var facts = Map(input).Select(item => new FactItem(item)).ToList();
            return FindSearchColor(facts, search).Where(item => item != search).Distinct().Count();
        }

        private static List<string> FindSearchColor(List<FactItem> factItems, string search, FactItem currentFact = null)
        {
            List<string> count = new List<string>();

            // start
            if (currentFact == null)
            {
                foreach (var fact in factItems)
                {
                    var foundItems = FindSearchColor(factItems, search, fact);
                    if (foundItems.Any())
                    {
                        count.Add(fact.What);
                        count.AddRange(foundItems);
                    }
                }
            }
            else if (currentFact.What == search)
            {
                count.Add(currentFact.What);
            }

            if (currentFact?.Contains.Any() != true)
            {
                return count;
            }
            foreach (var containFact in currentFact.Contains)
            {
                var curFact = factItems.FirstOrDefault(item => item.What == containFact.Key);
                if (curFact == null)
                {
                    continue;
                }
                var foundItems = FindSearchColor(factItems, search, curFact);
                if (foundItems.Any())
                {
                    count.Add(containFact.Key);
                    count.AddRange(foundItems);
                }
            }

            return count;
        }

        private static List<string> Map(string input)
        {
            return input.Split("\n").Select(item => item?.Trim()).Where(item => !string.IsNullOrWhiteSpace(item)).ToList();
        }

        public class FactItem
        {
            public string What { get; set; }
            public Dictionary<string, int> Contains { get; set; } = new Dictionary<string, int>();

            public FactItem(string input)
            {
                if (string.IsNullOrEmpty(input) || !input.EndsWith('.') || !input.Contains("bags contain"))
                {
                    throw new ArgumentNullException($"Invalid input: {input}");
                }
                var splitInput = input.Split("bags contain");
                What = splitInput[0].Trim();
                var splitContains = splitInput[1];
                if (!splitContains.Contains("no other bags."))
                {
                    var splitWhat = splitContains.Split(',').Select(item => item.Trim().Replace(".", ""));

                    foreach (var item in splitWhat)
                    {
                        var splitItem = item.Split(" ");
                        if (splitItem.Length != 4)
                        {
                            throw new ArgumentException($"Invalid argument: {item}", nameof(input));
                        }

                        Contains.Add($"{splitItem[1]} {splitItem[2]}".Trim(), int.Parse(splitItem[0].Trim()));
                    }
                }
            }
        }
    }
}