using adventofcode.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace adventofcode
{
    public static class Day7
    {
        private const string SHINY_GOLD_BAG = "shiny gold bag";
        private const string CONTAIN = " contain ";
        private const string NO_OTHER_BAGS = "no other bags";

        public static void PartOne(string[] fileLines)
        {
            Console.WriteLine("========== AdventOfCode Day 7 - Part One ==========");

            var total = GetColorsAvailableForMyShinyGoldBag(fileLines);

            Console.WriteLine($"There are a total of '{total}' bag colors available to contain at least one shiny gold bag.\r\n");
        }

        public static void PartTwo(string[] fileLines)
        {
            Console.WriteLine("========== AdventOfCode Day 7 - Part Two ==========");

            var total = GetIndividualBagsInsideShinyGoldBag(fileLines);

            Console.WriteLine($"'{total}' individual bags are required inside my single shiny gold bag.\r\n");
        }

        private static int GetColorsAvailableForMyShinyGoldBag(string[] fileLines)
        {
            var coloredBags = GetBagCollection(fileLines);

            foreach (var bag in coloredBags)
            {
                bag.CanContainShinyGoldBag = CanContainShinyGoldBag(bag, coloredBags);
            }

            return coloredBags.Count(b => b.CanContainShinyGoldBag);
        }

        private static int GetIndividualBagsInsideShinyGoldBag(string[] fileLines)
        {
            var coloredBags = GetBagCollection(fileLines);

            var goldBag = coloredBags.FirstOrDefault(b => b.BagColor.Contains(SHINY_GOLD_BAG));
            var rslt = GetIndividualBagCount(goldBag, coloredBags);            

            return rslt;
        }

        private static HashSet<Bag> GetBagCollection(string[] fileLines)
        {
            var coloredBags = new HashSet<Bag>();

            foreach (var line in fileLines)
            {
                var bagColor = line.Split(CONTAIN).First();
                var innerBags = line.Split(CONTAIN).Last().Split(',');

                // Try to get the bag on the existing collection
                var bag = coloredBags.FirstOrDefault(cb => cb.BagColor.Equals(bagColor));
                if (bag == null)
                {
                    // Add bag to the collection
                    bag = new Bag()
                    {
                        BagColor = bagColor
                    };
                }

                if (bag.InnerBags == null)
                {
                    bag.InnerBags = new List<KeyValuePair<string, int>>();
                }

                foreach (var innerBag in innerBags)
                {
                    int.TryParse(innerBag.Trim().First().ToString(), out var count);
                    var color = innerBag.Trim().Substring(2).Trim().Trim('.');

                    if (color == NO_OTHER_BAGS)
                    {
                        continue;
                    }

                    var newInnerBag = new KeyValuePair<string, int>(color, count);
                    if (!bag.InnerBags.Any(ib => ib.Equals(newInnerBag)))
                    {
                        bag.InnerBags.Add(newInnerBag);
                    }
                }

                coloredBags.Add(bag);
            }

            return coloredBags;
        }

        private static bool CanContainShinyGoldBag(Bag bag, HashSet<Bag> bagCollection)
        {
            // If there is no bag or the bag doesn't have inner bags
            if (bag == null || bag.InnerBags == null)
            {
                return false;
            }

            if (bag.InnerBags.Any(ib => ib.Key.Contains(SHINY_GOLD_BAG)))
            {
                return true;
            }

            // Recursively look if any of the inner bags has a bag that can contain the shiny gold bag
            foreach (var innerBag in bag.InnerBags)
            {
                var bagOnCollection = bagCollection.FirstOrDefault(b => b.BagColor.Contains(innerBag.Key));
                if (bagOnCollection != null && CanContainShinyGoldBag(bagOnCollection, bagCollection))
                {
                    return true;
                }
            }

            return false;
        }

        private static int GetIndividualBagCount(Bag bag, HashSet<Bag> bagCollection)
        {
            var individualCount = 0;

            // If there is no bag or the bag doesn't have inner bags
            if (bag == null || bag.InnerBags == null)
            {
                return individualCount;
            }

            foreach (var innerBag in bag.InnerBags)
            {
                if (innerBag.Key.Contains("other bag"))
                {
                    continue;
                }

                var individualInnerBagCount = 0;
                var bagOnCollection = bagCollection.FirstOrDefault(b => b.BagColor.Contains(innerBag.Key));
                if (bagOnCollection != null && bagOnCollection.InnerBags.Any())
                {
                    individualInnerBagCount = GetIndividualBagCount(bagOnCollection, bagCollection);
                }
                
                individualCount += (individualInnerBagCount == 0) ? individualInnerBagCount : innerBag.Value * individualInnerBagCount ;                

                // Also count the amount of this innerbag
                individualCount += innerBag.Value;
            }

            return individualCount;
        }
    }
}
