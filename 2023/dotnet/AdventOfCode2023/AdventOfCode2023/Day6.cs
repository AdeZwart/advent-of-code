using System;
using System.Text.RegularExpressions;
using System.Xml.Schema;

namespace AzW.AdventOfCode.Year2023
{
    internal class Day6 : Day.NewLineSplitParsed<string>
    {
        private const string PATTERN = @"\d+";
        private readonly Regex regex = new Regex(PATTERN);

        public override object ExecutePart1()
        {
            //Input = GetTestInput();

            var numberOfWinningRaces = new List<int>();

            var times = regex.Matches(Input[0]).Cast<Match>().Select(m => int.Parse(m.Value)).ToList();
            var distances = regex.Matches(Input[1]).Cast<Match>().Select(m => int.Parse(m.Value)).ToList();

            // Foreach race
            foreach (var index in Enumerable.Range(0, times.Count))
            {
                //Console.WriteLine($"Race {index}; Time: {times[index]}, Distance: {distances[index]}");

                var raceTime = times[index];
                var raceDistance = distances[index];

                var winningChargeTimes = Race(raceTime, raceDistance);

                numberOfWinningRaces.Add(winningChargeTimes.Count);
            }

            return numberOfWinningRaces.Aggregate((x, y) => x * y);
        }

        public override object ExecutePart2()
        {
            //Input = GetTestInput();

            const string WHITESPACE_PATTERN = @"\s+";
            var regex = new Regex(WHITESPACE_PATTERN);
            
            var time = int.Parse(regex.Replace(Input[0].Split(':')[1], ""));
            var distance = long.Parse(regex.Replace(Input[1].Split(':')[1], ""));

            var result = Race(time, distance);

            return result.Count;
        }

        private List<int> Race(int raceTime, long raceDistance)
        {
            var winningChargeTimes = new List<int>();
            long boatSpeed = 0;

            foreach (var t in Enumerable.Range(1, raceTime))
            {
                boatSpeed++;
                long remainingRaceTime = raceTime - t;

                long distance = remainingRaceTime * boatSpeed;

                //Console.WriteLine($"Charge time: {t}, speed: {boatSpeed}, distance: {distance}");

                if (distance > raceDistance)
                {
                    winningChargeTimes.Add(t);
                }
            }

            return winningChargeTimes;
        }    

        private string[] GetTestInput()
        {
            return
            [
                "Time:      7  15   30",
                "Distance:  9  40  200"
            ];
        }
    }
}
