using System;
using System.IO;

namespace adventofcode
{
    class Program
    {
        private const string PUZZLE = "puzzle";
        private const string INPUTTXT = "_input.txt";

        static void Main(string[] args)
        {
            var directory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
            var inputFiles = Directory.GetFiles(directory, $"*{INPUTTXT}");

            Console.Write($"Enter a number between 1 and {inputFiles.Length} to solve the puzzle for that day:\r\n");
            if (int.TryParse(Console.ReadLine(), out var puzzleIndex) && puzzleIndex > 0 && puzzleIndex <= inputFiles.Length)
            {
                var inputFile = $"{PUZZLE}{puzzleIndex}{INPUTTXT}";
                var lines = File.ReadAllLines($"{directory}\\{inputFile}");

                switch (puzzleIndex)
                {
                    case 1:
                        Console.WriteLine("Puzzle 1 was not implemented\r\n");
                        break;

                    case 2:
                        Day2.FindValidPasswordsPartOne(lines);
                        Day2.FindValidPasswordsPartTwo(lines);
                        break;

                    case 3:
                        Day3.PartOne(lines);
                        Day3.PartTwo(lines);
                        break;

                    case 4:
                        Day4.PartOne(lines);
                        Day4.PartTwo(lines);
                        break;

                    case 5:
                        Day5.PartOne(lines);
                        Day5.PartTwo(lines);
                        break;

                    case 6:
                        Day6.PartOne(lines);
                        Day6.PartTwo(lines);
                        break;

                    case 7:
                        Day7.PartOne(lines);
                        Day7.PartTwo(lines);
                        break;

                    case 8:
                        Day8.PartOne(lines);
                        Day8.PartTwo(lines);
                        break;

                    case 9:
                        Day9.PartOne(lines);
                        Day9.PartTwo(lines);
                        break;

                    case 10:
                        Day10.PartOne(lines);
                        Day10.PartTwo(lines);
                        break;

                    case 11:
                        Day11.PartOne(lines);
                        Day11.PartTwo(lines);
                        break;

                    case 12:
                        Day12.PartOne(lines);
                        Day12.PartTwo(lines);
                        break;

                    case 13:
                        Day13.PartOne(lines);
                        Day13.PartTwo(lines);
                        break;

                    case 14:
                        Day14.PartOne(lines);
                        Day14.PartTwo(lines);
                        break;

                    case 15:
                        Day15.PartOne(lines);
                        Day15.PartTwo(lines);
                        break;

                    case 16:
                        Day16.PartOne(lines);
                        Day16.PartTwo(lines);
                        break;

                    case 17:
                        Day17.PartOne(lines);
                        Day17.PartTwo(lines);
                        break;

                    case 18:
                        Day18.PartOne(lines);
                        Day18.PartTwo(lines);
                        break;

                    case 19:
                        Day19.PartOne(lines);
                        Day19.PartTwo(lines);
                        break;

                    case 20:
                        Day20.PartOne(lines);
                        Day20.PartTwo(lines);
                        break;

                    case 21:
                        Day21.PartOne(lines);
                        Day21.PartTwo(lines);
                        break;

                    case 22:
                        Day22.PartOne(lines);
                        Day22.PartTwo(lines);
                        break;

                    case 23:
                        Day23.PartOne(lines);
                        Day23.PartTwo(lines);
                        break;

                    case 24:
                        Day24.PartOne(lines);
                        Day24.PartTwo(lines);
                        break;

                    case 25:
                        Day25.PartOne(lines);
                        Day25.PartTwo(lines);
                        break;

                    default:
                        Console.WriteLine("An invalid puzzle number was selected.\r\n");
                        break;
                }           
            }

            Console.WriteLine("\nFINISHED");
            Console.ReadLine();
        }


    }
}
