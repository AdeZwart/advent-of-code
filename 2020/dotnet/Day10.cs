using System;
using System.Collections.Generic;
using System.Linq;

namespace adventofcode
{
    public static class Day10
    {
        private const int minJoltDiff = 1;
        private const int maxJoltDiff = 3;

        public static void PartOne(string[] fileLines)
        {
            Console.WriteLine("========== AdventOfCode Day 10 - Part One ==========");

            var sortedAdapters = GetSortedAdapters(fileLines);

            var oneDiff = 0;
            // threeDiff has an offset of one because of the build-in adapter
            var threeDiff = 1;

            for (int i = 0; i < sortedAdapters.Count; i++)
            {
                var diff = (i == 0) ? sortedAdapters[i] - 0 : sortedAdapters[i] - sortedAdapters[i - 1];

                switch (diff)
                {
                    case 1:
                        oneDiff++;
                        break;
                    case 2:
                        // Nothing ¯\_(ツ)_/¯
                        break;
                    case 3:
                        threeDiff++;
                        break;
                }
            }

            Console.WriteLine($"The outcome of multiplying 1- and 3-jolt difference counts is '{oneDiff * threeDiff}'\r\n");
        }

        public static void PartTwo(string[] fileLines)
        {
            Console.WriteLine("========== AdventOfCode Day 10 - Part Two ==========");

            var sortedAdapters = GetSortedAdapters(fileLines);

            var arrangementCount = GetAmountOfWorkingCombinations(sortedAdapters);

            Console.WriteLine($"A total of '{arrangementCount}' distinct arrangements are available.\r\n");
        }

        private static List<int> GetSortedAdapters(string[] fileLines)
        {
            var adapters = new List<int>();
            foreach (var line in fileLines)
            {
                if (!int.TryParse(line, out var adapter))
                {
                    // ouch, we broke the system
                }

                adapters.Add(adapter);
            }

            adapters.Sort();

            return adapters;
        }        

        private static int GetAmountOfWorkingCombinations(List<int> sortedAdapters, int start = 0, int workingCombinationCount = 0)
        {           
            return workingCombinationCount;
        }

        private static long Reddit(string[] input)
        {
            var joltages = input.Select(x => int.Parse(x)).Append(0).OrderBy(x => x).ToArray();
            var steps = new long[joltages.Length];
            steps[0] = 1;
            foreach (var i in Enumerable.Range(1, joltages.Length - 1))
            {
                foreach (var j in Enumerable.Range(0, i))
                {
                    if (joltages[i] - joltages[j] <= 3)
                    {
                        steps[i] += steps[j];
                    }
                }
            }
            return steps.Last();
        }

        private static bool IsJoltStepInRange(int currentJolt, int nextJolt)
        {
            var diff = nextJolt - currentJolt;
            return diff >= minJoltDiff && diff <= maxJoltDiff;
        }
    }
}
