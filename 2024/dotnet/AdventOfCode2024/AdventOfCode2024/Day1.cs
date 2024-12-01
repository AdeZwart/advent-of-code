using System.Text.RegularExpressions;

namespace AzW.AdventOfCode.Year2024
{
    [Day(2024, 1)]
    public class Day1 : Day.NewLineSplitParsed<string>
    {
        public override object ExecutePart1()
        {
            (var leftList, var rightList) = ParseLocationIdLists(Input);

            var sortedLeftList = SortLocationIds(leftList);
            var sortedRightList = SortLocationIds(rightList);

            var sumOfDistance = CalculateTotalDistance(sortedLeftList, sortedRightList);

            return sumOfDistance;
        }

        public override object ExecutePart2()
        {
            (var leftList, var rightList) = ParseLocationIdLists(Input);

            var sortedLeftList = SortLocationIds(leftList);
            var sortedRightList = SortLocationIds(rightList);

            var totalSimilarityScore = 0;
            foreach(var id in sortedLeftList)
            {
                var similarityScore = CalculateSimilarityScore(id, sortedRightList);
                totalSimilarityScore += similarityScore;
            }

            return totalSimilarityScore;
        }

        public int CalculateTotalDistance(List<int> leftList, List<int> rightList)
        {
            List<int> distances = [];

            foreach (var index in Enumerable.Range(0, leftList.Count()))
            {
                distances.Add(CalculateDistance(leftList[index], rightList[index]));
            }

            return distances.Sum();
        }

        public (int, int) ParseLocationIds(string input)
        {
            var splittedResult = Regex.Split(input, @"\s+");
            var leftValue = splittedResult[0].Trim();
            var rightValue = splittedResult[1].Trim();

            return (int.Parse(leftValue), int.Parse(rightValue));
        }

        public (IEnumerable<int>, IEnumerable<int>) ParseLocationIdLists(string[] input)
        {
            var leftList = new List<int>();
            var rightList = new List<int>();

            foreach (var entry in input)
            {
                (var left, var right) = ParseLocationIds(entry);

                leftList.Add(left);
                rightList.Add(right);
            }

            return (leftList, rightList);
        }

        public List<int> SortLocationIds(IEnumerable<int> input)
        {
            return input.ToList().OrderBy((x) => x).ToList();
        }

        public int CalculateDistance(int left, int right)
        {
            var distance = right - left;

            return (distance < 0) ? distance * -1 : distance;
        }

        public int CalculateSimilarityScore(int id, List<int> rightList)
        {
            var occurences = rightList.Count(x => x == id);

            return id * occurences;
        }
    }
}
