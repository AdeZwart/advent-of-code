using System;
using System.Collections.Generic;
using System.Linq;

namespace adventofcode
{
    public static class Day6
    {
        public static void PartOne(string[] fileLines)
        {
            Console.WriteLine("========== AdventOfCode Day 6 - Part One ==========");

            var total = GetAnswerCount(fileLines);

            Console.WriteLine($"The sum of yes answers for anyone in the group is '{total}'\r\n");
        }

        public static void PartTwo(string[] fileLines)
        {
            Console.WriteLine("========== AdventOfCode Day 6 - Part Two ==========");

            var total = GetAnswerCount(fileLines, true);

            Console.WriteLine($"The sum of yes answers for everyone in the group is '{total}'\r\n");
        }

        private static int GetAnswerCount(string[] fileLines, bool everyone = false)
        {
            var totalAnswerCount = 0;
            var groupAnswers = string.Empty;
            var groupedAnswers = new List<IEnumerable<char>>();

            for (int i = 0; i < fileLines.Length; i++)
            {
                var line = fileLines[i];
                if (line.Length > 0)
                {
                    if (everyone)
                    {
                        groupedAnswers.Add(line);
                    }
                    else
                    {
                        groupAnswers = $"{groupAnswers}{line}";
                    }
                }

                // If we hit an empty line || when it's the last entry of the input
                if (line.Length == 0 || i == fileLines.Length - 1)
                {
                    if (everyone)
                    {
                        var rslt = groupedAnswers.IntersectAll();
                        totalAnswerCount += rslt.Distinct().Count();
                    }
                    else
                    {
                        var totalAnswersInGroup = groupAnswers.Distinct();
                        totalAnswerCount += totalAnswersInGroup.Count();
                    }

                    groupedAnswers = new List<IEnumerable<char>>();
                    groupAnswers = string.Empty;
                }
            }

            return totalAnswerCount;
        }

        private static IEnumerable<TSource> IntersectAll<TSource>(
                this IEnumerable<IEnumerable<TSource>> source)
        {
            using (var enumerator = source.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                    yield break;

                var set = new HashSet<TSource>(enumerator.Current);
                while (enumerator.MoveNext())
                    set.IntersectWith(enumerator.Current);
                foreach (var item in set)
                    yield return item;
            }
        }
    }
}
