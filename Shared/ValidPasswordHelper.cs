using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Shared
{
    public static class ValidPasswordHelper
    {
        public static int CountValidPasswords(string input)
        {
            var validPasswords = GetPasswordLinesThatAre(input, true, (string pw, string letter, int firstParam, int secondParam) =>
            {
                var count = pw.Where(c => c == letter.First()).Count();
                return count >= firstParam && count <= secondParam;
            });
            return validPasswords.Count();
        }

        public static int CountValidPasswordsSecond(string input)
        {
            var validPasswords = GetPasswordLinesThatAre(input, true, (string pw, string letter, int firstParam, int secondParam) =>
            {
                return pw[firstParam - 1] == letter[0] ^ pw[secondParam - 1] == letter[0];
            });
            return validPasswords.Count();
        }

        public static List<string> GetPasswordLinesThatAre(string input, bool valid, Func<string, string, int, int, bool> isValid)
        {
            var splitLines = input.Split("\n").Where(item => !string.IsNullOrEmpty(item)).Select(item => item.Trim());
            List<string> passwordsToCount = new List<string>();
            foreach (var splitLine in splitLines)
            {
                var checkLine = splitLine.Split(" ");
                if (checkLine.Count() != 3)
                {
                    Console.WriteLine($"Invalid line: {splitLine}");
                    continue;
                }

                var amountWord = checkLine[0].Split("-").Select((item) => int.Parse(item)).ToArray();
                if (amountWord.Length != 2)
                {
                    Console.WriteLine($"Invalid line: {splitLine}");
                    continue;
                }

                var letter = checkLine[1].Replace(":", "");
                var password = checkLine[2];

                bool isValidResult = isValid(password, letter, amountWord[0], amountWord[1]);
                if (isValidResult && valid || (!isValidResult && !valid))
                {
                    passwordsToCount.Add(splitLine);
                }
            }

            return passwordsToCount;
        }
    }
}
