using System;
using System.Collections.Generic;
using System.Linq;

namespace adventofcode
{
    public static class Day12
    {
        private const char NORTH = 'N';
        private const char SOUTH = 'S';
        private const char EAST = 'E';
        private const char WEST = 'W';
        private const char LEFT = 'L';
        private const char RIGHT = 'R';
        private const char FORWARD = 'F';

        private static int currentDirection;

        public static void PartOne(string[] fileLines)
        {
            Console.WriteLine("========== AdventOfCode Day 12 - Part One ==========");

            var location = new Tuple<int, int>(0, 0);
            currentDirection = 90;
            foreach (var line in fileLines)
            {
                location = Navigate(location, line);
                Console.WriteLine($"{line} => {currentDirection} => {location.Item1},{location.Item2}");
            }

            var item1 = (location.Item1 < 0) ? location.Item1 * -1 : location.Item1;
            var item2 = (location.Item2 < 0) ? location.Item2 * -1 : location.Item2;

            Console.Write($"The manhattan distance is {item1 + item2}.\r\n");
        }

        public static void PartTwo(string[] fileLines)
        {
            Console.WriteLine("========== AdventOfCode Day 12 - Part Two ==========");

            var location = new Tuple<int, int>(0, 0);
            currentDirection = 90;

            foreach (var line in fileLines)
            {
                location = Navigate(location, line);
                Console.WriteLine($"{line} => {currentDirection} => {location.Item1},{location.Item2}");
            }

            var item1 = (location.Item1 < 0) ? location.Item1 * -1 : location.Item1;
            var item2 = (location.Item2 < 0) ? location.Item2 * -1 : location.Item2;

            Console.Write($"The manhattan distance is {item1 + item2}.\r\n");
        }

        private static Tuple<int, int> Navigate(Tuple<int, int> location, string directions)
        {
            var lat = location.Item1;
            var lon = location.Item2;

            int.TryParse(directions.Substring(1), out var value);
            var action = directions.First();
            switch (action)
            {
                case NORTH:
                    lon -= value;
                    break;
                
                case SOUTH:
                    lon += value;
                    break;
                
                case WEST:
                    lat -= value;
                    break;
                
                case EAST:
                    lat += value;
                    break;
                
                case LEFT:
                    currentDirection -= value;
                    currentDirection = (currentDirection < 0) ? currentDirection + 360 : currentDirection;
                    break;
                
                case RIGHT:
                    currentDirection += value;
                    currentDirection = (currentDirection > 360) ? currentDirection - 360 : currentDirection;
                    break;
                
                case FORWARD:
                    var a = string.Empty;
                    switch (currentDirection)
                    {
                        case 90:
                            a = EAST.ToString();
                            break;
                        case 180:
                            a = SOUTH.ToString();
                            break;
                        case 270:
                            a = WEST.ToString();
                            break;
                        case 0:
                        case 360:
                            a = NORTH.ToString();
                            break;
                    }
                    var nav = Navigate(location, $"{a}{value}");
                    lat = nav.Item1;
                    lon = nav.Item2;
                    break;
            }

            return new Tuple<int, int>(lat, lon);
        }
    }
}
