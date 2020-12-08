using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Shared
{
    public static class BagHelper
    {
        public static int CountDistinctWhereSearchInColors(string input, string search)
        {
            var facts = Map(input).Select(item => new FactItem(item)).ToList();
            return FindSearchColor(facts, search).Where(item => item != search).Distinct().Count();
        }

        public static int CountBagsInsideSearch(string input, string search)
        {
            var facts = Map(input).Select(item => new FactItem(item)).ToList();
            FillFactItemDependencies(facts);
            var start = facts.FirstOrDefault(item => item.What == search);

            return FindBagsInside(facts, start, true);
        }

        private static int FindBagsInside(List<FactItem> facts, FactItem start, bool init = false)
        {
            var count = 0;
            if(start == null)
            {
                return count;
            }
            count = init ? 0 : 1;
            foreach(var item in start.ContainsFactItems)
            {
                count += (item.Value * FindBagsInside(facts, item.Key));
            }

            return count;
        }

        private static void FillFactItemDependencies(List<FactItem> rootFacts, FactItem currentItem = null)
        {
            if (currentItem == null)
            {
                foreach(var item in rootFacts)
                {
                    FillFactItemDependencies(rootFacts, item);
                }
                // start
                return;
            }

            if (!currentItem.Contains.Any() || currentItem.ContainsFactItems.Any())
            {
                // end
                return;
            }

            foreach(var item in currentItem.Contains)
            {
                var found = rootFacts.FirstOrDefault(rf => rf.What == item.Key);

                if(found == null)
                {
                    continue;
                }

                currentItem.ContainsFactItems.Add(found, item.Value);
            }

            foreach(var item in currentItem.ContainsFactItems)
            {
                FillFactItemDependencies(rootFacts, item.Key);
            }
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

            public Dictionary<FactItem, int> ContainsFactItems { get; set; } = new Dictionary<FactItem, int>();

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