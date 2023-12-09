namespace AzW.AdventOfCode.Year2023
{
    public class Day8 : Day.NewLineSplitParsed<string>
    {
        private const char LEFT = 'L';
        private const char RIGHT = 'R';

        public override object ExecutePart1()
        {
            var stepsToTheEnd = 0;

            var directionInstructions = new Queue<char>(Input[0]);
            var elements = ParseElements().OrderBy(e => e.ID);

            var currentElement = elements.First(e => e.ID == "AAA");
            var nextElement = string.Empty;

            while (directionInstructions.Count > 0)
            {
                // Get the first instruction from the queue
                var instruction = directionInstructions.Dequeue();

                nextElement = instruction switch
                {
                    LEFT => currentElement.Left,
                    RIGHT => currentElement.Right,
                    _ => throw new NotImplementedException(),
                };

                stepsToTheEnd++;

                // Add the instruction back to the end of the queue
                directionInstructions.Enqueue(instruction);

                // We've found the end
                if (nextElement == "ZZZ")
                {
                    break;
                }

                // Move to next element
                currentElement = elements.First(e => e.ID == nextElement);
            }

            return stepsToTheEnd;
        }

        public override object ExecutePart2()
        {
            //Input = GetTestInput();

            var stepsToTheEnd = 0;

            var directionInstructions = new Queue<char>(Input[0]);
            var elements = ParseElements().OrderBy(e => e.ID);

            var currentElements = elements.Where(e => e.ID.EndsWith('A')).Select(e => e).ToArray();

            while (directionInstructions.Count > 0)
            {
                //Console.WriteLine($"Step {stepsToTheEnd}: {string.Join(',', currentElements.Select(e => e.ID))}");

                // Get the first instruction from the queue
                var instruction = directionInstructions.Dequeue();

                foreach (var index in Enumerable.Range(0, currentElements.Count()))
                {
                    currentElements[index] = MoveToNext(instruction, currentElements[index], elements);
                }

                stepsToTheEnd++;

                // Are all nodes ending with 'Z'?
                if (currentElements.Count() == currentElements.Count(e => e.ID.EndsWith('Z')))
                {
                    break;
                }

                // Add the instruction back to the end of the queue
                directionInstructions.Enqueue(instruction);
            }

            return stepsToTheEnd;
        }

        private string[] GetTestInput()
        {
            return [
                "LR",
                "",
                "11A = (11B, XXX)",
                "11B = (XXX, 11Z)",
                "11Z = (11B, XXX)",
                "22A = (22B, XXX)",
                "22B = (22C, 22C)",
                "22C = (22Z, 22Z)",
                "22Z = (22B, 22B)",
                "XXX = (XXX, XXX)"
            ];
        }

        private Element MoveToNext(char instruction, Element current, IOrderedEnumerable<Element> elements)
        {
            var next = instruction switch
            {
                LEFT => current.Left,
                RIGHT => current.Right,
                _ => throw new NotImplementedException(),
            };

            return elements.First(e => e.ID == next);
        }

        private List<Element> ParseElements()
        {
            var elements = new List<Element>();
            foreach (var index in Enumerable.Range(2, Input.Length - 2))
            {
                var elementID = Input[index].Split('=')[0].Trim();
                var elementDirections = Input[index].Split('=')[1].Trim().Trim('(').Trim(')').Split(',');

                var element = new Element
                {
                    ID = elementID,
                    Left = elementDirections[0].Trim(),
                    Right = elementDirections[1].Trim()
                };

                elements.Add(element);
            }

            return elements;
        }

        private sealed record Element()
        {
            public required string ID { get; init; }
            public required string Left { get; init; }
            public required string Right { get; init; }
        }
    }
}
