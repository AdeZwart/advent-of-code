namespace AzW.AdventOfCode.Year2023
{
    public class Day16 : Day.NewLineSplitParsed<string>
    {
        private const char EMPTY_SPACE = '.';
        private const char MIRROR_BACKWARD = '\\';
        private const char MIRROR_FORWARD = '/';
        private const char SPLITTER_HORIZONTAL = '-';
        private const char SPLITTER_VERTICAL = '|';
        private const string LEFT = "left";
        private const string RIGHT = "right";
        private const string UP = "up";
        private const string DOWN = "down";

        public override object ExecutePart1()
        {
            Input = GetTestInput();

            //The beam enters in the top-left corner from the left and heading to the right.
            //Then, its behavior depends on what it encounters as it moves:

            //If the beam encounters empty space(.), it continues in the same direction.
            //If the beam encounters a mirror(/ or \), the beam is reflected 90 degrees depending on the angle of the mirror.
            //For instance, a rightward - moving beam that encounters a / mirror would continue upward in the mirror's column,
            //while a rightward-moving beam that encounters a \ mirror would continue downward from the mirror's column.
            //If the beam encounters the pointy end of a splitter(| or -),
            //the beam passes through the splitter as if the splitter were empty space.
            //For instance, a rightward - moving beam that encounters a -splitter would continue in the same direction.
            //If the beam encounters the flat side of a splitter(| or -),
            //the beam is split into two beams going in each of the two directions the splitter's pointy ends are pointing.
            //For instance, a rightward-moving beam that encounters a | splitter would split into two beams:
            //one that continues upward from the splitter's column and
            //one that continues downward from the splitter's column.

            // Keep track of the energized tiles
            var energizedTiles = new HashSet<Coordinate>();

            // Keep track of the end position of the beam
            var originBeamPositions = new List<Beam>
            {
                new()
                {
                    X = 0,
                    Y = 0,
                    Direction = RIGHT
                }
            };

            while (true)
            {
                var newOrigins = new List<Beam>();

                foreach (var origin in originBeamPositions)
                {
                    var currentTile = Input[origin.Y][origin.X];

                    // Shit, this loop is infinite, we need to find a way to break it.
                    energizedTiles.Add(origin.AsCoordinate());

                    if (currentTile == EMPTY_SPACE)
                    {
                        var nextTile = GetNext(origin);
                        if (nextTile != null)
                        {
                            newOrigins.AddRange(nextTile);
                        }

                        continue;
                    }

                    if (currentTile == SPLITTER_VERTICAL)
                    {
                        // Move the beam origin forward
                        if (origin.Direction == UP || origin.Direction == DOWN)
                        {
                            var nextTile = GetNext(origin);
                            if (nextTile != null)
                            {
                                newOrigins.AddRange(nextTile);
                            }

                            continue;
                        }

                        if (origin.Direction == LEFT || origin.Direction == RIGHT)
                        {
                            var nextTiles = GetNext(origin, currentTile);
                            if (nextTiles?.Count > 0)
                            {
                                newOrigins.AddRange(nextTiles);
                            }
                            continue;
                        }
                    }

                    if (currentTile == SPLITTER_HORIZONTAL)
                    {
                        // Move the beam origin forward
                        if (origin.Direction == LEFT || origin.Direction == RIGHT)
                        {
                            var nextTile = GetNext(origin);
                            if (nextTile != null)
                            {
                                newOrigins.AddRange(nextTile);
                            }
                            continue;
                        }

                        if (origin.Direction == UP || origin.Direction == DOWN)
                        {
                            var nextTiles = GetNext(origin, currentTile);
                            if (nextTiles?.Count > 0)
                            {
                                newOrigins.AddRange(nextTiles);
                            }
                            continue;
                        }
                    }

                    if (currentTile == MIRROR_BACKWARD || currentTile == MIRROR_FORWARD)
                    {
                        var nextTile = GetNext(origin, currentTile);
                        if (nextTile != null)
                        {
                            newOrigins.AddRange(nextTile);
                        }
                        continue;
                    }
                }

                // replace originBeamPositions with new end positions
                originBeamPositions = newOrigins;

                PrintEnergizedGrid(energizedTiles, newOrigins);
            }

            throw new NotImplementedException();
            return energizedTiles.Count;
        }

        private string[] GetTestInput()
        {
            return
            [
                ".|...\\....",
                "|.-.\\.....",
                ".....|-...",
                "........|.",
                "..........",
                ".........\\",
                "..../.\\\\..",
                ".-.-/..|..",
                ".|....-|.\\",
                "..//.|...."
            ];
        }

        private void PrintEnergizedGrid(HashSet<Coordinate> energizedTiles, List<Beam> newOrigins)
        {
            Console.WriteLine($"Number of energized tiles: {energizedTiles.Count}");
            foreach (var y in Enumerable.Range(0, Input.Length))
            {
                foreach (var x in Enumerable.Range(0, Input[y].Length))
                {
                    var coordinate = new Coordinate(x, y);
                    var tile = (energizedTiles.Contains(coordinate)) ? '#' : '.';
                    var newOrigin = newOrigins.Where(o => o.AsCoordinate() == coordinate).Select(o => o);
                    if (newOrigin.Any())
                    {
                        tile = newOrigin.First().Direction switch
                        {
                            LEFT => '<',
                            RIGHT => '>',
                            UP => '^',
                            DOWN => 'V',
                            _ => throw new NotImplementedException()
                        };
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    Console.Write(tile);
                    Console.ResetColor();
                }
                Console.Write('\n');
            }

            Console.WriteLine('\n');
        }

        private List<Beam>? GetNext(Beam origin)
        {
            var nextBeam = origin.Direction switch
            {
                RIGHT => new Beam() { X = origin.X + 1, Y = origin.Y, Direction = origin.Direction },
                LEFT => new Beam() { X = origin.X - 1, Y = origin.Y, Direction = origin.Direction },
                UP => new Beam() { X = origin.X, Y = origin.Y - 1, Direction = origin.Direction },
                DOWN => new Beam() { X = origin.X, Y = origin.Y + 1, Direction = origin.Direction },
                _ => throw new NotImplementedException()
            };

            return nextBeam.IsInGrid(Input[0].Length, Input.Length) ? [nextBeam] : null;
        }

        private List<Beam>? GetNext(Beam origin, char reflection)
        {
            var newBeams = new List<Beam>();

            if (reflection == SPLITTER_VERTICAL)
            {
                var beamUp = new Beam() { X = origin.X, Y = origin.Y - 1, Direction = UP };
                if (beamUp.IsInGrid(Input[0].Length, Input.Length))
                {
                    newBeams.Add(beamUp);
                }

                var beamDown = new Beam() { X = origin.X, Y = origin.Y + 1, Direction = DOWN };
                if (beamDown.IsInGrid(Input[0].Length, Input.Length))
                {
                    newBeams.Add(beamDown);
                }

                return newBeams;
            }

            if (reflection == SPLITTER_HORIZONTAL)
            {
                var beamUp = new Beam() { X = origin.X - 1, Y = origin.Y, Direction = LEFT };
                if (beamUp.IsInGrid(Input[0].Length, Input.Length))
                {
                    newBeams.Add(beamUp);
                }

                var beamDown = new Beam() { X = origin.X + 1, Y = origin.Y, Direction = RIGHT };
                if (beamDown.IsInGrid(Input[0].Length, Input.Length))
                {
                    newBeams.Add(beamDown);
                }

                return newBeams;
            }

            if (reflection == MIRROR_FORWARD)
            {
                if (origin.Direction == RIGHT)
                {
                    var beam = new Beam() { X = origin.X, Y = origin.Y - 1, Direction = UP };
                    if (beam.IsInGrid(Input[0].Length, Input.Length))
                    {
                        return [beam];
                    }
                }

                if (origin.Direction == LEFT)
                {
                    var beam = new Beam() { X = origin.X, Y = origin.Y + 1, Direction = DOWN };
                    if (beam.IsInGrid(Input[0].Length, Input.Length))
                    {
                        return [beam];
                    }
                }

                if (origin.Direction == UP)
                {
                    var beam = new Beam() { X = origin.X + 1, Y = origin.Y, Direction = RIGHT };
                    if (beam.IsInGrid(Input[0].Length, Input.Length))
                    {
                        return [beam];
                    }
                }

                if (origin.Direction == DOWN)
                {
                    var beam = new Beam() { X = origin.X - 1, Y = origin.Y, Direction = LEFT };
                    if (beam.IsInGrid(Input[0].Length, Input.Length))
                    {
                        return [beam];
                    }
                }
            }

            if (reflection == MIRROR_BACKWARD)
            {
                if (origin.Direction == RIGHT)
                {
                    var beam = new Beam() { X = origin.X, Y = origin.Y + 1, Direction = DOWN };
                    if (beam.IsInGrid(Input[0].Length, Input.Length))
                    {
                        return [beam];
                    }
                }

                if (origin.Direction == LEFT)
                {
                    var beam = new Beam() { X = origin.X, Y = origin.Y - 1, Direction = UP };
                    if (beam.IsInGrid(Input[0].Length, Input.Length))
                    {
                        return [beam];
                    }
                }

                if (origin.Direction == UP)
                {
                    var beam = new Beam() { X = origin.X - 1, Y = origin.Y, Direction = LEFT };
                    if (beam.IsInGrid(Input[0].Length, Input.Length))
                    {
                        return [beam];
                    }
                }

                if (origin.Direction == DOWN)
                {
                    var beam = new Beam() { X = origin.X + 1, Y = origin.Y, Direction = RIGHT };
                    if (beam.IsInGrid(Input[0].Length, Input.Length))
                    {
                        return [beam];
                    }
                }
            }

            return null;
        }

        private sealed record Coordinate(int X, int Y);

        private sealed record Beam()
        {
            public required int X { get; init; }
            public required int Y { get; init; }
            public string? Direction { get; set; }
            public Coordinate AsCoordinate()
            {
                return new Coordinate(X, Y);
            }
            public bool IsInGrid(int gridWidth, int gridLength)
            {
                return (X >= 0
                     && Y >= 0
                     && Y < gridWidth
                     && X < gridLength);
            }
        }
    }
}
