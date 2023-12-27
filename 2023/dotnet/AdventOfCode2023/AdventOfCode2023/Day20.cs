namespace AzW.AdventOfCode.Year2023
{
    public class Day20 : Day.NewLineSplitParsed<string>
    {
        private const char FLIP_FLOP = '%';
        private const char CONJUNUCTION = '&';
        private const string BROADCASTER = "broadcaster";

        public override object ExecutePart1()
        {
            Input = GetTestInput1();
            //Input = GetTestInput2();

            var modules = ExtractModules(Input);

            var lowPulseCount = 0;
            var highPulseCount = 0;

            var cyclesToComplete = 0;

            while (true)
            {
                Console.WriteLine($"button -low-> broadcaster");
                lowPulseCount++;

                var currentModule = modules.First(m => m.Name == BROADCASTER);

                foreach (var destModuleName in currentModule.Destinations)
                {
                    var destinationModule = modules.First(m => m.Name == destModuleName);

                    var pulseType = string.Empty;
                    if (currentModule.Name == BROADCASTER)
                    {
                        pulseType = "low";
                        lowPulseCount++;
                    }

                    Console.WriteLine($"{currentModule.Name} -{pulseType}-> {destinationModule.Name}");
                }

                if (modules.Where(m => m.Type == ModuleType.FLIP_FLOP).ToList().TrueForAll(m => !m.IsOn))
                {
                    break;
                }
            }

            throw new NotImplementedException();

            return lowPulseCount * highPulseCount;
        }

        public override object ExecutePart2()
        {
            throw new NotImplementedException();
        }

        private static List<Module> ExtractModules(string[] input)
        {
            var modules = new List<Module>();

            foreach (var line in input)
            {
                var type = line[0] switch
                {
                    FLIP_FLOP => ModuleType.FLIP_FLOP,
                    CONJUNUCTION => ModuleType.CONJUNCTION,
                    _ => ModuleType.BROADCASTER
                };

                var module = new Module
                {
                    Type = type,
                    Name = line.Split(" -> ")[0].Trim('%').Trim('&'),
                    Destinations = line.Split(" -> ")[1].Split(',').Select(d => d.Trim())
                };

                modules.Add(module);
            }

            return modules;
        }

        private sealed record Module()
        {
            public required ModuleType Type { get; init; }
            public required string Name { get; init; }
            public required IEnumerable<string> Destinations { get; set; }
            public bool IsOn { get; set; } = false;
        }

        private string[] GetTestInput1()
        {
            return
            [
                "broadcaster -> a, b, c",
                "%a -> b",
                "%b -> c",
                "%c -> inv",
                "&inv -> a"
            ];
        }

        private string[] GetTestInput2()
        {
            return
            [
                "broadcaster -> a",
                "%a -> inv, con",
                "&inv -> b",
                "%b -> con",
                "&con -> output"
            ];
        }

        enum ModuleType
        {
            BROADCASTER,
            FLIP_FLOP,
            CONJUNCTION
        }
    }
}
