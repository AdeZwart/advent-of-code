using System.Drawing;

namespace AzW.AdventOfCode.Year2023
{
    public class Day18 : Day.NewLineSplitParsed<string>
    {
        private const char LEFT = 'L';
        private const char RIGHT = 'R';
        private const char UP = 'U';
        private const char DOWN = 'D';

        public override object ExecutePart1()
        {
            Input = GetTestInput();

            var instructions = Input.Select(i => GetInstruction(i));

            var lagoon = new HashSet<Coordinate>()
            {
                new(0, 0)
            };

            foreach (var instruction in instructions)
            {
                foreach (var meter in Enumerable.Range(0, instruction.Amount))
                {
                    var coordinate = instruction.Direction switch
                    {
                        RIGHT => new Coordinate(lagoon.Last().X + 1, lagoon.Last().Y),                        
                        LEFT => new Coordinate(lagoon.Last().X - 1, lagoon.Last().Y),
                        UP => new Coordinate(lagoon.Last().X, lagoon.Last().Y - 1),
                        DOWN => new Coordinate(lagoon.Last().X, lagoon.Last().Y + 1),
                        _ => throw new NotImplementedException(),
                    };

                    lagoon.Add(coordinate);
                }
            }

            var pointsInLake = DigInsideOfLagoon([.. lagoon]);

            PrintLagoon(lagoon, pointsInLake);

            var totalCubicMeters = lagoon.Count + pointsInLake.Count;

            Console.WriteLine($"The lagoon is {totalCubicMeters} cubic meters.");

            throw new NotImplementedException();
            return totalCubicMeters;
        }

        private static Instruction GetInstruction(string input)
        {
            var parsedInput = input.Split(' ');

            var instruction = new Instruction()
            {
                Direction = char.Parse(parsedInput[0]),
                Amount = int.Parse(parsedInput[1]),
                Color = ColorTranslator.FromHtml(parsedInput[2].Trim('(').Trim(')'))
            };

            return instruction;
        }



        /// <summary>
        /// Apply Ray Cast algorithm // => shoelace formula instead.
        /// </summary>
        /// <param name="lagoon"></param>
        /// <returns>A list of coordinates inside the trench of the lagoon</returns>
        private static List<Coordinate> DigInsideOfLagoon(List<Coordinate> lagoon)
        {
            var insideOfLagoon = new List<Coordinate>();

            var minX = lagoon.Min(c => c.X);
            var maxX = lagoon.Max(c => c.X);
            var minY = lagoon.Min(c => c.Y);
            var maxY = lagoon.Max(c => c.Y);

            foreach (var y in Enumerable.Range(minY, (maxY - minY) + 1))
            {
                bool isInside = false;
                foreach (var x in Enumerable.Range(minX, (maxX - minX) + 1))
                {
                    var c = new Coordinate(x, y);
                    if (lagoon.Contains(c))
                    {
                        // This point _is_ the edge
                        if (lagoon.Contains(new Coordinate(x - 1, y)))
                        {
                            continue;
                        }
                        isInside = !isInside;
                        continue;
                    }

                    if (isInside)
                    {
                        //Console.WriteLine($"Coordinate ({c.X},{c.Y}) is inside the lagoon");
                        insideOfLagoon.Add(c);
                    }
                }
            }

            return insideOfLagoon;
        }

        private static void PrintLagoon(IEnumerable<Coordinate> lagoon, IEnumerable<Coordinate> insideOfLagoon)
        {
            var minX = lagoon.Min(c => c.X);
            var maxX = lagoon.Max(c => c.X);
            var minY = lagoon.Min(c => c.Y);
            var maxY = lagoon.Max(c => c.Y);

            foreach (var y in Enumerable.Range(minY, (maxY - minY) + 1))
            {
                foreach (var x in Enumerable.Range(minX, (maxX - minX) + 1))
                {
                    var c = new Coordinate(x, y);
                    if (lagoon.Contains(c))
                    {
                        Console.Write('#');
                    }
                    else
                    {
                        if (insideOfLagoon.Contains(c))
                        {
                            Console.BackgroundColor = ConsoleColor.DarkRed;
                        }
                        Console.Write('.');
                        Console.ResetColor();
                    }
                }
                Console.Write('\n');
            }
        }

        private sealed record Instruction()
        {
            public required char Direction { get; init; }
            public required int Amount { get; init; }
            public required Color Color { get; init; }
        }

        private sealed record Coordinate(int X, int Y);

        private string[] GetTestInput()
        {
            return
            [
                "R 6 (#70c710)",
                "D 5 (#0dc571)",
                "L 2 (#5713f0)",
                "D 2 (#d2c081)",
                "R 2 (#59c680)",
                "D 2 (#411b91)",
                "L 5 (#8ceee2)",
                "U 2 (#caa173)",
                "L 1 (#1b58a2)",
                "U 2 (#caa171)",
                "R 2 (#7807d2)",
                "U 3 (#a77fa3)",
                "L 2 (#015232)",
                "U 2 (#7a21e3)"
            ];
        }
    }
}
