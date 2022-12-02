using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace adventofcode
{
    public static class Day4
    {
        private static readonly List<string> requiredFields = new List<string>() { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
        private static readonly List<string> optionalFields = new List<string>() { "cid" };

        public static void PartOne(string[] fileLines)
        {
            Console.WriteLine("========== AdventOfCode Day 4 - Part One ==========");

            int validCount = validatePassports(fileLines, false);

            Console.WriteLine($"The input contains {validCount} valid passports");
            Console.WriteLine("\r\n");
        }

        public static void PartTwo(string[] fileLines)
        {
            Console.WriteLine("========== AdventOfCode Day 4 - Part Two ==========");

            int validCount = validatePassports(fileLines, true);

            Console.WriteLine($"The input contains {validCount} valid passports");
            Console.WriteLine("\r\n");
        }

        private static int validatePassports(string[] fileLines, bool validateFieldValues)
        {
            int validCount = 0;

            var passport = string.Empty;
            for (int i = 0; i < fileLines.Length; i++)
            {
                var line = fileLines[i];
                if (line.Length > 0)
                {
                    passport = $"{passport} {line}";
                }

                // If we hit an empty line || when it's the last entry of the input
                if (line.Length == 0 || i == fileLines.Length - 1)
                {
                    // Validate the password
                    var fields = passport.Trim().Split(' ');
                    var fieldNames = new List<string>();
                    foreach (var field in fields)
                    {
                        if (validateFieldValues)
                        {
                            if (hasValidFieldValue(field))
                            {
                                fieldNames.Add(field.Split(':').First().Trim());
                            }
                        }
                        else
                        {
                            fieldNames.Add(field.Split(':').First().Trim());
                        }
                    }

                    var hasRequiredFields = Enumerable.SequenceEqual(fieldNames.Intersect(requiredFields).OrderBy(e => e), requiredFields.OrderBy(e => e));
                    if (hasRequiredFields)
                    {
                        validCount++;
                    }

                    // Reset for next passport.
                    passport = string.Empty;
                }
            }

            return validCount;
        }

        private static bool hasValidFieldValue(string field)
        {
            var parts = field.Split(':');
            var fieldName = parts.First().Trim();
            var fieldValue = parts.Last().Trim();

            switch (fieldName)
            {
                /// byr (Birth Year) - four digits; at least 1920 and at most 2002.
                case "byr":
                    return int.TryParse(fieldValue, out var byr) && (byr >= 1920 && byr <= 2002);

                /// iyr (Issue Year) - four digits; at least 2010 and at most 2020.
                case "iyr":
                    return int.TryParse(fieldValue, out var iyr) && (iyr >= 2010 && iyr <= 2020);

                /// eyr (Expiration Year) - four digits; at least 2020 and at most 2030.
                case "eyr":
                    return int.TryParse(fieldValue, out var eyr) && (eyr >= 2020 && eyr <= 2030);
                
                /// hgt (Height) - a number followed by either cm or in:
                ///  -- If cm, the number must be at least 150 and at most 193.
                ///  -- If in, the number must be at least 59 and at most 76.
                case "hgt":
                    var unit = fieldValue.Substring(fieldValue.Length - 2, 2);
                    var value = fieldValue[0..^2];
                    if (unit == "in")
                    {
                        return int.TryParse(value, out var hgt) && (hgt >= 59 && hgt <= 76);
                    }
                    else if (unit == "cm")
                    {
                        return int.TryParse(value, out var hgt) && (hgt >= 150 && hgt <= 193);
                    }

                    return false;

                /// hcl (Hair Color) - a # followed by exactly six characters 0-9 or a-f.
                case "hcl":
                    var pattern = "^#([a-fA-F0-9]{6}|[a-fA-F0-9]{3})$";
                    var rg = new Regex(pattern);
                    return rg.Match(fieldValue).Success;

                /// ecl (Eye Color) - exactly one of: amb blu brn gry grn hzl oth.
                case "ecl":
                    var colors = new List<string>() { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
                    return colors.Contains(fieldValue);

                /// pid (Passport ID) - a nine-digit number, including leading zeroes.
                case "pid":
                    return fieldValue.Length == 9 && int.TryParse(fieldValue, out var _);

                /// cid (Country ID) - ignored, missing or not.
                case "cid":
                    return true;

                default:
                    return false;
            }
        }
    }
}
