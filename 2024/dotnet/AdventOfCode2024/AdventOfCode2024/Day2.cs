

namespace AzW.AdventOfCode.Year2024
{
    public class Day2 : Day.NewLineSplitParsed<string>
    {
        public override object ExecutePart1()
        {
            var totalOfSafeReports = 0;

            foreach (var report in Input)
            {
                var parsedReport = ParseReport(report);

                if (IsReportSafe(parsedReport.ToArray()))
                {
                    totalOfSafeReports++;
                }
            }

            return totalOfSafeReports;
        }

        public override object ExecutePart2()
        {
            var totalOfSafeReports = 0;

            foreach (var report in Input)
            {
                var parsedReport = ParseReport(report);

                Console.Write($"Report: {string.Join(",", parsedReport.Select(x => x.ToString()).ToArray())} => ");

                var isReportSafe = IsReportSafe(parsedReport.ToArray(), true);
                Console.Write($"is safe: {isReportSafe}");
                Console.WriteLine();
                if (isReportSafe)
                {
                    totalOfSafeReports++;
                }
            }

            return totalOfSafeReports;
        }


        public bool IsReportSafe(int[] report, bool problemDampenerEnabled = false)
        {
            var levelSteps = CalculateLevelSteps(report).ToList();

            Console.Write($"level steps: {string.Join(",", levelSteps.Select(x => x.ToString()).ToArray())} => ");

            int? badLevelIndex = null;

            var positiveSteps = levelSteps.Count(x => x >= 0);
            var negativeSteps = levelSteps.Count(x => x <= 0);

            // Are we consistently increasing or decreasing?
            if (positiveSteps != levelSteps.Count && negativeSteps != levelSteps.Count)
            {
                if (!problemDampenerEnabled)
                {
                    return false;
                }

                if (negativeSteps == 1)
                {
                    badLevelIndex = levelSteps.FindIndex(x => x < 0);
                    // If the last index is the problem
                    if (badLevelIndex == levelSteps.Count - 1)
                    {
                        // Pop the last level of the report
                        badLevelIndex = report.Length - 1;
                    }
                }

                if (positiveSteps == 1)
                {
                    badLevelIndex = levelSteps.FindIndex(x => x > 0);
                    // If the last index is the problem
                    if (badLevelIndex == levelSteps.Count - 1)
                    {
                        // Pop the last level of the report
                        badLevelIndex = report.Length - 1;
                    }
                }

                if (!badLevelIndex.HasValue || badLevelIndex.Value < 0)
                {
                    return false;
                }

                var dampenedReport = report.ToList();
                dampenedReport.RemoveAt(badLevelIndex.Value);

                var isSafeAfterDampening = IsReportSafe([.. dampenedReport], false);
                if (!isSafeAfterDampening)
                {
                    return false;
                }

            }

            if (badLevelIndex.HasValue && badLevelIndex.Value >= 0)
            {
                if (badLevelIndex.Value >= levelSteps.Count)
                {
                    badLevelIndex = levelSteps.Count - 1;
                }

                levelSteps.RemoveAt(badLevelIndex.Value);
            }

            foreach (var index in Enumerable.Range(0, levelSteps.Count))
            {
                var stepSize = (levelSteps.ElementAt(index) < 0)
                    ? -levelSteps.ElementAt(index)
                    : levelSteps.ElementAt(index);

                // Is the step size within range?
                if (stepSize < 1 || stepSize > 3)
                {
                    if (!problemDampenerEnabled)
                    {
                        return false;
                    }

                    badLevelIndex = levelSteps.FindIndex(x => x == stepSize);
                    // If the last index is the problem
                    if (badLevelIndex == levelSteps.Count - 1)
                    {
                        // Pop the last level of the report
                        badLevelIndex = report.Length - 1;
                    }

                    if (!badLevelIndex.HasValue || badLevelIndex.Value < 0)
                    {
                        return false;
                    }

                    var dampenedReport = report.ToList();
                    dampenedReport.RemoveAt(badLevelIndex.Value);

                    var isSafeAfterDampening = IsReportSafe([.. dampenedReport], false);
                    if (!isSafeAfterDampening)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public IEnumerable<int> CalculateLevelSteps(int[] report)
        {
            var levelSteps = new List<int>();

            foreach (var index in Enumerable.Range(1, report.Length - 1))
            {
                var distance = report[index] - report[index - 1];
                levelSteps.Add(distance);
            }

            return levelSteps;
        }

        public IEnumerable<int> ParseReport(string input)
        {
            var extractedLevels = input.Split(' ');

            return extractedLevels.Select(x => int.Parse(x));
        }
    }
}
