using AzW.AdventOfCode.Year2024;

namespace AdventOfCode2024.UnitTests
{
    public class Day8Tests
    {
        private static readonly string[] testSample1 = ["..........", "..........", "..........", "....a.....", "..........", ".....a....", "..........", "..........", "..........", ".........."];
        private readonly Day8 _day8;

        public Day8Tests()
        {
            _day8 = new Day8
            {
                Input =
                [
                    "............",
                    "........0...",
                    ".....0......",
                    ".......0....",
                    "....0.......",
                    "......A.....",
                    "............",
                    "............",
                    "........A...",
                    ".........A..",
                    "............",
                    "............"
                ]
            };
        }

        [Fact]
        public void Should_Get_All_Antenna_Types()
        {
            // Arrange
            //_day8.Input =
            //[
            //    "..........",
            //    "..........",
            //    "..........",
            //    "....a.....",
            //    "..........",
            //    ".....a....",
            //    "..........",
            //    "..........",
            //    "..........",
            //    ".........."
            //];

            // Act
            var actualAntennaTypes = _day8.GetAntennaTypes();

            // Assert
            Assert.Equal(2, actualAntennaTypes.Length);
            Assert.Contains('0', actualAntennaTypes);
            Assert.Contains('A', actualAntennaTypes);
        }

        [Fact]
        public void Should_Get_Antenna_Locations()
        {
            // Arrange
            _day8.Input =
            [
                "..........",
                "..........",
                "..........",
                "....a.....",
                "..........",
                ".....a....",
                "..........",
                "..........",
                "..........",
                ".........."
            ];

            // Act
            var locations = _day8.GetAntennaLocations('a');

            var actualFirstLocation = locations[0];
            var actualSecondLocation = locations[1];

            // Assert
            Assert.Equal(new Coordinate(4, 3), actualFirstLocation);
            Assert.Equal(new Coordinate(5, 5), actualSecondLocation);
        }

        [Theory]
        [InlineData(new[] { 4, 3 }, new[] { 5, 5 }, new[] { 3, 1 }, new[] { 6, 7 })]
        [InlineData(new[] { 4, 3 }, new[] { 8, 4 }, new[] { 0, 2 }, new[] { 12, 5 })]
        [InlineData(new[] { 5, 5 }, new[] { 8, 4 }, new[] { 2, 6 }, new[] { 11, 3 })]

        [InlineData(new[] { 8, 1 }, new[] { 5, 2 }, new[] { 11, 0 }, new[] { 2, 3 })]
        [InlineData(new[] { 8, 1 }, new[] { 7, 3 }, new[] { 9, -1 }, new[] { 6, 5 })]
        [InlineData(new[] { 8, 1 }, new[] { 4, 4 }, new[] { 12, -2 }, new[] { 0, 7 })]

        [InlineData(new[] { 5, 2 }, new[] { 7, 3 }, new[] { 3, 1 }, new[] { 9, 4 })]
        [InlineData(new[] { 5, 2 }, new[] { 4, 4 }, new[] { 6, 0 }, new[] { 3, 6 })]

        [InlineData(new[] { 7, 3 }, new[] { 4, 4 }, new[] { 10, 2 }, new[] { 1, 5 })]
        public void Should_Calculate_Antinodes(int[] firstAntenna, int[] secondAntenna, int[] expectedFirstAntinode, int[] expectedSecondAntinode)
        {
            // Arrange
            var firstAntennaLocation = new Coordinate(firstAntenna[0], firstAntenna[1]);
            var secondAntennaLocation = new Coordinate(secondAntenna[0], secondAntenna[1]);

            // Act
            var antinodes = _day8.CalculateAntinodePositions(firstAntennaLocation, secondAntennaLocation);
            var actualFirstAntinode = antinodes[0];
            var actualSecondAntinode = antinodes[1];

            // Assert
            Assert.Equal(new Coordinate(expectedFirstAntinode[0], expectedFirstAntinode[1]), actualFirstAntinode);
            Assert.Equal(new Coordinate(expectedSecondAntinode[0], expectedSecondAntinode[1]), actualSecondAntinode);
        }

        [Fact]
        public void Should_Calculate_Antinodes_With_Resonant_Harmonics()
        {
            // Arrange
            // Act

            // Assert

        }

        [Fact]
        public void Should_Get_Sum_Of_Unique_Antinodes_When_Executing_Part_1()
        {
            // Arrange

            // Act
            var sumOfUniqueAntinodes = _day8.ExecutePart1();

            // Assert
            Assert.Equal(14, sumOfUniqueAntinodes);
        }

        //[Fact]
        //public void Should_Get_Sum_Of_Unique_Antinodes_When_Executing_Part_2()
        //{
        //    // Arrange

        //    // Act
        //    var sumOfUniqueAntinodes = _day8.ExecutePart2();

        //    // Assert
        //    Assert.Equal(34, sumOfUniqueAntinodes);
        //}
    }
}
