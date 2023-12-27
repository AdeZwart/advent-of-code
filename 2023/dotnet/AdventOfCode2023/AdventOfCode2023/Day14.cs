namespace AzW.AdventOfCode.Year2023
{
    public class Day14 : Day.NewLineSplitParsed<string>
    {
        private const char ROUND_ROCK = '0';
        private const char SQUARE_ROCK = '#';
        private const char EMPTY_SPACE = '.';

        public override object ExecutePart1()
        {
            Input = GetTestInput();

            foreach (var y in Enumerable.Range(0, Input.Length))
            {
                // We can't roll further north
                if (y == 0)
                {
                    continue;
                }

                foreach (var x in Enumerable.Range(0, Input[y].Length))
                {
                    var currentRock = Input[y][x];
                    var northRock = Input[y - 1][x];

                    if (currentRock == ROUND_ROCK && northRock == EMPTY_SPACE)
                    {
                        // Roll the rock north
                        //Input[y - 1][x] = ROUND_ROCK;
                        //Input[y][x] = EMPTY_SPACE;
                    }
                }
            }

            throw new NotImplementedException();
        }

        private string[] GetTestInput()
        {
            return
            [
                "O....#....",
                "O.OO#....#",
                ".....##...",
                "OO.#O....O",
                ".O.....O#.",
                "O.#..O.#.#",
                "..O..#O..O",
                ".......O..",
                "#....###..",
                "#OO..#...."
            ];
        }
    }
}
