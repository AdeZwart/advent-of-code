using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace adventofcode
{
    public static class Day23
    {
        private static Stopwatch stopWatch = new Stopwatch();

        private const int pickUpLength = 3;

        public static void PartOne(IEnumerable<string> input)
        {
            stopWatch.Restart();
            Console.WriteLine("========== AdventOfCode Day 23 - Part One ==========");

            var result = MoveCups(input, 100);

            Console.WriteLine($"The answer is: {result}");
            stopWatch.Stop();
            Console.WriteLine($"=> found in {stopWatch.Elapsed:mm\\:ss\\:ffff}\r\n");
        }

        public static void PartTwo(IEnumerable<string> input)
        {
            stopWatch.Restart();
            Console.WriteLine("========== AdventOfCode Day 23 - Part Two ==========");

            var cups = input.First().Where(c => char.IsDigit(c)).Select(i => int.Parse(i.ToString())).ToList();

            var startFrom = cups.Max() + 1;
            var length = (1000000 - cups.Count) + 1;

            cups.AddRange(Enumerable.Range(startFrom, length));

            var result = MoveCups(cups, 10000000, true);

            Console.WriteLine($"The answer is: {result}");
            stopWatch.Stop();
            Console.WriteLine($"=> found in {stopWatch.Elapsed:mm\\:ss\\:ffff}\r\n");
        }

        private static string MoveCups(IEnumerable<string> input, int moveCount, bool b = false)
        {
            var cups = input.First().Where(c => char.IsDigit(c)).Select(i => int.Parse(i.ToString())).ToList();
            return MoveCups(cups, moveCount, b);
        }

        private static string MoveCups(List<int> cups, int moveCount, bool b = false)
        {            
            /// The crab selects a new current cup: the cup which is immediately clockwise of the current cup.
            foreach (var move in Enumerable.Range(0, moveCount))
            {
                // Reset to start in at the beginning of the circle again
                var actualMove = move % cups.Count;
                var currentCup = cups[actualMove];
                var oneIndexedMove = actualMove + 1;

                Console.WriteLine($"-- move {move + 1} --");
#if DEBUG
                Console.WriteLine($"cups: {string.Join(" ", cups.Take(actualMove))} ({currentCup}) {string.Join(" ", cups.Skip(oneIndexedMove))}");
#endif

                var pickedUp = new int[pickUpLength];
                /// The crab picks up the three cups that are immediately clockwise of the current cup
                if (oneIndexedMove + pickUpLength > cups.Count)
                {
                    var pickUp = cups.Skip(oneIndexedMove).ToList();
                    cups.RemoveRange(oneIndexedMove, cups.Count - oneIndexedMove);

                    while(pickUp.Count < pickUpLength)
                    {
                        pickUp.Add(cups.First());
                        cups.RemoveAt(0);
                    }

                    pickedUp = pickUp.ToArray();
                }
                else
                {
                    cups.CopyTo(oneIndexedMove, pickedUp, 0, pickUpLength);
                    /// They are removed from the circle;
                    cups.RemoveRange(oneIndexedMove, pickUpLength);
                }

#if DEBUG
                Console.WriteLine($"pick up: {string.Join(", ", pickedUp)}");
#endif

                /// The crab selects a destination cup: the cup with a label equal to the current cup's label minus one. 
                /// If this would select one of the cups that was just picked up, 
                /// the crab will keep subtracting one until it finds a cup that wasn't just picked up. 
                /// If at any point in this process the value goes below the lowest value on any cup's label, 
                /// it wraps around to the highest value on any cup's label instead.
                var destination = GetDestination(cups, actualMove);
#if DEBUG
                Console.WriteLine($"destination: {destination}");
#endif
                /// The crab places the cups it just picked up so that they are immediately clockwise of the destination cup. 
                /// They keep the same order as when they were picked up. 
                var insertIndex = cups.FindIndex(c => c.Equals(destination)) + 1;
                cups.InsertRange(insertIndex, pickedUp);

                // rotate if needed to keep the current cup on the correct index
                if (insertIndex <= actualMove)
                {
                    var diff = cups.FindIndex(c => c.Equals(currentCup)) - actualMove;

                    cups.AddRange(cups.Take(diff));
                    cups.RemoveRange(0, diff);
                }
#if DEBUG
                Console.WriteLine($"\r");
#endif

                if (b && move.Equals(moveCount))
                {
                    var result = cups.Skip(oneIndexedMove).Take(2);
                    return (result.First() * result.Last()).ToString();
                }
            }            

            while (!cups.First().Equals(1))
            {
                cups.Add(cups.First());
                cups.RemoveAt(0);
            }

            return string.Join("", cups.Skip(1));
        }

        private static int GetDestination(IEnumerable<int> cups, int currentIndex)
        {
            var destination = 0;
            
            var currentCup = (currentIndex >= cups.Count()) ? cups.Last() : cups.ToList()[currentIndex];

            var lst = cups.Skip(currentIndex).ToList();
            lst.AddRange(cups.Take(currentIndex));

            var lowestValue = cups.OrderBy(i => i).First();
            var highestValue = cups.OrderBy(i => i).Last();

            var count = 1;
            while (destination == 0)
            {
                if (currentCup - count < lowestValue)
                {
                    destination = highestValue;
                    break;
                }

                var rslt = lst.Where(l => l.Equals(currentCup - count));
                if (rslt.Any())
                {
                    destination = rslt.First();
                    break;
                }

                count++;
            }

            return destination;
        }
    }
}
