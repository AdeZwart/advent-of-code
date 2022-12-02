using AzW.AdventOfCode2021.Year2021;
using Xunit;

namespace AdventOfCode2021.UnitTests
{
    public class Day18Tests
    {        
        [Theory]
        [InlineData(new string[] 
            { 
                "[1,2]", 
                "[[3,4],5]" 
            }, 143L)]
        [InlineData(new string[]
            {
                "[[[0,[4,5]],[0,0]],[[[4,5],[2,6]],[9,5]]]",
                "[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]",
                "[[2,[[0,8],[3,4]]],[[[6,7],1],[7,[1,6]]]]",
                "[[[[2,4],7],[6,[0,5]]],[[[6,8],[2,8]],[[2,1],[4,5]]]]",
                "[7,[5,[[3,8],[1,4]]]]",
                "[[2,[2,2]],[8,[8,1]]]",
                "[2,9]",
                "[1,[[[9,3],9],[[9,0],[0,7]]]]",
                "[[[5,[7,4]],7],1]",
                "[[[[4,2],2],6],[8,7]]"
            }, 3488L)]
        [InlineData(new string[]
            {
                "[[[0,[5,8]],[[1,7],[9,6]]],[[4,[1,2]],[[1,4],2]]]",
                "[[[5,[2,8]],4],[5,[[9,9],0]]]",
                "[6,[[[6,2],[5,6]],[[7,6],[4,7]]]]",
                "[[[6,[0,7]],[0,9]],[4,[9,[9,0]]]]",
                "[[[7,[6,4]],[3,[1,3]]],[[[5,5],1],9]]",
                "[[6,[[7,3],[3,2]]],[[[3,8],[5,7]],4]]",
                "[[[[5,4],[7,7]],8],[[8,3],8]]",
                "[[9,3],[[9,9],[6,[4,9]]]]",
                "[[2,[[7,7],7]],[[5,8],[[9,3],[0,2]]]]",
                "[[[[5,2],5],[8,[3,7]]],[[5,[7,5]],[4,4]]]"
            }, 4140L)]
        public void ExecutePart1(string[] input, long expectedResult)
        {
            // Arrange
            var day18 = new Day18();
            day18.Input = input;

            // Act
            var result = day18.ExecutePart1();

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(new string[]
        {
            "[1,1]",
            "[2,2]",
            "[3,3]",
            "[4,4]"
        }, "[[[[1,1],[2,2]],[3,3]],[4,4]]")]
        [InlineData(new string[]
        {
            "[1,1]",
            "[2,2]",
            "[3,3]",
            "[4,4]",
            "[5,5]"
        }, "[[[[3,0],[5,3]],[4,4]],[5,5]]")]
        [InlineData(new string[]
        {
            "[1,1]",
            "[2,2]",
            "[3,3]",
            "[4,4]",
            "[5,5]",
            "[6,6]"
        }, "[[[[5,0],[7,4]],[5,5]],[6,6]]")]
        [InlineData(new string[]
            {
                "[[[0,[4,5]],[0,0]],[[[4,5],[2,6]],[9,5]]]",
                "[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]",
                "[[2,[[0,8],[3,4]]],[[[6,7],1],[7,[1,6]]]]",
                "[[[[2,4],7],[6,[0,5]]],[[[6,8],[2,8]],[[2,1],[4,5]]]]",
                "[7,[5,[[3,8],[1,4]]]]",
                "[[2,[2,2]],[8,[8,1]]]",
                "[2,9]",
                "[1,[[[9,3],9],[[9,0],[0,7]]]]",
                "[[[5,[7,4]],7],1]",
                "[[[[4,2],2],6],[8,7]]"
            }, "[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]")]
        public void CalculateSumReturnExpectedValue(string[] input, string expectedResult)
        {
            // Arrange
            var day18 = new Day18();

            // Act
            var result = day18.CalculateSum(input);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("[[[[[4,3],4],4],[7,[[8,4],9]]],[1,1]]", "[[[[0,7],4],[[7,8],[6,0]]],[8,1]]")]
        [InlineData("[[[[0,[4,5]],[0,0]],[[[4,5],[2,6]],[9,5]]],[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]]", "[[[[4,0],[5,4]],[[7,7],[6,0]]],[[8,[7,7]],[[7,9],[5,0]]]]")]
        [InlineData("[[[[[4,0],[5,4]],[[7,7],[6,0]]],[[8,[7,7]],[[7,9],[5,0]]]],[[2,[[0,8],[3,4]]],[[[6,7],1],[7,[1,6]]]]]", "[[[[6,7],[6,7]],[[7,7],[0,7]]],[[[8,7],[7,7]],[[8,8],[8,0]]]]")]
        [InlineData("[[[[[6,7],[6,7]],[[7,7],[0,7]]],[[[8,7],[7,7]],[[8,8],[8,0]]]],[[[[2,4],7],[6,[0,5]]],[[[6,8],[2,8]],[[2,1],[4,5]]]]]", "[[[[7,0],[7,7]],[[7,7],[7,8]]],[[[7,7],[8,8]],[[7,7],[8,7]]]]")]
        [InlineData("[[[[[7,0],[7,7]],[[7,7],[7,8]]],[[[7,7],[8,8]],[[7,7],[8,7]]]],[7,[5,[[3,8],[1,4]]]]]", "[[[[7,7],[7,8]],[[9,5],[8,7]]],[[[6,8],[0,8]],[[9,9],[9,0]]]]")]
        [InlineData("[[[[[7,7],[7,8]],[[9,5],[8,7]]],[[[6,8],[0,8]],[[9,9],[9,0]]]],[[2,[2,2]],[8,[8,1]]]]", "[[[[6,6],[6,6]],[[6,0],[6,7]]],[[[7,7],[8,9]],[8,[8,1]]]]")]
        [InlineData("[[[[[6,6],[6,6]],[[6,0],[6,7]]],[[[7,7],[8,9]],[8,[8,1]]]],[2,9]]", "[[[[6,6],[7,7]],[[0,7],[7,7]]],[[[5,5],[5,6]],9]]")]
        public void ReduceSnailfishNumberShouldReturnExpectedValue(string snailfishNumber, string expectedResult)
        {
            // Assert
            var day18 = new Day18();

            // Act
            var result = day18.ReduceSnailfishNumber(snailfishNumber);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("[[[[[9,8],1],2],3],4]", "[[[[0,9],2],3],4]")]
        [InlineData("[7,[6,[5,[4,[3,2]]]]]", "[7,[6,[5,[7,0]]]]")]
        [InlineData("[[6,[5,[4,[3,2]]]],1]", "[[6,[5,[7,0]]],3]")]
        [InlineData("[[3,[2,[1,[7,3]]]],[6,[5,[4,[3,2]]]]]", "[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]")]
        [InlineData("[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]", "[[3,[2,[8,0]]],[9,[5,[7,0]]]]")]
        [InlineData("[[[[0,7],4],[7,[[8,4],9]]],[1,1]]", "[[[[0,7],4],[15,[0,13]]],[1,1]]")]
        [InlineData("[[[[0,7],4],[[7,8],[0,13]]],[1,1]]", "[[[[0,7],4],[[7,8],[0,13]]],[1,1]]")]
        public void ExplodeShouldReturnExpectedValue(string snailfishNumber, string expectedResult)
        {
            // Arrange
            var day18 = new Day18();

            // Act
            var result = day18.Explode(snailfishNumber);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("[10,0]","[[5,5],0]")]
        [InlineData("[11,0]", "[[5,6],0]")]
        [InlineData("[0,12]", "[0,[6,6]]")]
        [InlineData("[0,13]", "[0,[6,7]]")]
        [InlineData("[[[[0,7],4],[15,[0,13]]],[1,1]]", "[[[[0,7],4],[[7,8],[0,13]]],[1,1]]")]
        [InlineData("[[[[0,7],4],[7,[[8,4],9]]],[1,1]]", "[[[[0,7],4],[7,[[8,4],9]]],[1,1]]")]        
        public void SplitShouldReturnExpectedValue(string snailfishNumber, string expectedResult)
        {
            // Arrange
            var day18 = new Day18();

            // Act
            var result = day18.Split(snailfishNumber);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("[[1,2],[[3,4],5]]", 143L)]
        [InlineData("[[[[0,7],4],[[7,8],[6,0]]],[8,1]]", 1384L)]
        [InlineData("[[[[1,1],[2,2]],[3,3]],[4,4]]", 445L)]
        [InlineData("[[[[3,0],[5,3]],[4,4]],[5,5]]", 791L)]
        [InlineData("[[[[5,0],[7,4]],[5,5]],[6,6]]", 1137L)]
        [InlineData("[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]", 3488L)]
        [InlineData("[[[[6,6],[7,6]],[[7,7],[7,0]]],[[[7,7],[7,7]],[[7,8],[9,9]]]]", 4140L)]
        public void CalculateMagnitudeShouldReturnExpectedValue(string snailfishNumber, long expectedResult)
        {
            // Arrange
            var day18 = new Day18();

            // Act
            var result = day18.CalculateMagnitude(snailfishNumber);

            // Assert
            Assert.Equal(expectedResult, result);
        }
    }
}