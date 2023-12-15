namespace AzW.AdventOfCode.Year2023
{
    /*
     * | is a vertical pipe connecting north and south.
     * - is a horizontal pipe connecting east and west.
     * L is a 90-degree bend connecting north and east.
     * J is a 90-degree bend connecting north and west.
     * 7 is a 90-degree bend connecting south and west.
     * F is a 90-degree bend connecting south and east.
     * . is ground; there is no pipe in this tile.
     * S is the starting position of the animal; there is a pipe on this tile, but your sketch doesn't show what shape the pipe has.
     */
    public class Day10 : Day.NewLineSplitParsed<string>
    {
        public const string NORTH = "north";
        public const string EAST = "east";
        public const string SOUTH = "south";
        public const string WEST = "west";
        public const char NORTH_SOUTH = '|'; 
        public const char EAST_WEST = '-'; 
        public const char NORTH_EAST = 'L'; 
        public const char NORTH_WEST = 'J'; 
        public const char SOUTH_WEST = '7'; 
        public const char SOUTH_EAST = 'F'; 
        public const char GROUND = '.';
        public const char START = 'S';

        public override object ExecutePart1()
        {
            //Input = GetTestInput();

            var y = Array.FindIndex(Input, ln => ln.Contains(START));
            var x = Input[y].IndexOf(START);

            var entrancePoint = new Coordinate(x, y);
            entrancePoint.Shape = START;

            var clockwiseSteps = new List<Coordinate>();
            // Check north and east for clockwise
            if (IsConnectedNorth(entrancePoint, out var north))
            {
                clockwiseSteps.Add(north);
            }
            if (IsConnectedEast(entrancePoint, out var east))
            {
                clockwiseSteps.Add(east);
            }

            while (true)
            {
                var nextClockwise = GetNext(clockwiseSteps.Last());
                if (nextClockwise.GetHashCode() == entrancePoint.GetHashCode())
                {
                    // we're back at the start
                    break;
                }
                clockwiseSteps.Add(nextClockwise);
            }

            return Math.Ceiling(clockwiseSteps.Count / 2d);
        }

        private Coordinate GetNext(Coordinate current)
        {
            if (current.Origin == WEST)
            {
                if (current.Shape == NORTH_WEST)
                {
                    var next = new Coordinate(current.X, current.Y - 1);
                    next.Shape = Input[next.Y][next.X];
                    next.Origin = SOUTH;
                    return next;
                }

                if (current.Shape == SOUTH_WEST)
                {
                    var next = new Coordinate(current.X, current.Y + 1);
                    next.Shape = Input[next.Y][next.X];
                    next.Origin = NORTH;
                    return next;
                }

                if (current.Shape == EAST_WEST)
                {
                    var next = new Coordinate(current.X + 1, current.Y);
                    next.Shape = Input[next.Y][next.X];
                    next.Origin = WEST;
                    return next;
                }
            }

            if (current.Origin == SOUTH)
            {
                if (current.Shape == SOUTH_EAST)
                {
                    var next = new Coordinate(current.X + 1, current.Y);
                    next.Shape = Input[next.Y][next.X];
                    next.Origin = WEST;
                    return next;
                }

                if (current.Shape == SOUTH_WEST)
                {
                    var next = new Coordinate(current.X - 1, current.Y);
                    next.Shape = Input[next.Y][next.X];
                    next.Origin = EAST;
                    return next;
                }

                if (current.Shape == NORTH_SOUTH)
                {
                    var next = new Coordinate(current.X, current.Y - 1);
                    next.Shape = Input[next.Y][next.X];
                    next.Origin = SOUTH;
                    return next;
                }
            }

            if (current.Origin == NORTH)
            {
                if (current.Shape == NORTH_SOUTH)
                {
                    var next = new Coordinate(current.X, current.Y + 1);
                    next.Shape = Input[next.Y][next.X];
                    next.Origin = NORTH;
                    return next;
                }

                if (current.Shape == NORTH_EAST)
                {
                    var next = new Coordinate(current.X + 1, current.Y);
                    next.Shape = Input[next.Y][next.X];
                    next.Origin = WEST;
                    return next;
                }

                if (current.Shape == NORTH_WEST)
                {
                    var next = new Coordinate(current.X - 1, current.Y);
                    next.Shape = Input[next.Y][next.X];
                    next.Origin = EAST;
                    return next;
                }
            }

            if (current.Origin == EAST)
            {
                if (current.Shape == EAST_WEST)
                {
                    var next = new Coordinate(current.X - 1, current.Y);
                    next.Shape = Input[next.Y][next.X];
                    next.Origin = EAST;
                    return next;
                }

                if (current.Shape == NORTH_EAST)
                {
                    var next = new Coordinate(current.X, current.Y - 1);
                    next.Shape = Input[next.Y][next.X];
                    next.Origin = SOUTH;
                    return next;
                }

                if (current.Shape == SOUTH_EAST)
                {
                    var next = new Coordinate(current.X, current.Y + 1);
                    next.Shape = Input[next.Y][next.X];
                    next.Origin = NORTH;
                    return next;
                }
            }

            throw new NotImplementedException();
        }

        private bool IsConnectedNorth(Coordinate c, out Coordinate north)
        {
            north = new Coordinate(c.X, c.Y - 1);

            if (IsOutOfBound(north)) { return false; }

            north.Shape = Input[north.Y][north.X];
            north.Origin = SOUTH;

            return Input[north.Y][north.X] switch
            {
                NORTH_SOUTH => true,
                SOUTH_WEST => true,
                SOUTH_EAST => true,
                _ => false,
            };
        }

        private bool IsConnectedEast(Coordinate c, out Coordinate east)
        {
            east = new Coordinate(c.X + 1, c.Y);

            if (IsOutOfBound(east)) { return false; }

            east.Shape = Input[east.Y][east.X];
            east.Origin = WEST;

            return Input[east.Y][east.X] switch
            {
                EAST_WEST => true,
                NORTH_WEST => true,
                SOUTH_WEST => true,
                _ => false
            };
        }

        private bool IsOutOfBound(Coordinate c)
        {
            return (c.X < 0
                 || c.Y < 0
                 || c.Y >= Input.Length
                 || c.X >= Input[c.Y].Length);
        }

        private string[] GetTestInput()
        {
            return
            [
                "7-F7-",
                ".FJ|7",
                "SJLL7",
                "|F--J",
                "LJ.LJ"
            ];
        }

        private sealed record Coordinate(int X, int Y)
        {
            public char? Shape { get; set; }

            public string? Origin { get; set; }

            public override string ToString()
            {
                return $"[{this.X};{this.Y}] => [{this.Shape}]";
            }

            public override int GetHashCode()
            {
                return int.Parse($"{X}{Y}{(int)Shape}");
            }
        };
    }
}
