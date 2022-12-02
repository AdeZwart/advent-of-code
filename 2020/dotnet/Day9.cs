using System;
using System.Collections.Generic;
using System.Linq;

namespace adventofcode
{
    public static class Day9
    {
        private const int preambleSize = 25;

        public static void PartOne(string[] fileLines)
        {
            Console.WriteLine("========== AdventOfCode Day 9 - Part One ==========");

            var weakness = FindWeakness(fileLines, preambleSize);

            Console.WriteLine($"The vulnerability in the XMAS data is number '{weakness}'");
        }

        public static void PartTwo(string[] fileLines)
        {
            Console.WriteLine("========== AdventOfCode Day 9 - Part Two ==========");

            var weakness = FindWeakness(fileLines, preambleSize);

            var encryptionAttack = FindEncryptionAttack(fileLines, weakness);

            Console.WriteLine($"The XMAS encryption can be attacked at '{encryptionAttack}'");
        }

        private static long FindWeakness(string[] fileLines, int preamleSize)
        {
            var preamble = new List<long>();
            foreach (var line in fileLines)
            {
                if (!long.TryParse(line, out var nextNumber))
                {
                    // unable to parse as long.
                    continue;
                }

                if (preamble.Count < preamleSize)
                {
                    preamble.Add(nextNumber);
                    continue;
                }

                if (ContainsPair(nextNumber, preamble))
                {
                    // Add the next number to the preamble
                    preamble.Add(nextNumber);
                    // Remove the first entry in the preamble
                    preamble = preamble.Skip(1).ToList();
                }
                else
                {
                    return nextNumber;
                }
            }

            return long.MinValue;
        }

        private static long FindEncryptionAttack(string[] fileLines, long weakness)
        {
            var contiguousSet = new List<long>();
            foreach (var line in fileLines)
            {
                long.TryParse(line, out var nextNumber);
                contiguousSet.Add(nextNumber);

                var sum = contiguousSet.Sum();
                if (sum == weakness)
                {
                    // Found it!
                    contiguousSet.Sort();
                    return contiguousSet.First() + contiguousSet.Last();
                }
                else if (sum > weakness)
                {
                    // gotta start over at +1
                    var rslt = FindEncryptionAttack(fileLines.ToList().Skip(1).ToArray(), weakness);
                    if(rslt > long.MinValue)
                    {
                        return rslt;
                    }
                }
            }

            return long.MinValue;
        }

        private static bool ContainsPair(long result, List<long> preamble)
        {
            for (int i = 0; i < preamble.Count; i++)
            {
                foreach (var j in preamble.Skip(i + 1))
                {
                    if (preamble[i] + j == result)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
