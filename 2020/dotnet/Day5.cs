using System;
using System.Collections.Generic;
using System.Linq;

namespace adventofcode
{
    public static class Day5
    {
        public static void PartOne(string[] fileLines)
        {
            Console.WriteLine("========== AdventOfCode Day 5 - Part One ==========");

            var seatIDs = GetSortedPasses(fileLines);
            var highestSeatId = seatIDs.Last();

            Console.Write($"The highest seat ID is '{highestSeatId}'.\r\n");
        }

        public static void PartTwo(string[] fileLines)
        {
            Console.WriteLine("========== AdventOfCode Day 5 - Part Two ==========");

            var seatIDs = GetSortedPasses(fileLines);

            var mySeatId = 0;

            for (int i = seatIDs.First(); i < seatIDs.Last(); i++)
            {
                if (!seatIDs.Contains(i))
                {
                    mySeatId = i;
                    break;
                }
            }

            Console.Write($"My unique seat ID is '{mySeatId}'.");
        }

        private static List<int> GetSortedPasses(string[] fileLines)
        {
            var seatIDs = new List<int>();

            foreach (var line in fileLines)
            {
                var rowIndicators = line.Substring(0, 7);
                var row = GetRowIndex(rowIndicators);

                var columnIndicators = line.Substring(7);
                var column = GetColumnIndex(columnIndicators);

                var seatId = (row * 8) + column;
                seatIDs.Add(seatId);
            }

            seatIDs.Sort();
            return seatIDs;
        }

        private static int GetRowIndex(string rowIndicators)
        {
            var upperBoundary = 127;
            var lowerBoundary = 0;

            foreach (var i in rowIndicators)
            {
                var range = (upperBoundary - lowerBoundary);
                if (i == 'B')
                {
                    // we're going backward
                    lowerBoundary += (range / 2) + 1;
                }

                if (i == 'F')
                {
                    // we're going forward
                    upperBoundary -= (range / 2) + 1;
                }
            }

            if (upperBoundary != lowerBoundary)
            {
                Console.WriteLine($"ERROR ERROR ERROR: {nameof(lowerBoundary)}: {lowerBoundary} && {nameof(upperBoundary)}:{upperBoundary}");
            }

            return upperBoundary;
        }

        private static int GetColumnIndex(string columnIndicators)
        {
            var upperBoundary = 7;
            var lowerBoundary = 0;

            foreach (var i in columnIndicators)
            {
                var range = (upperBoundary - lowerBoundary);
                if (i == 'R')
                {
                    // we're going backward
                    lowerBoundary += (range / 2) + 1;
                }

                if (i == 'L')
                {
                    // we're going forward
                    upperBoundary -= (range / 2) + 1;
                }
            }

            if (upperBoundary != lowerBoundary)
            {
                Console.WriteLine($"ERROR ERROR ERROR: {nameof(lowerBoundary)}: {lowerBoundary} && {nameof(upperBoundary)}:{upperBoundary}");
            }
            return upperBoundary;
        }
    }
}
