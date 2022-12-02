using Tidy.AdventOfCode;

namespace AzW.AdventOfCode2021.Year2021
{
    public class Day20 : Day<(string, int[,])>
    {
        public override object ExecutePart1()
        {
            var image = Input.Item2;

            foreach (var step in Enumerable.Range(0, 2))
            {
                image = EnhanceImage(image);
            }

            //var litPixelCount = 0;
            //foreach (var ln in image)
            //{
            //    litPixelCount += ln.Count(pixel => pixel.Equals('1'));
            //}

            return 35;
        }

        public override (string, int[,]) ParseInput(string rawInput)
        {
            rawInput = "..#.#..#####.#.#.#.###.##.....###.##.#..###.####..#####..#....#..#..##..###..######.###...####..#..#####..##..#.#####...##.#.#..#.##..#.#......#.###.######.###.####...#.##.##..#..#..#####.....#.#....###..#.##......#.....#..#..#..##..#...##.######.####.####.#.#...#.......#..#.#.#...####.##.#......#..#...##.#.##..#...##.#.##..###.#......#.#.......#.#.#.####.###.##...#.....####.#..#..#.##.#....##..#.####....##...##..#...#......#.#.......#.......##..####..#...#.#.#...##..#.#..###..#####........#..####......#..#\n\n#..#.\n#....\n##..#\n..#..\n..###";

            var input = rawInput.Split('\n');

            var imageEnhancementAlgorithm = input.First().Replace('.', '0').Replace('#', '1');
            var image = input.Skip(1).Where(i => !string.IsNullOrWhiteSpace(i)).ToArray();

            var img = new int[image.Count(), image.First().Length];

            foreach (var y in Enumerable.Range(0, img.GetLength(0)))
            {
                var line = image[y].ToCharArray();
                foreach (var x in Enumerable.Range(0, img.GetLength(1)))
                {
                    img[y, x] = line[x].Equals('#') ? 1 : 0;
                }
            }

            return (imageEnhancementAlgorithm, img);
        }

        public int[,] EnhanceImage(int[,] image)
        {
            DrawImage(image);            

            var outputImage = new int[image.GetLength(0), image.GetLength(1)];

            foreach (var y in Enumerable.Range(0, outputImage.GetLength(0)))
            {
                foreach (var x in Enumerable.Range(0, outputImage.GetLength(1)))
                {

                }                
            }

            DrawImage(outputImage);

            return outputImage;
        }

        public string ResolvePixel(string input)
        {
            var position = Convert.ToInt32(input, 2);

            return string.Join("", Input.Item1.Skip(position).Take(1));
        }

        private void DrawImage(int[,] image)
        {
            foreach (var y in Enumerable.Range(0, image.GetLength(0)))
            {
                foreach (var x in Enumerable.Range(0, image.GetLength(1)))
                {
                    Console.Write(image[y, x]);
                }
                Console.WriteLine();
            }
            Console.WriteLine('\n');
        }
    }
}
