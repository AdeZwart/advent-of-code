namespace AzW.AdventOfCode.Year2023
{
    public class Day13 : Day.NewLineSplitParsed<string>
    {
        public override object ExecutePart1()
        {
            //Input = GetTestInput();

            var notes = new List<long>();
            var patterns = GetPatterns();

            foreach (var i in Enumerable.Range(0, patterns.Count))
            {
                Console.WriteLine($"Get score for pattern {i}");
                var score = GetMirrorScore(patterns[i]);
                
                notes.Add(score);
            }

            return notes.Sum();
        }

        private List<string[]> GetPatterns()
        {
            var patterns = new List<string[]>();
            var pattern = new List<string>();

            foreach (var ln in Input)
            {
                // Start a new pattern
                if (string.IsNullOrWhiteSpace(ln))
                {
                    patterns.Add(pattern.ToArray());
                    pattern = [];
                    continue;
                }

                pattern.Add(ln);
            }

            patterns.Add(pattern.ToArray());

            return patterns;
        }

        private long GetMirrorScore(string[] pattern)
        {
            var isHorizontalReflection = false;
            var mirroredCount = 0;

            // Check horizontal
            foreach (var index in Enumerable.Range(0, pattern.Length))
            {
                try
                {
                    if (pattern[index] == pattern[index + 1] && IsReflectingUntilBoundary(pattern, index, true))
                    {
                        //Console.WriteLine($"Pattern is mirrored on index {index}");
                        //Console.WriteLine($"{index}: {pattern[index]}");
                        //Console.WriteLine($"{index + 1}: {pattern[index + 1]}");
                        
                        // Horizontal mirror found.
                        isHorizontalReflection = true;
                        mirroredCount = (index + 1) * 100;

                        PrintPattern(pattern, index, true);
                        Console.WriteLine($"Horizontal note: {mirroredCount}\n");

                        return mirroredCount;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    // do nothing
                }
            }

            // Check vertical
            foreach (var index in Enumerable.Range(0, pattern[0].Length))
            {
                try
                {
                    var currentCol = string.Concat(pattern.Select(ln => ln[index]));
                    var nextCol = string.Concat(pattern.Select(ln => ln[index + 1]));

                    if (currentCol == nextCol && IsReflectingUntilBoundary(pattern, index))
                    {
                        //Console.WriteLine($"Pattern is mirrored on index {index}");
                        //Console.WriteLine($"{index}: {currentCol}");
                        //Console.WriteLine($"{index + 1}: {nextCol}");

                        // Vertical mirror found
                        mirroredCount = index + 1;

                        PrintPattern(pattern, index);
                        Console.WriteLine($"Vertical note: {mirroredCount}\n");

                        return mirroredCount;
                    }

                }
                catch (IndexOutOfRangeException)
                {
                    // do nothing
                }
            }

            return 0;
        }

        private static bool IsReflectingUntilBoundary(string[] pattern, int index, bool isHorizontal = false)
        {
            var len = isHorizontal ? pattern.Length : pattern[0].Length;

            var midpoint = Math.Ceiling((decimal)(len / 2));

            var range = (index > midpoint) ? Enumerable.Range(index, len - index) : Enumerable.Range(1, index);

            try
            {
                foreach (var i in range)
                {
                    if (isHorizontal)
                    {
                        var prev = index - i;
                        var next = index + 1 + i;

                        if (pattern[prev] != pattern[next])
                        {
                            return false;
                        }
                    }
                    else
                    {
                        var prev = index - i;
                        var next = index + 1 + i;

                        var prevCol = string.Concat(pattern.Select(ln => ln[prev]));
                        var nextCol = string.Concat(pattern.Select(ln => ln[next]));

                        if (prevCol != nextCol)
                        {
                            return false;
                        }
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
            }

            return true;
        }

        private void PrintPattern(string[] pattern, int index, bool isHorizontal = false)
        {
            foreach (var y in Enumerable.Range(0, pattern.Length))
            {
                if (isHorizontal && (y == index || y == index + 1))
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                foreach (var x in Enumerable.Range(0, pattern[y].Length))
                {
                    if (!isHorizontal && (x == index || x == index + 1))
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.Write(pattern[y][x]);
                    if (!isHorizontal && (x == index || x == index + 1))
                    {
                        Console.ResetColor();
                    }
                }

                Console.ResetColor();
                Console.Write("\n");
            }

            Console.ResetColor();
            Console.WriteLine("");
        }

        private string[] GetTestInput()
        {
            return
            [
                "#.##..##.",
                "..#.##.#.",
                "##......#",
                "##......#",
                "..#.##.#.",
                "..##..##.",
                "#.#.##.#.",
                "",
                "#...##..#",
                "#....#..#",
                "..##..###",
                "#####.##.",
                "#####.##.",
                "..##..###",
                "#....#..#"
            ];
        }
    }
}
