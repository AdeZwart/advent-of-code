using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace adventofcode
{
    public static class Day21
    {
        private static Stopwatch stopWatch = new Stopwatch();

        public static void PartOne(string[] input)
        {
            stopWatch.Restart();
            Console.WriteLine("========== AdventOfCode Day 21 - Part One ==========");

            var result = FindFoodWithoutAllergens(input);

            Console.WriteLine($"The answer is: {result}");
            stopWatch.Stop();
            Console.WriteLine($"=> found in {stopWatch.Elapsed:mm\\:ss\\:ffff}\r\n");
        }

        public static void PartTwo(string[] input)
        {
            stopWatch.Restart();
            Console.WriteLine("========== AdventOfCode Day 21 - Part Two ==========");

            var foodList = input.Select(x => x.Split(" (")).ToDictionary(x => x.First().Trim().Split(' ').ToList(), x => x.Last().Replace("contains", "").Trim('(', ')', ' ').Split(", ").ToList());

            var uniqueAllergens = RetrieveAllergenMapping(foodList).OrderBy(a => a.Key).SelectMany(a => a.Value);

            var result = string.Join(',', uniqueAllergens);

            Console.WriteLine($"The answer is: {result}");
            stopWatch.Stop();
            Console.WriteLine($"=> found in {stopWatch.Elapsed:mm\\:ss\\:ffff}\r\n");
        }

        private static int FindFoodWithoutAllergens(IEnumerable<string> input)
        {
            var foodList = input.Select(x => x.Split(" (")).ToDictionary(x => x.First().Trim().Split(' ').ToList(), x => x.Last().Replace("contains", "").Trim('(', ')', ' ').Split(", ").ToList());

            var uniqueAllergens = RetrieveAllergenMapping(foodList).SelectMany(a => a.Value);

            var ingredients = foodList.SelectMany(x => x.Key).ToList();

            var nonallergenIngredients = ingredients.Where(i => uniqueAllergens.All(a => a != i));

            return nonallergenIngredients.Count();
        }

        private static Dictionary<string, List<string>> RetrieveAllergenMapping(Dictionary<List<string>, List<string>> foodList)
        {
            var uniqueAllergens = foodList.SelectMany(x => x.Value).Distinct().ToDictionary(x => x, x => new List<string>());

            foreach (var ingredientsList in foodList)
            {
                foreach (var allergen in ingredientsList.Value)
                {
                    if (!uniqueAllergens[allergen].Any())
                    {
                        uniqueAllergens[allergen] = ingredientsList.Key;
                        continue;
                    }

                    uniqueAllergens[allergen] = uniqueAllergens[allergen].Intersect(ingredientsList.Key).ToList();
                }
            }

            //// Eliminate allergens that are already unique untill they are all unique
            while (uniqueAllergens.Any(ua => ua.Value.Count > 1))
            {
                var mappedIngredients = uniqueAllergens.Where(ua => ua.Value.Count == 1).SelectMany(ua => ua.Value);

                foreach(var allergen in uniqueAllergens.Where(ua => ua.Value.Count > 1).ToList())
                {                    
                    uniqueAllergens[allergen.Key] = allergen.Value.Where(v => !mappedIngredients.Contains(v)).ToList();
                }
            }

            return uniqueAllergens;
        }
    }
}
