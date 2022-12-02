using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace adventofcode
{
    public static class Day24
    {
        private static Stopwatch stopWatch = new Stopwatch();

        private const char NORTH = 'n';
        private const char SOUTH = 's';
        private const char EAST = 'e';
        private const char WEST = 'w';

        public static void PartOne(string[] fileLines)
        {
            stopWatch.Restart();
            Console.WriteLine("========== AdventOfCode Day 24 - Part One ==========");

            var result = flipTiles(fileLines);

            Console.WriteLine($"The number of tiles flipped to black is '{result.Count}'\r\n");
            stopWatch.Stop();
            Console.WriteLine($"=> found in {stopWatch.Elapsed:mm\\:ss\\:ffff}\r\n");
        }

        public static void PartTwo(string[] fileLines)
        {
            stopWatch.Restart();
            Console.WriteLine("========== AdventOfCode Day 24 - Part Two ==========");

            var result = "";

            Console.WriteLine($"{result}.\r\n");
            stopWatch.Stop();
            Console.WriteLine($"=> found in {stopWatch.Elapsed:mm\\:ss\\:ffff}\r\n");
        }

        private static List<(int, int, int)> flipTiles(IEnumerable<string> fileLines)
        {
            var tilesFlippedToBlack = new List<(int, int, int)>();

            foreach (var line in fileLines)
            {
                var tileToFlip = GetTileCoordinates(line);

                if (tilesFlippedToBlack.Any(t => t.Equals(tileToFlip)))
                {
                    tilesFlippedToBlack.Remove(tileToFlip);
                }
                else
                {
                    tilesFlippedToBlack.Add(tileToFlip);
                }
            }

            return tilesFlippedToBlack;
        }

        private static (int, int, int) GetTileCoordinates(string line)
        {
            var x = 0;
            var y = 0;
            var z = 0;                                             

            for (int i = 0; i < line.Length;)
            {
                /// W  -> x-- && y++
                if (line[i] == WEST)
                {
                    x--;
                    y++;

                    /// Next Char
                    i++;
                }
                /// E  -> x++ && y--
                else if (line[i] == EAST)
                {
                    x++;
                    y--;

                    /// Next Char
                    i++;
                }
                else if (line[i] == NORTH)
                {
                    /// Next Char
                    i++;
                    /// NW -> z-- && y++
                    if (line[i] == WEST)
                    {
                        z--;
                        y++;
                    }
                    /// NE -> x++ && z--
                    else if (line[i] == EAST)
                    {
                        x++;
                        z--;
                    }
                    /// Next Char
                    i++;
                }
                else if (line[i] == SOUTH)
                {
                    /// Next Char
                    i++;
                    /// SW -> x-- && z++
                    if (line[i] == WEST)
                    {
                        x--;
                        z++;
                    }
                    /// SE -> y-- && z++
                    else if (line[i] == EAST)
                    {
                        y--;
                        z++;
                    }
                    /// Next Char
                    i++;
                }
            }

            return (x, y, z);
        }
    }
}
