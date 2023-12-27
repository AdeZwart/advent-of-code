using System.Text.RegularExpressions;

namespace AzW.AdventOfCode.Year2023
{
    public class Day19 : Day.NewLineSplitParsed<string>
    {
        private const string RULE_PATTERN = @"^[a-z]+{";
        private const string PART_PATTERN = @"^{x=\d";

        private const string ACCEPT = "A";
        private const string REJECT = "R";
        private const char SMALLER_THAN = '<';
        private const char GREATER_THAN = '>';

        public override object ExecutePart1()
        {
            //Input = GetTestInput();

            var ruleRegex = new Regex(RULE_PATTERN);
            var rules = Input.Where(i => ruleRegex.IsMatch(i)).Select(i => ConvertToSortingInstruction(i)).ToList();

            var partRegex = new Regex(PART_PATTERN);
            var parts = Input.Where(i => partRegex.IsMatch(i)).Select(i => ConvertToMachinePart(i)).ToList();

            var acceptedParts = parts.Where(p => IsAccepted(p, rules));

            var total = acceptedParts.Sum(a => a.GetMachinePartScoreTotal());

            //throw new NotImplementedException();

            return total;
        }

        public override object ExecutePart2()
        {
            throw new NotImplementedException();
        }

        private static bool IsAccepted(MachinePart part, List<SortingInstruction> rules)
        {
            Console.Write($"{part}:");

            var nextRule = rules.First(r => r.Name == "in");

            while (true)
            {
                Console.Write($"{nextRule.Name} -> ");
                foreach (var rule in nextRule.Rules)
                {
                    var ruleParts = rule.Split(':');

                    if (rule.Contains(SMALLER_THAN))
                    {
                        var instruction = ruleParts[0].Split(SMALLER_THAN);
                        var valueToCheck = (long)part.GetType().GetProperty(instruction[0], System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).GetValue(part, null);

                        if (valueToCheck < long.Parse(instruction[1]))
                        {
                            var result = ruleParts[1];
                            if (result == REJECT)
                            {
                                Console.Write($"{REJECT}");
                                Console.Write('\n');
                                return false;
                            }

                            if (result == ACCEPT)
                            {
                                Console.Write($"{ACCEPT}");
                                Console.Write('\n');
                                return true;
                            }

                            nextRule = rules.First(r => r.Name == result);
                            break;
                        }
                    }
                    else if (rule.Contains(GREATER_THAN))
                    {
                        var instruction = ruleParts[0].Split(GREATER_THAN);
                        var valueToCheck = (long)part.GetType().GetProperty(instruction[0], System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).GetValue(part, null);

                        if (valueToCheck > long.Parse(instruction[1]))
                        {
                            var result = ruleParts[1];
                            if (result == REJECT)
                            {
                                Console.Write($"{REJECT}");
                                Console.Write('\n');
                                return false;
                            }

                            if (result == ACCEPT)
                            {
                                Console.Write($"{ACCEPT}");
                                Console.Write('\n');
                                return true;
                            }

                            nextRule = rules.First(r => r.Name == result);
                            break;
                        }
                    }
                    else
                    {
                        if (rule == ACCEPT)
                        {
                            Console.Write($"{ACCEPT}");
                            Console.Write('\n');
                            return true;
                        }

                        if (rule == REJECT)
                        {
                            Console.Write($"{REJECT}");
                            Console.Write('\n');
                            return false;
                        }

                        nextRule = rules.First(r => r.Name == rule);
                    }
                }
            }
        }

        private static SortingInstruction ConvertToSortingInstruction(string input)
        {
            var instruction = input.Split('{');
            var rules = instruction[1].TrimEnd('}').Split(',');

            return new SortingInstruction() { Name = instruction[0], Rules = rules };
        }

        private static MachinePart ConvertToMachinePart(string input)
        {
            var rslt = input.TrimStart('{').TrimEnd('}').Split(',');

            return new MachinePart(GetRating(rslt[0]), GetRating(rslt[1]), GetRating(rslt[2]), GetRating(rslt[3]));
        }

        private static long GetRating(string input)
        {
            return long.Parse(input.Split('=')[1]);
        }

        private sealed record SortingInstruction()
        {
            public required string Name { get; init; }
            public required IEnumerable<string> Rules { get; init; }
        }

        private sealed record MachinePart(long X, long M, long A, long S)
        {
            public long GetMachinePartScoreTotal()
            {
                return X + M + A + S;
            }

            public override string ToString()
            {
                return $"[x={X},m={M},a={A},s={S}]";
            }
        }

        private string[] GetTestInput()
        {
            return
            [
                "px{a<2006:qkq,m>2090:A,rfg}",
                "pv{a>1716:R,A}",
                "lnx{m>1548:A,A}",
                "rfg{s<537:gd,x>2440:R,A}",
                "qs{s>3448:A,lnx}",
                "qkq{x<1416:A,crn}",
                "crn{x>2662:A,R}",
                "in{s<1351:px,qqz}",
                "qqz{s>2770:qs,m<1801:hdj,R}",
                "gd{a>3333:R,R}",
                "hdj{m>838:A,pv}",
                "",
                "{x=787,m=2655,a=1222,s=2876}",
                "{x=1679,m=44,a=2067,s=496}",
                "{x=2036,m=264,a=79,s=2244}",
                "{x=2461,m=1339,a=466,s=291}",
                "{x=2127,m=1623,a=2188,s=1013}"
            ];
        }
    }

}
