using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Shared
{
    public static class PassportHelper
    {
        public static int CountValid(string input, string[] requiredFields, string[] optionalFields, Dictionary<string, string> validateFieldPattern, Func<Dictionary<string,string>, bool> isValid = null)
        {
            var map = Map(input);

            var count = 0;

            bool isValidIntern(Dictionary<string, string> inputKvp)
            {
                var leftRequiredFields = requiredFields?.ToList();
                foreach (var kvPair in inputKvp)
                {
                    if (validateFieldPattern?.ContainsKey(kvPair.Key) == true)
                    {
                        var regex = new Regex(validateFieldPattern[kvPair.Key]);
                        if (!regex.IsMatch(kvPair.Value))
                        {
                            return false;
                        }
                    }

                    if (leftRequiredFields?.Contains(kvPair.Key) == true){
                        leftRequiredFields.Remove(kvPair.Key);
                        continue;
                    }
                    if(optionalFields?.Contains(kvPair.Key) == true)
                    {
                        continue;
                    }
                    return false;
                }
                return leftRequiredFields.Count() == 0;
            }
           

            foreach(var item in map)
            {
                var validItem = true;
                try
                {
                    var kv = GetKeyValue(item);
                    if(isValid != null)
                    {
                        validItem = isValid(kv);
                    }
                    else
                    {
                        validItem = isValidIntern(kv);
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Invalid input {input}: {ex.Message}");
                    validItem = false;
                }

                if (validItem)
                {
                    count++;
                }
            }

            return count;
        }

        private static List<string> Map(string input)
        {
            var lines = input?.Split("\n") ?? new string[0];
            if(lines.Length <= 1) {
                return lines?.ToList() ?? Enumerable.Empty<string>().ToList();
            }
            List<string> result = new List<string>();
            StringBuilder b = new StringBuilder();
            foreach(var line in lines)
            {
                if(string.IsNullOrWhiteSpace(line))
                {
                    // next
                    result.Add(b.ToString());
                    b = new StringBuilder();
                }
                else
                {
                    b.Append(" ");
                    b.Append(line);
                }
            }
            result.Add(b.ToString());
            return result;
        }

        private static Dictionary<string,string> GetKeyValue(string input)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            var kvs = input.Split(" ").Where(item => !string.IsNullOrWhiteSpace(item)).Select(item => item.Trim());
            foreach(var kv in kvs)
            {
                var split = kv.Split(":").Where(item => !string.IsNullOrEmpty(item)).Select(item => item.Trim()).ToArray();
                result.Add(split[0], split[1]);
            }
            return result;
        }
    }
}
