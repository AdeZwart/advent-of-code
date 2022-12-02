using System;
using System.Collections.Generic;
using System.Linq;

namespace adventofcode
{
    public static class Day11
    {
        private const char EMPTY = 'L';
        private const char OCCUPIED = '#';
        private const char FLOOR = '.';

        public static void PartOne(string[] fileLines)
        {
            Console.WriteLine("========== AdventOfCode Day 11 - Part One ==========");

#if DEBUG
            foreach (var ln in fileLines)
            {
                Console.WriteLine(ln);
            }
            Console.WriteLine("\r\n==========\r\n");
#endif

            var updatedPlan = new string[fileLines.Length];
            // Continue applying the rules untill nothing changes
            while (true)
            {
                updatedPlan = ApplyRules(fileLines, 1, 4);

#if DEBUG
                foreach (var ln in updatedPlan)
                {
                    Console.WriteLine(ln);
                }
                Console.WriteLine("\r\n==========\r\n");
#endif

                if (updatedPlan.SequenceEqual(fileLines))
                {
                    // We're done
                    break;
                }

                // Update fileLines to give it another go.
                fileLines = updatedPlan;
            }

            var occupiedCount = 0;
            foreach (var line in updatedPlan)
            {
                occupiedCount += line.Count(s => s == OCCUPIED);
            }

            Console.WriteLine($"{occupiedCount} seats end up occupied.\r\n");
        }

        public static void PartTwo(string[] fileLines)
        {
            Console.WriteLine("========== AdventOfCode Day 11 - Part Two ==========");
            var newFileLines = new string[fileLines.Length];
#if DEBUG
            foreach (var i in Enumerable.Range(0, fileLines.Length))
            {
                Console.WriteLine($"{fileLines[i]}");

                newFileLines[i] = fileLines[i].Replace("L", "#");
            }

            Console.WriteLine("\r\n==========================================================================================\r\n");
#endif

#if DEBUG
            foreach (var ln in newFileLines)
            {
                Console.WriteLine(ln);
            }
            Console.WriteLine("\r\n==========================================================================================\r\n");
#endif

            var updatedPlan = new string[newFileLines.Length];
            // Continue applying the rules untill nothing changes
            while (true)
            {
                updatedPlan = ApplyRules(newFileLines, 1, 5, false);

#if DEBUG
                var count = 0;
                foreach (var ln in updatedPlan)
                {
                    Console.WriteLine(ln);
                    count += ln.Count(s => s == OCCUPIED);
                }
                Console.WriteLine($"\r\nThis step has {count} seats occupied.");
                Console.WriteLine("==========================================================================================\r\n");
#endif

                if (updatedPlan.SequenceEqual(fileLines))
                {
                    // We're done
                    break;
                }

                // Update fileLines to give it another go.
                newFileLines = updatedPlan;
            }

            var occupiedCount = 0;
            foreach (var line in updatedPlan)
            {
                occupiedCount += line.Count(s => s == OCCUPIED);
            }

            Console.WriteLine($"{occupiedCount} seats end up occupied.\r\n");
        }

        private static string[] ApplyRules(string[] floorPlan, int becomeOccupiedTolerance, int becomeEmptyTolerance, bool onlyAdjacent = true)
        {
            var updatedSeatPlan = new List<string>();

            for (var lineIndex = 0; lineIndex < floorPlan.Length; lineIndex++)
            {
                var currentLine = floorPlan[lineIndex];
                var updatedLine = new List<char>();
                for (var seatIndex = 0; seatIndex < currentLine.Length; seatIndex++)
                {
                    switch (currentLine[seatIndex])
                    {
                        case EMPTY:
                            char updatedEmptySeat;
                            if (onlyAdjacent)
                            {
                                updatedEmptySeat = SeatShouldBeOccupied(floorPlan, lineIndex, seatIndex, becomeOccupiedTolerance) ? OCCUPIED : EMPTY;
                            }
                            else
                            {
                                updatedEmptySeat = SeatShouldBeOccupied2(floorPlan, lineIndex, seatIndex, becomeOccupiedTolerance) ? OCCUPIED : EMPTY;
                            }
                            updatedLine.Add(updatedEmptySeat);
                            break;

                        case FLOOR:
                            /// Floor(.) never changes; seats don't move, and nobody sits on the floor.
                            updatedLine.Add(FLOOR);
                            continue;

                        case OCCUPIED:
                            char updatedOccupiedSeat;
                            if (onlyAdjacent)
                            {
                                updatedOccupiedSeat = SeatShouldBeOccupied(floorPlan, lineIndex, seatIndex, becomeEmptyTolerance) ? OCCUPIED : EMPTY;
                            }
                            else
                            {
                                updatedOccupiedSeat = SeatShouldBeOccupied2(floorPlan, lineIndex, seatIndex, becomeEmptyTolerance) ? OCCUPIED : EMPTY;
                            }
                            updatedLine.Add(updatedOccupiedSeat);
                            break;
                    }
                }
                updatedSeatPlan.Add(new string(updatedLine.ToArray()));
            }

            return updatedSeatPlan.ToArray();
        }

        /// All decisions are based on the number of occupied seats adjacent to a given seat 
        /// (one of the eight positions immediately up, down, left, right, or diagonal from the seat)        
        /// If a seat is empty (L) and there are no occupied seats adjacent to it, the seat becomes occupied.
        /// If a seat is occupied (#) and four or more seats adjacent to it are also occupied, the seat becomes empty.
        /// [*] [*] [*]
        /// [*] [X] [*]
        /// [*] [*] [*]
        private static bool SeatShouldBeOccupied(string[] floorPlan, int lineIndex, int seatIndex, int maxOccupiedAdjacentSeats)
        {
            var occupiedSeatCount = 0;
            for (int ln = lineIndex - 1; ln < lineIndex + 2; ln++)
            {
                // Can't look outside the waiting area
                if (ln < 0 || ln >= floorPlan.Length)
                {
                    continue;
                }

                for (int st = seatIndex - 1; st < seatIndex + 2; st++)
                {
                    // Can't look outside the waiting area
                    if (st < 0 || st >= floorPlan[ln].Length)
                    {
                        continue;
                    }

                    // No need to check the current seat
                    if (ln == lineIndex && st == seatIndex)
                    {
                        continue;
                    }

                    var seat = floorPlan[ln][st];
                    if (seat == OCCUPIED)
                    {
                        occupiedSeatCount++;
                        if (occupiedSeatCount >= maxOccupiedAdjacentSeats)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        /// All decisions are based on the number of occupied seats adjacent to a given seat 
        /// (one of the eight positions immediately up, down, left, right, or diagonal from the seat)        
        /// If a seat is empty (L) and there are no occupied seats adjacent to it, the seat becomes occupied.
        /// If a seat is occupied (#) and four or more seats adjacent to it are also occupied, the seat becomes empty.
        /// As soon as people start to arrive, you realize your mistake. 
        /// People don't just care about adjacent seats - they care about the first seat they can see in each of those eight directions!
        /// [.][.][.][.][.][.][.][#][.]
        /// [.][.][.][#][.][.][.][.][.]
        /// [.][#][.][.][.][.][.][.][.]
        /// [.][.][.][.][.][.][.][.][.]
        /// [.][.][#][L][.][.][.][.][#]
        /// [.][.][.][.][#][.][.][.][.]
        /// [.][.][.][.][.][.][.][.][.]
        /// [#][.][.][.][.][.][.][.][.]
        /// [.][.][.][#][.][.][.][.][.]
        private static bool SeatShouldBeOccupied2(string[] floorPlan, int lineIndex, int seatIndex, int maxOccupiedAdjacentSeats)
        {
            var occupiedSeatCount = 0;

            bool checkLeft = true;
            bool checkRight = true;
            bool checkAbove = true;
            bool checkBelow = true;
            bool checkLeftUp = true;
            bool checkRightDown = true;
            bool checkRightUp = true;
            bool checkLeftDown = true;

            var seatsToCheck = new List<char>();

            for (int i = 1; i <= floorPlan.Length; i++)
            {
                if (checkLeft)
                {
                    try
                    {
                        var leftSeat = floorPlan[lineIndex][seatIndex - i];
                        seatsToCheck.Add(leftSeat);

                        checkLeft = leftSeat == FLOOR;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        checkLeft = false;
                    }
                }

                if (checkRight)
                {
                    try
                    {
                        var rightSeat = floorPlan[lineIndex][seatIndex + i];
                        seatsToCheck.Add(rightSeat);

                        checkRight = rightSeat == FLOOR;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        checkRight = false;
                    }
                }

                if (checkAbove)
                {
                    try
                    {
                        var aboveSeat = floorPlan[lineIndex - i][seatIndex];
                        seatsToCheck.Add(aboveSeat);

                        checkAbove = aboveSeat == FLOOR;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        checkAbove = false;
                    }
                }

                if (checkBelow)
                {
                    try
                    {
                        var belowSeat = floorPlan[lineIndex + i][seatIndex];
                        seatsToCheck.Add(belowSeat);

                        checkBelow = belowSeat == FLOOR;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        checkBelow = false;
                    }
                }

                if (checkLeftUp)
                {
                    try
                    {
                        var diagUp = floorPlan[lineIndex - i][seatIndex - i];
                        seatsToCheck.Add(diagUp);

                        checkLeftUp = diagUp == FLOOR;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        checkLeftUp = false;
                    }
                }

                if (checkRightDown)
                {
                    try
                    {
                        var diagDown = floorPlan[lineIndex + i][seatIndex + i];
                        seatsToCheck.Add(diagDown);

                        checkRightDown = diagDown == FLOOR;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        checkRightDown = false;
                    }
                }

                if (checkRightUp)
                {
                    try
                    {
                        var diagUp = floorPlan[lineIndex - i][seatIndex + i];
                        seatsToCheck.Add(diagUp);

                        checkRightUp = diagUp == FLOOR;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        checkRightUp = false;
                    }
                }

                if (checkLeftDown)
                {
                    try
                    {
                        var diagDown = floorPlan[lineIndex + i][seatIndex - i];
                        seatsToCheck.Add(diagDown);

                        checkLeftDown = diagDown == FLOOR;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        checkLeftDown = false;
                    }
                }

                occupiedSeatCount += seatsToCheck.Count(s => s == OCCUPIED);
                if (occupiedSeatCount >= maxOccupiedAdjacentSeats)
                {
                    //Console.WriteLine($"Seat with coordinates '{lineIndex}','{seatIndex}' required '{i}' iterations to determine if it should be occupied.");
                    return false;
                }

                // We've hit a chair in all directions
                if (!checkLeft && !checkRight
                    && !checkAbove && !checkBelow
                    && !checkLeftUp && !checkLeftDown
                    && !checkRightUp && !checkRightDown)
                {
                    //Console.WriteLine($"Seat with coordinates '{lineIndex}','{seatIndex}' required '{i}' iterations to determine if it should be occupied.");
                    break;
                }
            }

            return true;
        }
    }
}
