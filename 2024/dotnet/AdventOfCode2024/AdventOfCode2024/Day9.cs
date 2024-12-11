namespace AzW.AdventOfCode.Year2024
{
    public class Day9 : Day
    {
        public string DecompressDiskMap(string diskMap)
        {
            var decompressedDiskMap = string.Empty;
            var currentFileId = 0;
            foreach (var index in Enumerable.Range(0, diskMap.Length - 1))
            {
                if (int.IsEvenInteger(index))
                {
                    // Indicates file length
                    var len = diskMap[index];
                    
                }
                else
                {
                    // Indicates free space length
                }
            }

            return string.Empty;
        }

        public override object ExecutePart1()
        {
            return base.ExecutePart1();
        }

        public override object ExecutePart2()
        {
            return base.ExecutePart2();
        }
    }
}
