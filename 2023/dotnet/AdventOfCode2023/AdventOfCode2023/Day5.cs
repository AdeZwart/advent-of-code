using System.Text.RegularExpressions;

namespace AzW.AdventOfCode.Year2023
{
    public class Day5 : Day.NewLineSplitParsed<string>
    {
        private const string SEED_SOIL = "seed-to-soil";
        private const string SOIL_FERTILIZER = "soil-to-fertilizer";
        private const string FERTILIZER_WATER = "fertilizer-to-water";
        private const string WATER_LIGHT = "water-to-light";
        private const string LIGHT_TEMPERATURE = "light-to-temperature";
        private const string TEMPERATURE_HUMIDITY = "temperature-to-humidity";
        private const string HUMIDITY_LOCATION = "humidity-to-location";

        private const string PATTERN = @"\d+";
        private readonly Regex regex = new Regex(PATTERN);

        public override object ExecutePart1()
        {
            //Input = GetTestInput();

            var seeds = regex.Matches(Input[0]).Cast<Match>().Select(x => long.Parse(x.Value)).ToList();

            var lowestLocationNumber = GetLowestLocationNumber(seeds);

            return lowestLocationNumber;
        }

        public override object ExecutePart2()
        {
            Input = GetTestInput();

            var seeds = regex.Matches(Input[0]).Cast<Match>().Select(x => long.Parse(x.Value)).ToList();

            var seedStarts = new List<long>();
            var seedEnds = new List<long>();
            
            for (var i = 0; i < seeds.Count; i += 2)
            {
                seedStarts.Add(seeds[i]);
                seedEnds.Add((seeds[i] + seeds[i + 1]) - 1);
            }

            (var seed2soilMap, var soil2fertilizerMap, var fertilizer2waterMap, var water2lightMap, var light2temperatureMap, var temperature2humidityMap, var humidity2locationMap) = GetPlantingMaps();

            // Foreach SeedRange, see if it has any intersection in a seed2soilMap Range
            foreach(var s in Enumerable.Range(0, seedStarts.Count))
            {
                foreach(var r in Enumerable.Range(0, seed2soilMap.Sources.Count))
                {

                }
            }

            var lowestLocationNumber = GetLowestLocationNumber(seeds);

            return lowestLocationNumber;
        }

        private long GetLowestLocationNumber(List<long> seeds)
        {
            long lowestLocationNumber = -1;

            (var seed2soilMap, var soil2fertilizerMap, var fertilizer2waterMap, var water2lightMap, var light2temperatureMap, var temperature2humidityMap, var humidity2locationMap) = GetPlantingMaps();

            foreach (var seed in seeds)
            {
                var soil = FindMatchingNumber(seed, seed2soilMap);
                var fertilizer = FindMatchingNumber(soil, soil2fertilizerMap);
                var water = FindMatchingNumber(fertilizer, fertilizer2waterMap);
                var light = FindMatchingNumber(water, water2lightMap);
                var temp = FindMatchingNumber(light, light2temperatureMap);
                var hum = FindMatchingNumber(temp, temperature2humidityMap);
                var location = FindMatchingNumber(hum, humidity2locationMap);

                //Console.WriteLine($"Seed [{seed}], soil [{soil}], fertilizer [{fertilizer}], water [{water}], light [{light}], temperatur [{temp}], humidity [{hum}], location [{location}]");

                if (lowestLocationNumber == -1 || location < lowestLocationNumber)
                {
                    lowestLocationNumber = location;
                }
            }

            return lowestLocationNumber;
        }

        private (PlantingMap, PlantingMap, PlantingMap, PlantingMap, PlantingMap, PlantingMap, PlantingMap) GetPlantingMaps()
        {
            var input = Input.ToList();
            int startIndex = 0;

            var mapStart = input.FindIndex(startIndex, s => s.StartsWith(SEED_SOIL));
            var mapEnd = input.FindIndex(mapStart, s => string.IsNullOrWhiteSpace(s));
            var seed2soilMap = GetPlantingMap(mapStart, mapEnd);

            mapStart = input.FindIndex(startIndex, s => s.StartsWith(SOIL_FERTILIZER));
            mapEnd = input.FindIndex(mapStart, s => string.IsNullOrWhiteSpace(s));
            var soil2fertilizerMap = GetPlantingMap(mapStart, mapEnd);

            mapStart = input.FindIndex(startIndex, s => s.StartsWith(FERTILIZER_WATER));
            mapEnd = input.FindIndex(mapStart, s => string.IsNullOrWhiteSpace(s));
            var fertilizer2waterMap = GetPlantingMap(mapStart, mapEnd);

            mapStart = input.FindIndex(startIndex, s => s.StartsWith(WATER_LIGHT));
            mapEnd = input.FindIndex(mapStart, s => string.IsNullOrWhiteSpace(s));
            var water2lightMap = GetPlantingMap(mapStart, mapEnd);

            mapStart = input.FindIndex(startIndex, s => s.StartsWith(LIGHT_TEMPERATURE));
            mapEnd = input.FindIndex(mapStart, s => string.IsNullOrWhiteSpace(s));
            var light2temperatureMap = GetPlantingMap(mapStart, mapEnd);

            mapStart = input.FindIndex(startIndex, s => s.StartsWith(TEMPERATURE_HUMIDITY));
            mapEnd = input.FindIndex(mapStart, s => string.IsNullOrWhiteSpace(s));
            var temperature2humidityMap = GetPlantingMap(mapStart, mapEnd);

            mapStart = input.FindIndex(startIndex, s => s.StartsWith(HUMIDITY_LOCATION));
            var humidity2locationMap = GetPlantingMap(mapStart, Input.Length);

            return (seed2soilMap, soil2fertilizerMap, fertilizer2waterMap, water2lightMap, light2temperatureMap, temperature2humidityMap, humidity2locationMap);
        }
        
        private string[] GetTestInput()
        {
            return
            [
                "seeds: 79 14 55 13",
                "",
                "seed-to-soil map:",
                "50 98 2",
                "52 50 48",
                "",
                "soil-to-fertilizer map:",
                "0 15 37",
                "37 52 2",
                "39 0 15",
                "",
                "fertilizer-to-water map:",
                "49 53 8",
                "0 11 42",
                "42 0 7",
                "57 7 4",
                "",
                "water-to-light map:",
                "88 18 7",
                "18 25 70",
                "",
                "light-to-temperature map:",
                "45 77 23",
                "81 45 19",
                "68 64 13",
                "",
                "temperature-to-humidity map:",
                "0 69 1",
                "1 0 69",
                "",
                "humidity-to-location map:",
                "60 56 37",
                "56 93 4"
            ];
        }

        private static long FindMatchingNumber(long target, PlantingMap plantingMap)
        {
            foreach (var i in Enumerable.Range(0, plantingMap.Sources.Count))
            {
                var rangeStart = plantingMap.Sources[i];
                var rangeEnd = plantingMap.Sources[i] + plantingMap.Ranges[i] - 1;

                if (target >= rangeStart && target <= rangeEnd)
                {
                    var offset = plantingMap.Destinations[i] - plantingMap.Sources[i];

                    return target + offset;
                }
            }

            return target;
        }

        private PlantingMap GetPlantingMap(int startIndex, int EndIndex)
        {
            var destinations = new List<long>();
            var sources = new List<long>();
            var rangeLengths = new List<long>();

            foreach (var mapIndex in Enumerable.Range(startIndex + 1, EndIndex - startIndex - 1))
            {
                var map = regex.Matches(Input[mapIndex]).Cast<Match>().Select(x => long.Parse(x.Value)).ToArray();

                destinations.Add(map[0]);
                sources.Add(map[1]);
                rangeLengths.Add(map[2]);
            }

            return new PlantingMap()
            {
                Destinations = destinations,
                Sources = sources,
                Ranges = rangeLengths
            };
        }

        private sealed record PlantingMap()
        {
            public required List<long> Destinations { get; init; }
            public required List<long> Sources { get; init; }
            public required List<long> Ranges { get; init; }
        }

        private sealed record SeedMap()
        {
            public required List<long> SeedStart { get; init; }
            public required List<long> SeedRange { get; init; }
        }
    }
}
