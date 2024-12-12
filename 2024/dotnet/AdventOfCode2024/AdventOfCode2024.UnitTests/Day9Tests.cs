using AzW.AdventOfCode.Year2024;

namespace AdventOfCode2024.UnitTests
{
    public class Day9Tests
    {
        private readonly Day9 _day9;

        public Day9Tests()
        {
            _day9 = new Day9
            {
                Input = "2333133121414131402"
            };
        }

        [Theory]
        [InlineData("12345", "0..111....22222")]
        [InlineData("2333133121414131402", "00...111...2...333.44.5555.6666.777.888899")]
        public void Decompresses_Disk_Map(string diskMap, string expectedDecompressedDiskMap)
        {
            // Arrange

            // Act
            var decompressedDiskMap = _day9.DecompressDiskMap(diskMap);

            // Assert
            Assert.Equal(expectedDecompressedDiskMap, decompressedDiskMap);
        }

        [Theory]
        [InlineData("0..111....22222", "022111222......")]
        [InlineData("00...111...2...333.44.5555.6666.777.888899", "0099811188827773336446555566..............")]
        public void Should_Defrag_Disk(string diskMap, string expectedDefragmentedDiskMap)
        {
            // Arrange

            // Act
            var defragmentedDiskMap = _day9.DefragmentDisk(diskMap);

            // Assert
            Assert.Equal(expectedDefragmentedDiskMap, defragmentedDiskMap);
        }

        [Theory]
        [InlineData("0099811188827773336446555566..............", 1928)]
        public void Should_Calculate_Filesystem_Checksum(string diskMap, long expectedChecksum)
        {
            // Arrange

            // Act
            var checksum = _day9.CalculateChecksum(diskMap);

            // Assert
            Assert.Equal(expectedChecksum, checksum);
        }
    }
}
