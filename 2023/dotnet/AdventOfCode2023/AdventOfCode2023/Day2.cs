using System.Text.RegularExpressions;

namespace AzW.AdventOfCode.Year2023
{
    public class Day2 : Day.NewLineSplitParsed<string>
    {
        const string RED = "red";
        const string GREEN = "green";
        const string BLUE = "blue";

        public override object ExecutePart1()
        {
            const int RED_UPPER_LIMIT = 12;
            const int GREEN_UPPER_LIMIT = 13;
            const int BLUE_UPPER_LIMIT = 14;

            const string DIGIT_REGEX = @"\D+";
            var regex = new Regex(DIGIT_REGEX);

            long sum = 0;

            foreach (var game in Input)
            {
                var gameID = int.Parse(game.Split(':').First().Split(' ').Last());
                var gameSets = game.Split(':').Last();

                if (HasResultOverUpperLimit(regex, gameSets, BLUE_UPPER_LIMIT))
                {
                    // A result is over the highest upper limit, invalid game
                    //Console.WriteLine($"Game {gameID} is INVALID");
                    //Console.WriteLine(gameSets);
                    continue;
                }

                if (!HasResultOverUpperLimit(regex, gameSets, RED_UPPER_LIMIT))
                {
                    // All results are below the lowest upper limit, valid game
                    sum += gameID;
                    Console.WriteLine($"Game {gameID} is VALID");
                    Console.WriteLine(gameSets);
                    continue;
                }

                bool gameIsValid = true;
                foreach (var set in gameSets.Split(';'))
                {
                    if (!gameIsValid)
                    {
                        break;
                    }

                    foreach (var result in set.Split(','))
                    {
                        if ((result.Contains(RED) && HasResultOverUpperLimit(regex, result, RED_UPPER_LIMIT))
                            || (result.Contains(GREEN) && HasResultOverUpperLimit(regex, result, GREEN_UPPER_LIMIT))
                            || (result.Contains(BLUE) && HasResultOverUpperLimit(regex, result, BLUE_UPPER_LIMIT)))
                        {
                            // Invalid game result
                            gameIsValid = false;
                            //Console.WriteLine($"Game {gameID} is INVALID with result {result}");
                            //Console.WriteLine(gameSets);
                            break;
                        }
                    }
                }

                if (gameIsValid)
                {
                    Console.WriteLine($"Game {gameID} is VALID");
                    Console.WriteLine(gameSets);
                    sum += gameID;
                }
            }

            return sum;
        }

        public override object ExecutePart2()
        {
            var powerOfMinCubes = new List<long>();

            foreach (var game in Input)
            {
                int minRedCubes = 0;
                int minGreenCubes = 0;
                int minBlueCubes = 0;

                var sets = game.Split(':').Last();
                foreach (var set in sets.Split(';'))
                {
                    var results = set.Split(',').Where(v => !string.IsNullOrWhiteSpace(v));
                    foreach (var result in results)
                    {
                        var res = result.Split(' ').Where(v => !string.IsNullOrWhiteSpace(v));
                        if (res.Last() == RED && int.Parse(res.First()) > minRedCubes)
                        {
                            minRedCubes = int.Parse(res.First());
                        }

                        if(res.Last() == GREEN && int.Parse(res.First()) > minGreenCubes)
                        {
                            minGreenCubes = int.Parse(res.First());
                        }

                        if (res.Last() == BLUE && int.Parse(res.First()) > minBlueCubes)
                        {
                            minBlueCubes = int.Parse(res.First());
                        }
                    }
                }

                var powerOfGameResults = minRedCubes * minGreenCubes * minBlueCubes;
                powerOfMinCubes.Add(powerOfGameResults);
            }

            var sum = powerOfMinCubes.Sum();
            return sum;
        }

        private static bool HasResultOverUpperLimit(Regex regex, string input, int upperLimit)
        {
            var values = regex.Split(input)
                              .Where(val => !string.IsNullOrWhiteSpace(val))
                              .Select(int.Parse);

            return (values.Any(val => val > upperLimit));
        }
    }
}
