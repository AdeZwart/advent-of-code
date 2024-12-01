
using AzW.AdventOfCode.Year2024;

namespace AdventOfCode2024.UnitTests
{
    public class Day1Tests
    {
        private readonly Day1 _day1;

        public Day1Tests()
        {
            _day1 = new Day1();
            _day1.Input =
            [
                "3   4",
                "4   3",
                "2   5",
                "1   3",
                "3   9",
                "3   3"
            ];
        }

        [Theory]
        [InlineData("3   4", 3, 4)]
        [InlineData("4   3", 4, 3)]
        [InlineData("2   5", 2, 5)]
        public void Should_Convert_Input_To_Left_And_Right_Int(string input, int expectedLeft, int expectedRight)
        {
            // Arrange

            // Act
            (var actualLeft, var actualRight) = _day1.ParseLocationIds(input);

            // Assert
            Assert.Equal(expectedLeft, actualLeft);
            Assert.Equal(expectedRight, actualRight);
        }

        [Fact]
        public void Should_Parse_Input_To_Id_Lists()
        {
            // Arrange
            var input = new[]
            {
                "3   4",
                "4   3",
                "2   5",
                "1   3",
                "3   9",
                "3   3"
            };

            // Act
            var (leftList, rightList) = _day1.ParseLocationIdLists(input);

            // Assert
            Assert.Equal(new[] { 3, 4, 2, 1, 3, 3 }, leftList);
            Assert.Equal(new[] { 4, 3, 5, 3, 9, 3 }, rightList);
        }

        [Theory]
        [InlineData(new[] { 3, 4, 2, 1, 3, 3 }, new[] { 1, 2, 3, 3, 3, 4 })]
        [InlineData(new[] { 4, 3, 5, 3, 9, 3 }, new[] { 3, 3, 3, 4, 5, 9 })]
        public void Should_Sort_List_Small_To_Large(IEnumerable<int> input, IEnumerable<int> expectedResult)
        {
            // Arrange

            // Act
            var result = _day1.SortLocationIds(input);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Should_Calculate_Total_Distance()
        {
            // Arrange
            var leftList = new List<int> { 1, 2, 3, 3, 3, 4 };
            var rightList = new List<int> { 3, 3, 3, 4, 5, 9 };
            var expectedTotal = 11;

            // Act
            var actualTotal = _day1.CalculateTotalDistance(leftList, rightList);

            // Assert
            Assert.Equal(expectedTotal, actualTotal);
        }

        [Theory]
        [InlineData(1, 3, 2)]
        [InlineData(2, 3, 1)]
        [InlineData(3, 3, 0)]
        [InlineData(9, 5, 4)]
        public void Should_Calculate_Distance(int left, int right, int expectedDistance)
        {
            // Arrange

            // Act
            var distance = _day1.CalculateDistance(left, right);

            // Assert
            Assert.Equal(expectedDistance, distance);
        }

        [Fact]
        public void Should_Calculate_Total_Similarity_Score()
        {
            // Arrange
            var expectedSimilarityScore = 31;

            // Act
            var similarityScore = _day1.ExecutePart2();

            // Assert
            Assert.Equal(expectedSimilarityScore, similarityScore);
        }

        [Theory]
        [InlineData(3, 9)]
        [InlineData(4, 4)]
        [InlineData(2, 0)]
        [InlineData(1, 0)]
        public void Should_Calculate_Similarity_Score(int Id, int expectedSimilarityScore)
        {
            // Arrange
            var rightList = new List<int> { 4, 3, 5, 3, 9, 3 };

            // Act
            var similarityScore = _day1.CalculateSimilarityScore(Id, rightList);

            // Assert
            Assert.Equal(expectedSimilarityScore, similarityScore);
        }
    }
}