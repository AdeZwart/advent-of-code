using AzW.AdventOfCode.Year2024;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
