using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace adventofcode
{
    public static class Day15
    {
        private static Stopwatch stopWatch = new Stopwatch();

        public static void PartOne(string[] fileLines)
        {            
            Console.WriteLine("========== AdventOfCode Day 15 - Part One ==========");
                       
            var result = GetNthNumberSpoken(fileLines, 2020);

            Console.WriteLine($"The 2020th number spoken is {result}.\r\n");            
        }

        public static void PartTwo(string[] fileLines)
        {            
            Console.WriteLine("========== AdventOfCode Day 15 - Part Two ==========");
            
            var result = GetNthNumberSpoken(fileLines, 30000000);
            
            Console.WriteLine($"The 30000000th number spoken is {result}.\r\n");            
        }

        private static int GetNthNumberSpoken(string[] input, long n)
        {
            stopWatch.Restart();

            var numbers = new List<int>(input.First().Split(',').Select(x => int.Parse(x)));

            while (numbers.Count < n)
            {
                if (numbers.Count == 10000)
                {
                    Console.WriteLine($"=> 10000 in {stopWatch.Elapsed:mm\\:ss\\:ffff}\r\n");
                }
                else if (numbers.Count == 100000)
                {
                    Console.WriteLine($"=> 100000 in {stopWatch.Elapsed:mm\\:ss\\:ffff}\r\n");
                }
                else if (numbers.Count == 1000000)
                {
                    Console.WriteLine($"=> 1000000 in {stopWatch.Elapsed:mm\\:ss\\:ffff}\r\n");
                }
                else if (numbers.Count == 5000000)
                {
                    Console.WriteLine($"=> 5000000 in {stopWatch.Elapsed:mm\\:ss\\:ffff}\r\n");
                }
                else if (numbers.Count == 10000000)
                {
                    Console.WriteLine($"=> 10000000 in {stopWatch.Elapsed:mm\\:ss\\:ffff}\r\n");
                }
                else if (numbers.Count == 15000000)
                {
                    Console.WriteLine($"=> 15000000 in {stopWatch.Elapsed:mm\\:ss\\:ffff}\r\n");
                }
                else if (numbers.Count == 20000000)
                {
                    Console.WriteLine($"=> 20000000 in {stopWatch.Elapsed:mm\\:ss\\:ffff}\r\n");
                }

                var lastCalledNumber = numbers.Last();
                var numberHistory = numbers.Take(numbers.Count - 1).ToList();

                var previouslySpokenTurn = numberHistory.FindLast(lastCalledNumber);
                if (previouslySpokenTurn < 0)
                {
                    // The previous number was new
                    // So the number spoken is 0
                    numbers.Add(0);
                    continue;
                }

                var age = numbers.Count - previouslySpokenTurn;
                numbers.Add(age);                
            }

            stopWatch.Stop();
            Console.WriteLine($"=> found in {stopWatch.Elapsed:mm\\:ss\\:ffff}\r\n");

            return numbers.Last();
        }


        private static int FindLast(this List<int> list, int value)
        {     
            for (int i = list.Count; i > 0; i--)
            {
                if (list[i - 1] == value)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}