using System;

namespace adventofcode
{
    public static class Day3
    {
        public static void PartOne(string[] fileLines)
        {
            Console.WriteLine("========== AdventOfCode Day 3 - Part One ==========");

            var treeCount = TraverseSlopes(fileLines, 3, 1);

            Console.WriteLine($"Part one answer: {treeCount}");
        }

        public static void PartTwo(string[] fileLines)
        {
            Console.WriteLine("========== AdventOfCode Day 3 - Part Two ==========");

            var result1 = TraverseSlopes(fileLines, 1, 1);
            var result2 = TraverseSlopes(fileLines, 3, 1);
            var result3 = TraverseSlopes(fileLines, 5, 1);
            var result4 = TraverseSlopes(fileLines, 7, 1);
            var result5 = TraverseSlopes(fileLines, 1, 2);

            Console.WriteLine($"Part two anser: {result1 * result2 * result3 * result4 * result5}");
        }

        private static int TraverseSlopes(string[] fileLines, int rightStepSize, int downStepSize)
        {
            var treeCount = 0;            
            var goRight = 0;

            int i = 0;
            while (i < fileLines.Length)
            {
                if (i == 0)
                {
                    i += downStepSize;
                    continue;
                }

                var line = fileLines[i];

                goRight += rightStepSize;
                /// These aren't the only trees, though; 
                /// due to something you read about once involving arboreal genetics and biome stability, 
                /// the same pattern repeats to the right many times                
                goRight = (goRight > (line.Length - 1)) ? goRight - line.Length : goRight;

                var position = line.Substring(goRight, 1);
                if (position.Equals("#"))
                {
                    treeCount++;                    
                }

                i += downStepSize;
            }

            Console.WriteLine($"{treeCount} trees encountered while following slope of right {rightStepSize} and down {downStepSize}.\r\n");

            return treeCount;
        }
    }
}
