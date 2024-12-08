using AzW.AdventOfCode.Year2024;

namespace AdventOfCode2024.UnitTests
{
    public class Day2Tests
    {
        private readonly Day2 _day2;

        public Day2Tests()
        {
            _day2 = new Day2
            {
                Input =
                [
                    "7 6 4 2 1",
                    "1 2 7 8 9",
                    "9 7 6 2 1",
                    "1 3 2 4 5",
                    "8 6 4 4 1",
                    "1 3 6 7 9",
                ]
            };
        }

        [Theory]
        [InlineData("7 6 4 2 1", new[] { 7, 6, 4, 2, 1 })]
        [InlineData("1 2 7 8 9", new[] { 1, 2, 7, 8, 9 })]
        public void Should_Extract_Levels_From_Report(string input, int[] expectedReport)
        {
            // Arrange

            // Act
            var actualReport = _day2.ParseReport(input);

            // Assert
            Assert.Equal(expectedReport, actualReport);
        }

        [Theory]
        [InlineData(new[] { 7, 6, 4, 2, 1 }, true)]
        [InlineData(new[] { 1, 2, 7, 8, 9 }, false)]
        [InlineData(new[] { 9, 7, 6, 2, 1 }, false)]
        [InlineData(new[] { 1, 3, 2, 4, 5 }, false)]
        [InlineData(new[] { 5, 4, 2, 3, 1 }, false)]
        [InlineData(new[] { 8, 6, 4, 4, 1 }, false)]
        [InlineData(new[] { 1, 3, 6, 7, 9 }, true)]
        public void Should_Determine_If_Report_Is_Safe(int[] input, bool expectedResult)
        {
            // Arrange

            // Act
            var isSafe = _day2.IsReportSafe(input);

            // Assert
            Assert.Equal(expectedResult, isSafe);
        }

        [Theory]
        [InlineData(new[] { 7, 6, 4, 2, 1 }, true)]
        [InlineData(new[] { 1, 2, 7, 8, 9 }, false)]
        [InlineData(new[] { 9, 7, 6, 2, 1 }, false)]
        [InlineData(new[] { 1, 3, 2, 4, 5 }, true)]
        [InlineData(new[] { 5, 4, 2, 3, 1 }, true)]
        [InlineData(new[] { 8, 6, 4, 4, 1 }, true)]
        [InlineData(new[] { 1, 3, 6, 7, 9 }, true)]
        [InlineData(new[] { 52, 52, 53, 56, 58, 59}, true)]
        [InlineData(new[] { 40, 42, 44, 47, 49, 50, 48 }, true)]
        [InlineData(new[] { 74, 76, 78, 81, 83, 85, 87, 91 }, true)]
        public void Should_Determine_If_Dampened_Report_Is_Safe(int[] input, bool expectedResult)
        {
            // Arrange
            bool isProblemDampenerEnabled = true;

            // Act
            var isSafe = _day2.IsReportSafe(input, isProblemDampenerEnabled);

            // Assert
            Assert.Equal(expectedResult, isSafe);
        }

        [Fact]
        public void Should_Get_Total_Number_Of_Safe_Reports()
        {
            // Arrange
            var expectedResult = 2;

            // Act
            var result = _day2.ExecutePart1();

            // Assert
            Assert.Equal(expectedResult, result);
        }
    }

}
