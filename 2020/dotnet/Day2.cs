using System;
using System.Linq;

namespace adventofcode
{
    public static class Day2
    {
        public static void FindValidPasswordsPartOne(string[] fileLines)
        {
            Console.WriteLine("========== AdventOfCode Day 2 - Part One ==========");

            var validPasswordCount = 0;
            foreach (var line in fileLines)
            {
                var policy = line.Split(':').First().Trim();
                var password = line.Split(':').Last().Trim();

                var policyParts = policy.Split(' ');

                int.TryParse(policyParts.First().Trim().Split('-').First(), out var policyCharMin);
                int.TryParse(policyParts.First().Trim().Split('-').Last(), out var policyCharMax);

                var policyChar = policyParts.Last().Trim().ToCharArray();
                if (policyChar.Length != 1)
                {
                    // something's wrong.
                }

                var charCount = password.Count(c => c.Equals(policyChar.First()));
                if (charCount >= policyCharMin && charCount <= policyCharMax)
                {
                    validPasswordCount++;
                }
            }

            Console.WriteLine($"The puzzle input contains {validPasswordCount} passwords that are valid according to their policy.\r\n");
        }

        public static void FindValidPasswordsPartTwo(string[] fileLines)
        {
            Console.WriteLine("========== AdventOfCode Day 2 - Part Two ==========");

            var validPasswordCount = 0;
            foreach (var line in fileLines)
            {
                var policy = line.Split(':').First().Trim();
                var password = line.Split(':').Last().Trim();

                var policyParts = policy.Split(' ');

                if (!int.TryParse(policyParts.First().Trim().Split('-').First(), out var policyCharMin))
                {
                    // something's wrong
                }
                policyCharMin -= 1;

                if (!int.TryParse(policyParts.First().Trim().Split('-').Last(), out var policyCharMax))
                {
                    // something's wrong
                }
                policyCharMax -= 1;

                var policyChars = policyParts.Last().Trim().ToCharArray();
                if (policyChars.Length != 1)
                {
                    // something's wrong.
                }
                var policyChar = policyChars.First();

                if ((password[policyCharMin] == policyChar && password[policyCharMax] != policyChar)
                    || (password[policyCharMin] != policyChar && password[policyCharMax] == policyChar))
                {

                    validPasswordCount++;
                }
            }

            Console.WriteLine($"The puzzle input contains {validPasswordCount} passwords that are valid according to their policy.\r\n");
        }
    }
}
