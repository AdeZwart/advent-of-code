using System.Text.RegularExpressions;

namespace AzW.AdventOfCode.Year2023
{
    public class Day3 : Day.NewLineSplitParsed<string>
    {
        private const string NUMBER_PATTERN = @"\d+";
        private readonly Regex numberRegex = new Regex(NUMBER_PATTERN);

        private const string GEAR_PATTERN = @"[*]";
        private readonly Regex gearRegex = new Regex(GEAR_PATTERN);

        public override object ExecutePart1()
        {
            var partNumbers = new List<long>();

            foreach (var lineIndex in Enumerable.Range(0, Input.Length))
            {
                var matches = numberRegex.Matches(Input[lineIndex]);
                foreach (Match m in matches)
                {
                    var value = long.Parse(m.Value);
                    var index = m.Index;

                    if (IsPartNumber(lineIndex, index, m.Length))
                    {
                        partNumbers.Add(value);
                    }
                }
            }

            return partNumbers.Sum();
        }

        public override object ExecutePart2()
        {
            var gearRatios = new List<long>();

            foreach (var lineIndex in Enumerable.Range(0, Input.Length))
            {
                var gearMarkers = gearRegex.Matches(Input[lineIndex]);
                foreach (Match m in gearMarkers.Cast<Match>())
                {
                    var gearRatio = GetGearRatio(lineIndex, m.Index);
                    if (gearRatio > 0)
                    {
                        gearRatios.Add(gearRatio);
                    }
                }
            }

            return gearRatios.Sum();
        }

        private bool IsPartNumber(int lineIndex, int valueIndex, int valueLength)
        {
            foreach (var line in Enumerable.Range(lineIndex - 1, 3))
            {
                foreach (var position in Enumerable.Range(valueIndex - 1, valueLength + 2))
                {
                    if (!IsOutOfBound(line, position) && (Input[line][position] != 46 && !Enumerable.Range(48, 10).Contains(Input[line][position])))
                    {
                        Console.WriteLine($"Value is adjacent to Symbol {Input[line][position]}");
                        return true;
                    }
                }
            }

            return false;
        }

        private long GetGearRatio(int lineIndex, int gearIndex)
        {
            var partNumbers = new List<long>();

            foreach (var index in Enumerable.Range(lineIndex - 1, 3))
            {
                if (index < 0 || index >= Input.Length){
                    continue;
                }

                var matches = numberRegex.Matches(Input[index]);
                foreach (Match m in matches.Cast<Match>())
                {
                    if (Enumerable.Range(m.Index - 1, m.Length + 2).Contains(gearIndex))
                    {
                        partNumbers.Add(long.Parse(m.Value));
                    }
                }
            }

            if (partNumbers.Count < 2)
            {
                return 0;
            }

            return partNumbers.Aggregate((x, y) => x * y);
        }

        private bool IsOutOfBound(int lineIndex, int positionIndex)
        {
            return (lineIndex < 0 || lineIndex >= Input.Length || positionIndex < 0 || positionIndex >= Input[lineIndex].Length);
        }
    }
}
