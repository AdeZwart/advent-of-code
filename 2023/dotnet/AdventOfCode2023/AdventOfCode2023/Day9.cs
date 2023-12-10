namespace AzW.AdventOfCode.Year2023
{
    public class Day9 : Day.NewLineSplitParsed<string>
    {
        public override object ExecutePart1()
        {
            //Input = GetTestInput();

            var extrapolatedValues = new List<long>();

            foreach (var line in Input)
            {
                //Console.WriteLine(line);
                var sensorReadings = line.Split(' ').Select(long.Parse).ToList();

                var diffResult = GetExtrapolatedValue(sensorReadings);

                var extrapolatedValue = sensorReadings.Last() + diffResult.Last();
                //Console.WriteLine($"Extrapolated value: {extrapolatedValue}");
                extrapolatedValues.Add(extrapolatedValue);
            }

            return extrapolatedValues.Sum();
        }


        public override object ExecutePart2()
        {
            //Input = GetTestInput();

            var extrapolatedValues = new List<long>();

            foreach (var line in Input)
            {
                //Console.WriteLine(line);
                var sensorReadings = line.Split(' ').Select(long.Parse).ToList();

                var diffResult = GetExtrapolatedValue(sensorReadings, true);

                var extrapolatedValue = sensorReadings.First() - diffResult.First();
                //Console.WriteLine($"Extrapolated value: {extrapolatedValue}");
                extrapolatedValues.Add(extrapolatedValue);
            }

            return extrapolatedValues.Sum();
        }

        private static List<long> GetExtrapolatedValue(List<long> input, bool isBackwards = false)
        {
            var differences = new List<long>();
            foreach (var index in Enumerable.Range(0, input.Count - 1))
            {
                var diff = input[index + 1] - input[index];
                //Console.Write($"{diff} ");

                differences.Add(diff);
            }
            //Console.Write("\n");

            if (differences.TrueForAll(d => d == 0))
            {

                differences.Add(0);
                //Console.WriteLine($"Extrapolation: {0}");
                return differences;
            }

            var diffResult = GetExtrapolatedValue(differences, isBackwards);

            long extrapolatedValue;
            if (isBackwards)
            {
                extrapolatedValue = differences.First() - diffResult.First();
                //Console.WriteLine($"Extrapolation: {extrapolatedValue}");
                differences.Insert(0, extrapolatedValue);
            }
            else
            {
                extrapolatedValue = differences.Last() + diffResult.Last();
                //Console.WriteLine(extrapolatedValue);
                differences.Add(extrapolatedValue);
            }

            return differences;
        }

        private string[] GetTestInput()
        {
            return
            [
                "0 3 6 9 12 15",
                "1 3 6 10 15 21",
                "10 13 16 21 30 45"
            ];
        }
    }
}
