using AzW.AdventOfCode.Year2024;

namespace AdventOfCode2024.UnitTests
{
    public class Day11Tests
    {
        private readonly Day11 _day11;

        public Day11Tests()
        {
            _day11 = new Day11
            {
                Input = "0 1 10 99 999"
            };
        }

        [Fact]
        public void Should_parse_raw_data_input_to_list_of_int()
        {
            // arrange
            var expectedResult = new List<long>() { 0, 1, 10, 99, 999 };

            // act
            var result = _day11.ParseInput();

            // assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(0, 1, null)]
        [InlineData(1, 2024, null)]
        [InlineData(10, 1, 0)]
        [InlineData(99, 9, 9)]
        [InlineData(999, 2021976, null)]
        public void Should_mutate_stone(int input, int expectedResult1, int? expectedResult2)
        {
            // arrange

            // act
            var (result1, result2) = _day11.MutateStone(input);

            // assert
            Assert.Equal(expectedResult1, result1);
            Assert.Equal(expectedResult2, result2);
        }

        [Fact]
        public void Should_update_all_stones_when_blinking()
        {
            // arrange
            var expectedResult = new List<long>() { 1, 2024, 1, 0, 9, 9, 2021976 };
            var input = _day11.ParseInput();

            // act
            var result = _day11.Blink(input);

            // assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("125 17", 1, new long[] { 253000, 1, 7 })]
        [InlineData("125 17", 2, new long[] { 253, 0, 2024, 14168 })]
        [InlineData("125 17", 3, new long[] { 512072, 1, 20, 24, 28676032 })]
        [InlineData("125 17", 6, new long[] { 2097446912, 14168, 4048, 2, 0, 2, 4, 40, 48, 2024, 40, 48, 80, 96, 2, 8, 6, 7, 6, 0, 3, 2 })]
        public void Should_update_according_to_number_of_blinks(string rawInput, int numberOfBlinks, IEnumerable<long> expectedStones)
        {
            // arrange
            _day11.Input = rawInput;
            var stones = _day11.ParseInput();

            // act
            foreach (var index in Enumerable.Range(1, numberOfBlinks))
            {
                stones = _day11.Blink(stones);
            }

            // assert
            Assert.Equal(expectedStones, stones);
        }

        [Fact]
        public void Should_count_stones_after_25_blinks()
        {
            // arrange
            _day11.Input = "125 17";
            var stones = _day11.ParseInput();

            // act
            foreach (var index in Enumerable.Range(1, 25))
            {
                stones = _day11.Blink(stones);
            }

            // assert
            Assert.Equal(55312, stones.Count());
        }

        [Fact]
        public void Should_Execute_Part_1()
        {
            // arrange
            _day11.Input = "5688 62084 2 3248809 179 79 0 172169";

            // act
            var stoneCount = _day11.ExecutePart1();

            // assert
            Assert.Equal(186175, stoneCount);
        }
    }
}
