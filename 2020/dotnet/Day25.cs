using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace adventofcode
{
    public static class Day25
    {
        private static Stopwatch stopWatch = new Stopwatch();

        private const int SUBJECT_NUMBER = 7;
        private const int DIVIDER = 20201227;

        public static void PartOne(IEnumerable<string> fileLines)
        {
            stopWatch.Restart();
            Console.WriteLine("========== AdventOfCode Day 25 - Part One ==========");

            var publicKeys = fileLines.Select(x => int.Parse(x));

            var cardLoop = ReverseEngineerLoopSize(publicKeys.First());
            Console.WriteLine($"Card LoopSize is '{cardLoop}'");
            var doorLoop = ReverseEngineerLoopSize(publicKeys.Last());
            Console.WriteLine($"Door LoopSize is '{doorLoop}'");

            var a = TransformSubjectNumber(publicKeys.First(), doorLoop);
            Console.WriteLine($"Card encryptionKey is '{a}'");
            var b = TransformSubjectNumber(publicKeys.Last(), cardLoop);
            Console.WriteLine($"Door encryptionKey is '{b}'");

            var result = a;

            Console.WriteLine($"The encryption key is '{result}'\r\n");
            stopWatch.Stop();
            Console.WriteLine($"=> found in {stopWatch.Elapsed:mm\\:ss\\:ffff}\r\n");
        }

        public static void PartTwo(string[] fileLines)
        {
            stopWatch.Restart();
            Console.WriteLine("========== AdventOfCode Day 25 - Part Two ==========");

            var result = "";

            Console.WriteLine($"{result}.\r\n");
            stopWatch.Stop();
            Console.WriteLine($"=> found in {stopWatch.Elapsed:mm\\:ss\\:ffff}\r\n");
        }

        private static int ReverseEngineerLoopSize(int publicKey)
        {
            var loopSize = 0;

            var value = 1;

            while(value != publicKey)
            {
                value *= SUBJECT_NUMBER;
                value %= DIVIDER;
                
                loopSize++;
            }

            return loopSize;
        }

        private static long TransformSubjectNumber(int publicKey, int loopSize)
        {
            long value = 1;

            foreach(int i in Enumerable.Range(1, loopSize))
            {
                value *= publicKey;
                value %= DIVIDER;
            }

            return value;
        }
    }
}
