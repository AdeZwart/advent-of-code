using System.Text.RegularExpressions;

namespace AzW.AdventOfCode.Year2023
{
    public class Day1 : Day.NewLineSplitParsed<string>
    {
        private const string PATTERN = @"(\d)";

        public override object ExecutePart1()
        {
            long sum = 0;
            
            var regex = new Regex(PATTERN);

            foreach (var input in Input)
            {
                var regexGroups = regex.Matches(input);
                int.TryParse($"{regexGroups.First()}{regexGroups.Last()}", out int calibrationValue);

                sum += calibrationValue;
            }

            return sum;
        }

        public override object ExecutePart2()
        {
            long sum = 0;
            var regex = new Regex(PATTERN);

            foreach (var input in Input)
            {
                Console.WriteLine($"Input: {input}");

                var inp = input.Replace("one", "one1one")
                               .Replace("two", "two2two")
                               .Replace("three", "three3three")
                               .Replace("four", "four4four")
                               .Replace("five", "five5five")
                               .Replace("six", "six6six")
                               .Replace("seven", "seven7seven")
                               .Replace("eight", "eight8eight")
                               .Replace("nine", "nine9nine");

                Console.WriteLine($"Inp: {inp}");

                var regexGroups = regex.Matches(inp);
                int.TryParse($"{regexGroups.First()}{regexGroups.Last()}", out int calibrationValue);

                Console.WriteLine($"Calibration Value: {calibrationValue}");

                sum += calibrationValue;
                Console.WriteLine($"Sum: {sum}");
            }

            return sum;
        }
    }
}
