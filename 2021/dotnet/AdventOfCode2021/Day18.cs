using System.Text.RegularExpressions;
using Tidy.AdventOfCode;

namespace AzW.AdventOfCode2021.Year2021
{
    public class Day18 : Day.NewLineSplitParsed<string>
    {
        public override object ExecutePart1()
        {
            var result = CalculateSum(Input);

            return CalculateMagnitude(result);
            //return base.ExecutePart1();
        }

        public string CalculateSum(string[] snailfishNumbers)
        {
            var result = string.Empty;

            foreach (var snailfishNumber in snailfishNumbers)
            {
                var reducedSnailfishNumber = ReduceSnailfishNumber(snailfishNumber);                
                result = string.IsNullOrWhiteSpace(result) ? reducedSnailfishNumber : ReduceSnailfishNumber($"[{result},{reducedSnailfishNumber}]");
            }

            return result;
        }

        public string ReduceSnailfishNumber(string snailfishNumber)
        {
            //var additionLine = $"After addition: {snailfishNumber}";
            //Console.WriteLine(additionLine);
            while (true)
            {
                // If any pair is nested inside four pairs, the leftmost such pair explodes.
                var explodedSnailfishNumber = Explode(snailfishNumber);
                if (!explodedSnailfishNumber.Equals(snailfishNumber))
                {
                    //var explodeLine = $"After explode: {explodedSnailfishNumber}";
                    //Console.WriteLine(explodeLine);
                    // A pair was exploded, continue to next iteration
                    snailfishNumber = explodedSnailfishNumber;
                    continue;
                }

                // If any regular number is 10 or greater, the leftmost such regular number splits.  
                var splitSnailfishNumber = Split(snailfishNumber);
                if (!splitSnailfishNumber.Equals(snailfishNumber))
                {
                    //var splitLine = $"After split: {splitSnailfishNumber}";
                    //Console.WriteLine(splitLine);
                    // A number is split, continue to next iteration
                    snailfishNumber = splitSnailfishNumber;
                    continue;
                }

                //Console.WriteLine('\n');
                break;
            }

            return snailfishNumber;
        }

        public string Explode(string snailfishNumber)
        {
            var regex = @"\d+,\d+";
            var match = Regex.Match(snailfishNumber, regex);

            while (match.Success)
            {
                if (GetEnclosedCount(snailfishNumber.Take(match.Index)) > 4)
                {
                    var intRegex = @"\d+";
                    var values = match.Value.Split(',');

                    var leftValueMatch = Regex.Match(string.Join("", snailfishNumber.Take(match.Index - 1)), intRegex, RegexOptions.RightToLeft);
                    if (leftValueMatch.Success)
                    {
                        var value = int.Parse(leftValueMatch.Value) + int.Parse(values.First());
                        snailfishNumber = snailfishNumber.Remove(leftValueMatch.Index, leftValueMatch.Length).Insert(leftValueMatch.Index, $"{value}");
                    }

                    var rightValueMatch = Regex.Match(string.Join("", snailfishNumber.Skip(match.Index - 1 + match.Value.Length + 2)), intRegex); // All hail the magic numbers!
                    if (rightValueMatch.Success)
                    {
                        var value = int.Parse(rightValueMatch.Value) + int.Parse(values.Last());
                        var index = rightValueMatch.Index + match.Index - 1 + match.Value.Length + 2; // All hail the magic numbers!
                        snailfishNumber = snailfishNumber.Remove(index, rightValueMatch.Length).Insert(index, $"{value}");
                    }
                    break;
                }

                match = match.NextMatch();
            }

            return new Regex(Regex.Escape($"[{match.Value}]")).Replace(snailfishNumber, "0", 1);
        }

        public string Split(string snailfishNumber)
        {
            var regex = @"[0-9]{2}";
            var match = Regex.Match(snailfishNumber, regex);

            if (match.Success)
            {
                var num = int.Parse(match.Value);

                var left = Math.Floor(num / 2d);
                var right = Math.Ceiling(num / 2d);

                var splitResult = $"[{left},{right}]";

                return new Regex(Regex.Escape(match.Value)).Replace(snailfishNumber, splitResult, 1);
            }

            return snailfishNumber;
        }

        public long CalculateMagnitude(string snailfishNumber)
        {
            var regex = @"\[\d+,\d+\]";
            var match = Regex.Match(snailfishNumber, regex);

            while (match.Success)
            {
                var values = match.Value.Trim(new char[] { '[', ']' }).Split(',');

                var totalResult = (int.Parse(values.First()) * 3) + (int.Parse(values.Last()) * 2);

                snailfishNumber = snailfishNumber.Replace(match.Value, totalResult.ToString());

                match = match.NextMatch();
            }

            return (Regex.Match(snailfishNumber, @"\D").Success) ? CalculateMagnitude(snailfishNumber) : long.Parse(snailfishNumber);
        }

        private int GetEnclosedCount(IEnumerable<char> input)
        {
            return input.Where(c => c.Equals('[')).Count() - input.Where(c => c.Equals(']')).Count();
        }
    }
}
