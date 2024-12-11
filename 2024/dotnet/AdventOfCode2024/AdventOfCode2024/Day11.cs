namespace AzW.AdventOfCode.Year2024
{
    public class Day11 : Day
    {
        public IEnumerable<long> ParseInput()
        {
            var stones = new List<long>();

            stones.AddRange(Input.Split(' ').Select(x => long.Parse(x)));

            return stones;
        }

        public override object ExecutePart1()
        {
            var stones = ParseInput();

            stones = ExecuteBlinks(stones, 25);

            return stones.Count();
        }

        private IEnumerable<long> ExecuteBlinks(IEnumerable<long> stones, int blinkCount)
        {
            foreach (var index in Enumerable.Range(1, blinkCount))
            {
                stones = Blink(stones);
            }

            return stones;
        }

        public override object ExecutePart2()
        {
            throw new NotImplementedException();
        }


        /*
        If the stone is engraved with the number 0, it is replaced by a stone engraved with the number 1.
        If the stone is engraved with a number that has an even number of digits, it is replaced by two stones.The left half of the digits are engraved on the new left stone, and the right half of the digits are engraved on the new right stone. (The new numbers don't keep extra leading zeroes: 1000 would become stones 10 and 0.)
        If none of the other rules apply, the stone is replaced by a new stone; the old stone's number multiplied by 2024 is engraved on the new stone.
        */
        public (long, long?) MutateStone(long input)
        {
            // If 0, return 1
            if (input == 0)
            {
                return (1, null);
            }

            // If even number of digits, split and return left + right
            var digitCount = input.ToString().Length;
            if (digitCount % 2 == 0)
            {
                var leftStoneValue = input.ToString()[..(digitCount / 2)];
                var rightStoneValue = input.ToString()[(digitCount / 2)..];

                return (long.Parse(leftStoneValue), long.Parse(rightStoneValue));
            }

            // Otherwise we multiply by 2024
            return (input * 2024, null);
        }

        public IEnumerable<long> Blink(IEnumerable<long> stones)
        {
            var updatedStones = new List<long>();

            foreach (var stone in stones)
            {
                var (stone1, stone2) = MutateStone(stone);

                updatedStones.Add(stone1);
                if (stone2 != null)
                {
                    updatedStones.Add(stone2.Value);
                }
            }

            return updatedStones;
        }
    }
}
