using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace adventofcode
{
    public static class Day16
    {
        private static Stopwatch stopWatch = new Stopwatch();

        public static void PartOne(string[] fileLines)
        {
            stopWatch.Restart();
            Console.WriteLine("========== AdventOfCode Day 16 - Part One ==========");

            var result = GetTicketScanningErrorRate(fileLines);

            Console.WriteLine($"The ticket scanning error rate is '{result}'\r\n");
            stopWatch.Stop();
            Console.WriteLine($"=> found in {stopWatch.Elapsed:mm\\:ss\\:ffff}\r\n");
        }

        public static void PartTwo(string[] fileLines)
        {
            stopWatch.Restart();
            Console.WriteLine("========== AdventOfCode Day 16 - Part Two ==========");

            var result = "";

            Console.WriteLine($"{result}.\r\n");
            stopWatch.Stop();
            Console.WriteLine($"=> found in {stopWatch.Elapsed:mm\\:ss\\:ffff}\r\n");
        }

        private static long GetTicketScanningErrorRate(string[] fileLines)
        {
            var allInvalidValues = new List<int>();
            // First split the input in the relavant parts
            var validValues = GetValidValues(fileLines);
            var nearbyTickets = GetNearbyTickets(fileLines);

            foreach(var nearbyTicket in nearbyTickets)
            {
                var ticketValues = nearbyTicket.Split(',').Select(x => int.Parse(x));
                var invalidValues = ticketValues.Where(tv => !validValues.Contains(tv));

                allInvalidValues.AddRange(invalidValues);
            }

            return allInvalidValues.Sum();
        }

        private static string[] GetValidNearbyTickets(string[] fileLines)
        {
            var validNearbyTickets = new List<string>();
            // First split the input in the relavant parts
            var validValues = GetValidValues(fileLines);
            var nearbyTickets = GetNearbyTickets(fileLines);

            foreach (var nearbyTicket in nearbyTickets)
            {
                var ticketValues = nearbyTicket.Split(',').Select(x => int.Parse(x));
                var invalidValues = ticketValues.Where(tv => !validValues.Contains(tv));
                if (!invalidValues.Any())
                {
                    validNearbyTickets.Add(nearbyTicket);
                }                
            }

            return validNearbyTickets.ToArray();
        }

        private static HashSet<int> GetValidValues(string[] fileLines)
        {
            var data = new List<string>(fileLines);

            var maps = fileLines.Take(data.FindIndex(d => string.IsNullOrWhiteSpace(d)));
            
            var validRanges = new List<int>();
            
            foreach(var map in maps)
            {                
                var rangeOne = map.Split(':').Last().Trim().Split(' ').First().Split("-").Select(x => int.Parse(x));
                var r1 = Enumerable.Range(rangeOne.First(), (rangeOne.Last() - rangeOne.First()) + 1);
                validRanges.AddRange(r1);
                
                var rangeTwo = map.Split(':').Last().Trim().Split(' ').Last().Split("-").Select(x => int.Parse(x));
                var r2 = Enumerable.Range(rangeTwo.First(), (rangeTwo.Last() - rangeTwo.First()) + 1);
                validRanges.AddRange(r2);
            }

            return new HashSet<int>(validRanges);
        }

        private static string[] GetOwnTicket(string[] fileLines)
        {
            var data = new List<string>(fileLines);

            var ownTicket = fileLines.Skip(data.FindIndex(d => d.Contains("your ticket")) + 1).Take(1);

            return ownTicket.ToArray();
        }

        private static string[] GetNearbyTickets(string[] fileLines)
        {
            var data = new List<string>(fileLines);

            var nearbyTickets = fileLines.Skip(data.FindIndex(d => d.Contains("nearby tickets")) + 1).Take(fileLines.Length);

            return nearbyTickets.ToArray();
        }
    }
}
