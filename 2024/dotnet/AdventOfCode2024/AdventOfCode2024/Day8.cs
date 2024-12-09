



namespace AzW.AdventOfCode.Year2024
{
    public class Day8 : Day.NewLineSplitParsed<string>
    {
        public Coordinate[] CalculateAntinodePositions(Coordinate firstAntennaLocation, Coordinate secondAntennaLocation)
        {
            var locations = new List<Coordinate>();

            var x = firstAntennaLocation.X - secondAntennaLocation.X;
            x = (x < 0 || firstAntennaLocation.X > secondAntennaLocation.X) ? -x : x;

            var y = firstAntennaLocation.Y - secondAntennaLocation.Y;
            y = (y < 0 || firstAntennaLocation.Y > secondAntennaLocation.Y) ? -y : y;

            locations.Add(new Coordinate(firstAntennaLocation.X - x, firstAntennaLocation.Y - y));
            locations.Add(new Coordinate(secondAntennaLocation.X + x, secondAntennaLocation.Y + y));

            return [.. locations];
        }

        public override object ExecutePart1()
        {
            var uniqueAntennaLocations = new HashSet<Coordinate>();

            var antennaTypes = GetAntennaTypes();

            foreach (var antennaType in antennaTypes)
            {
                var antennaLocations = GetAntennaLocations(antennaType);
                foreach (var index in Enumerable.Range(0, antennaLocations.Length - 1))
                {
                    foreach (var index2 in Enumerable.Range(index + 1, antennaLocations.Length - (index + 1)))
                    {
                        var firstAntenna = antennaLocations[index];
                        var secondAntenna = antennaLocations[index2];
                        var antinodes = CalculateAntinodePositions(firstAntenna, secondAntenna);

                        if (IsInGrid(antinodes[0]))
                        {
                            uniqueAntennaLocations.Add(antinodes[0]);
                        }

                        if (IsInGrid(antinodes[1]))
                        {
                            uniqueAntennaLocations.Add(antinodes[1]);
                        }
                    }
                }
            }

            return uniqueAntennaLocations.Count;
        }

        private bool IsInGrid(Coordinate coordinate)
        {
            if (coordinate.X < 0 || coordinate.Y < 0)
            {
                return false;
            }

            if (coordinate.Y >= Input.Length || coordinate.X >= Input[0].Length)
            {
                return false;
            }

            return true;
        }

        public override object ExecutePart2()
        {
            var uniqueAntennaLocations = new HashSet<Coordinate>();

            var antennaTypes = GetAntennaTypes();

            foreach (var antennaType in antennaTypes)
            {
                var antennaLocations = GetAntennaLocations(antennaType);
                foreach (var index in Enumerable.Range(0, antennaLocations.Length - 1))
                {
                    foreach (var index2 in Enumerable.Range(index + 1, antennaLocations.Length - (index + 1)))
                    {
                        var firstAntenna = antennaLocations[index];
                        var secondAntenna = antennaLocations[index2];
                        var antinodes = CalculateAntinodePositions(firstAntenna, secondAntenna);

                        if (IsInGrid(antinodes[0]))
                        {
                            uniqueAntennaLocations.Add(antinodes[0]);
                        }

                        if (IsInGrid(antinodes[1]))
                        {
                            uniqueAntennaLocations.Add(antinodes[1]);
                        }
                    }
                }
            }

            return uniqueAntennaLocations.Count;
        }

        public Coordinate[] GetAntennaLocations(char antenna)
        {
            var locations = new List<Coordinate>();

            foreach (var y in Enumerable.Range(0, Input.Length))
            {
                foreach (var x in Enumerable.Range(0, Input[0].Length))
                {
                    if (Input[y][x] == antenna)
                    {
                        locations.Add(new Coordinate(x, y));
                    }
                }
            }

            return [.. locations];
        }

        public char[] GetAntennaTypes()
        {
            // Find all distinct antennas
            var antennas = string.Join("", Input).Distinct().ToList();
            // Cleanup the . placeholders
            antennas.Remove('.');

            return [.. antennas];
        }
    }

    public record Coordinate(int X, int Y);
}
