using System.Text.RegularExpressions;

namespace AzW.AdventOfCode.Year2024
{
    public partial class Day9 : Day
    {
        public string DecompressDiskMap(string diskMap)
        {
            var decompressedDiskMap = new List<string>();
            var currentFileId = 0;

            foreach (var index in Enumerable.Range(0, diskMap.Length))
            {
                if (int.IsEvenInteger(index))
                {
                    // Indicates file length
                    var fileLen = (int)char.GetNumericValue(diskMap[index]);
                    foreach (var _ in Enumerable.Range(0, fileLen))
                    {
                        decompressedDiskMap.Add(currentFileId.ToString());
                    }
                    currentFileId++;
                }
                else
                {
                    // Indicates free space length
                    var freeSpaceLen = (int)char.GetNumericValue(diskMap[index]);
                    foreach (var _ in (Enumerable.Range(0, freeSpaceLen)))
                    {
                        decompressedDiskMap.Add(".");
                    }
                }
            }

            return string.Join("", decompressedDiskMap);
        }

        public string DefragmentDisk(string decompressedDiskMap)
        {
            var map = decompressedDiskMap.ToCharArray();
            foreach (var index in Enumerable.Range(0, map.Length - 1))
            {
                if (map[index] == '.')
                {
                    var match = DefragRegex().Match(string.Join("", map));
                    if (match.Index > index)
                    {
                        map[index] = map[match.Index];
                        map[match.Index] = '.';
                    }
                }
            }

            return string.Join("", map);
        }

        public override object ExecutePart1()
        {
            var decompressedDiskMap = DecompressDiskMap(Input);

            var defragmentedDisk = DefragmentDisk(decompressedDiskMap);

            var checksum = CalculateChecksum(defragmentedDisk);

            return checksum;
        }

        public override object ExecutePart2()
        {
            return base.ExecutePart2();
        }

        [GeneratedRegex(@"[0-9]", RegexOptions.RightToLeft)]
        private static partial Regex DefragRegex();

        public long CalculateChecksum(string diskMap)
        {
            var checksum = 0;

            foreach (var index in Enumerable.Range(0, diskMap.Length - 1))
            {
                if (char.IsDigit(diskMap[index]))
                {
                    var id = (int)char.GetNumericValue(diskMap[index]);
                    checksum += (id * index);
                }
            }

            return checksum;
        }
    }
}
