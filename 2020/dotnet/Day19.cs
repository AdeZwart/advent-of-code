using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace adventofcode
{
    public static class Day19
    {
        private static Stopwatch stopWatch = new Stopwatch();

        public static void PartOne(string[] fileLines)
        {
            stopWatch.Restart();
            Console.WriteLine("========== AdventOfCode Day 19 - Part One ==========");

            var validMessageCounter = 0;

            var rules = GetRules(fileLines);
            var decodedRuleZero = GetDecodedRule(rules, 0);
            var messages = GetMessages(fileLines);

            foreach (var message in messages)
            {
                if (decodedRuleZero.Any(m => m.Equals(message)))
                {
                    validMessageCounter++;
                }
            }

            Console.WriteLine($"'{validMessageCounter}' valid messages found.\r\n");
            stopWatch.Stop();
            Console.WriteLine($"=> found in {stopWatch.Elapsed:mm\\:ss\\:ffff}\r\n");
        }

        public static void PartTwo(string[] fileLines)
        {
            stopWatch.Restart();
            Console.WriteLine("========== AdventOfCode Day 19 - Part Two ==========");

            var result = "";

            Console.WriteLine($"{result}.\r\n");
            stopWatch.Stop();
            Console.WriteLine($"=> found in {stopWatch.Elapsed:mm\\:ss\\:ffff}\r\n");
        }

        private static IEnumerable<string> GetDecodedRule(Dictionary<int, string> rules, int index)
        {
            var decodedRule = new List<string>();

            //var charRuleA = rules.Where(r => r.Value.Any(c => c.Equals("\"a\""))).Select(r => new KeyValuePair<int, string>(r.Key, r.Value.Trim('"'))).FirstOrDefault();
            //var charRuleB = rules.Where(r => r.Value.Any(c => c.Equals("\"b\""))).Select(r => new KeyValuePair<int, string>(r.Key, r.Value.Trim('"'))).FirstOrDefault();

            var ruleToDecode = rules.First(r => r.Key.Equals(index));

            if (ruleToDecode.Value.Any(c => c.Equals('"')))
            {
                // Matching character rule
                yield return ruleToDecode.Value.Trim('"');
            }

            // This rule has 2 rules!
            if (ruleToDecode.Value.Any(r => r.Equals('|')))
            {
                var ruleParts = ruleToDecode.Value.Split('|');


            }
            else
            {
                var rslt = GetDecodedRule(rules, ruleToDecode.Key);
                foreach(var r in rslt)
                {
                    yield return r;
                }
            }
        }

        private static Dictionary<int, string> GetRules(string[] fileLines)
        {
            var data = new List<string>(fileLines);

            var rawRules = fileLines.Take(data.FindIndex(d => string.IsNullOrWhiteSpace(d)));            

            var rules = rawRules.Select(x => x.Split(':')).ToDictionary(x => int.Parse(x.First().Trim()), x => x.Last().Trim());

            return rules;
        }

        private static IEnumerable<string> GetMessages(string[] fileLines)
        {
            var data = new List<string>(fileLines);

            var messages = fileLines.Skip(data.FindIndex(d => string.IsNullOrWhiteSpace(d)) + 1).Take(fileLines.Length);

            return messages;
        }

        //public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        //{
        //    return listToClone.Select(item => (T)item.Clone()).ToList();
        //}
    }
}
