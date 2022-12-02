using Tidy.AdventOfCode;

namespace AzW.AdventOfCode2021.Year2021
{
    public class Day21 : Day.NewLineSplitParsed<string>
    {
        private readonly string[] SampleInput = new string[] { "Player 1 starting position: 4", "Player 2 starting position: 8" };

        public override object ExecutePart1()
        {            
            var scoreboard = new int[] { 0, 0 };

            var playerPositions = new int[]
            {
                int.Parse(Input.First().Split(' ').Last()),
                int.Parse(Input.Last().Split(' ').Last())
            };

            //Console.WriteLine($"Player1 starts in position: {playerPositions.First()}");
            //Console.WriteLine($"Player2 starts in position: {playerPositions.Last()}");

            var totalDieRolls = 0;
            var rollValue = 0;
            var winner = false;

            while (true)
            {
                foreach (var i in Enumerable.Range(0, playerPositions.Length))
                {
                    //Console.Write($"Player{i + 1} rolls: ");
                    foreach (var rolltimes in Enumerable.Range(0, 3))
                    {
                        totalDieRolls++; rollValue++;

                        if (rollValue > 100)
                        {
                            rollValue = 1;
                        }

                        //Console.Write($"{rollValue}, ");

                        playerPositions[i] += rollValue;
                        if (playerPositions[i] > 10)
                        {
                            playerPositions[i] = (playerPositions[i] % 10 > 0) ? playerPositions[i] % 10 : 10;
                        }
                    }

                    scoreboard[i] += playerPositions[i];
                    //Console.Write($"and moves to space: {playerPositions[i]} for a total score of {scoreboard[i]}.");
                    //Console.WriteLine();
                    //Console.WriteLine($"Total die rolls: {totalDieRolls}\n");

                    if (scoreboard.Any(s => s >= 1000))
                    {
                        winner = true;
                        break;
                    }
                }

                if (winner)
                {
                    break;
                }
            }

            var result = scoreboard.Min() * totalDieRolls;

            return result;
        }

        public override object ExecutePart2()
        {            
            return base.ExecutePart2();
        }
    }
}
