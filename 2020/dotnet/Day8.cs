using System;
using System.Collections.Generic;
using System.Linq;

namespace adventofcode
{
    public static class Day8
    {
        public static void PartOne(string[] fileLines)
        {
            Console.WriteLine("========== AdventOfCode Day 8 - Part One ==========");
            var accumulator = -1;
            try
            {
                accumulator = ExecuteBootCode(fileLines);
            }
            catch (InsufficientExecutionStackException ex)
            {
                Console.WriteLine($"DuplicateInstructionException; The accumulator value is '{ex.Message}'.\r\n");
            }

            Console.WriteLine($"BootCode executed with exit code '{accumulator}'.\r\n");
        }

        public static void PartTwo(string[] fileLines)
        {
            Console.WriteLine("========== AdventOfCode Day 8 - Part Two ==========");
            var accumulator = -1;
            try
            {
                var suspiciousLines = new List<int>() { 0, 7, 11, 13, 25, 33, 37, 38, 47, 50, 63, 65, 81, 84, 91, 95, 108, 124, 134, 138, 140, 144, 146, 153, 154, 160, 166, 178, 192, 206, 217, 221, 228, 230, 232, 236, 249, 251, 252, 254, 256, 261, 284, 285, 292, 298, 302, 303, 305, 311, 313, 315, 318, 324, 330, 331, 333, 362, 369, 371, 379, 390, 392, 402, 403, 419, 423, 424, 430, 434, 443, 445, 450, 457, 458, 467, 473, 486, 490, 491, 498, 507, 526, 527, 543, 544, 550, 566, 569, 571, 585, 587, 591, 608 };
                accumulator = ExecuteBootCode(fileLines, suspiciousLines);
            }
            catch (InsufficientExecutionStackException ex)
            {
                Console.WriteLine($"DuplicateInstructionException; The accumulator value is '{ex.Message}'.\r\n");
            }

            Console.WriteLine($"BootCode executed with exit code '{accumulator}'.\r\n");
        }

        private static int ExecuteBootCode(string[] fileLines, IEnumerable<int> debugLines = null)
        {
            var instructionSequence = new Dictionary<int, string>();
            var accumulator = 0;

            for (int i = 0; i < fileLines.Length;)
            {
                var lineParts = fileLines[i].Split(' ');
                var instruction = lineParts.First().Trim();
                var argument = lineParts.Last().Trim();

                if (debugLines != null && debugLines.First().Equals(i))
                {
                    if (instruction == "nop")
                    {
                        instruction = "jmp";
                    }
                    else if (instruction == "jmp")
                    {
                        instruction = "nop";
                    }
                }

                switch (instruction)
                {
                    /// nop stands for No OPeration - it does nothing. The instruction immediately below it is executed next.
                    case "nop":
                        try
                        {
                            instructionSequence.Add(i, fileLines[i]);
                            i++;
                        }
                        catch (Exception)
                        {
                            /// Debug Logging
                            //var stackTrace = instructionSequence.Where(ins => !ins.Value.Contains("acc")).OrderBy(ins => ins.Key).ToList();
                            //foreach (var trc in stackTrace)
                            //{
                            //    Console.WriteLine($"{trc}");
                            //}

                            if (debugLines != null)
                            {
                                var debugAccumulator = ExecuteBootCode(fileLines, debugLines.Skip(1));
                                if(debugAccumulator > 0)
                                {
                                    return debugAccumulator;
                                }
                            }

                            throw new InsufficientExecutionStackException($"{accumulator}");
                        }
                        break;

                    /// acc increases or decreases a single global value called the accumulator by the value given in the argument.
                    case "acc":
                        try
                        {                           
                            instructionSequence.Add(i, fileLines[i]);
                            if (int.TryParse(argument, out var acc))
                            {
                                accumulator += acc;
                                i++;
                                break;
                            }
                        }
                        catch (Exception)
                        {
                            /// Debug Logging
                            //var stackTrace = instructionSequence.Where(ins => !ins.Value.Contains("acc")).OrderBy(ins => ins.Key).ToList();
                            //foreach (var trc in stackTrace)
                            //{
                            //    Console.WriteLine($"{trc}");
                            //}

                            if (debugLines != null)
                            {
                                var debugAccumulator = ExecuteBootCode(fileLines, debugLines.Skip(1));
                                if (debugAccumulator > 0)
                                {
                                    return debugAccumulator;
                                }
                            }

                            throw new InsufficientExecutionStackException($"{accumulator}");
                        }
                        // unexpected error
                        break;

                    /// jmp jumps to a new instruction relative to itself.
                    case "jmp":
                        try
                        {
                            instructionSequence.Add(i, fileLines[i]);
                            if (int.TryParse(argument, out var jmp))
                            {
                                i += jmp;
                                break;
                            }
                        }
                        catch (Exception)
                        {
                            /// Debug logging
                            //var stackTrace = instructionSequence.Where(ins => !ins.Value.Contains("acc")).OrderBy(ins => ins.Key).ToList();
                            //foreach (var trc in stackTrace)
                            //{
                            //    Console.WriteLine($"{trc}");
                            //}

                            if (debugLines != null)
                            {
                                var debugAccumulator = ExecuteBootCode(fileLines, debugLines.Skip(1));
                                if (debugAccumulator > 0)
                                {
                                    return debugAccumulator;
                                }
                            }

                            throw new InsufficientExecutionStackException($"{accumulator}");
                        }
                        break;

                    default:
                        // Unexpected input
                        Console.WriteLine($"Completed BootCode execution.\r\n");
                        i = fileLines.Length;
                        break;
                }
            }

            return accumulator;
        }
    }
}
