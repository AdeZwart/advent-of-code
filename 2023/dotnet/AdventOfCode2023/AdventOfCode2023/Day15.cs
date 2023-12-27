namespace AzW.AdventOfCode.Year2023
{
    public class Day15 : Day
    {
        private const char DASH = '-';
        private const char EQUALS = '=';

        public override object ExecutePart1()
        {
            var input = Input.TrimEnd('\r', '\n').Split(',');

            //input = "rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7\r\n".TrimEnd('\r', '\n').Split(',');

            var values = new List<int>();
            foreach (var entry in input)
            {
                var value = RunHashAlgorithm(entry);
                values.Add(value);
            }

            return values.Sum();
        }

        public override object ExecutePart2()
        {
            var input = Input.TrimEnd('\r', '\n').Split(',');

            //input = "rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7\r\n".TrimEnd('\r', '\n').Split(',');

            var boxes = new Box[265];
            foreach (var entry in input)
            {
                var separator = entry.Contains(EQUALS) ? EQUALS : DASH;
                var e = entry.Split(separator);

                var boxID = RunHashAlgorithm(e[0]);
                int? lensID;

                if (separator == EQUALS)
                {
                    var lens = new Lens() { Label = e.First(), FocalLength = int.Parse(e.Last()) };

                    boxes[boxID] = boxes[boxID] ?? new Box() { lenses = [] };

                    lensID = boxes[boxID].lenses.FindIndex(l => l.Label == e.First());
                    if (lensID > -1)
                    {
                        // Remove existing lens with the same label
                        boxes[boxID].lenses.RemoveAt((int)lensID);
                        // Add the lens
                        boxes[boxID].lenses.Insert((int)lensID, lens);
                    }
                    else
                    {
                        // Just add the lens to the box
                        boxes[boxID].lenses.Add(lens);
                    }

                    //Console.WriteLine($"After \"{entry}\":");
                    //PrintBoxes(boxes);

                    continue;
                }

                lensID = boxes[boxID]?.lenses?.FindIndex(l => l.Label == e.First());
                if (lensID != null && lensID > -1)
                {
                    boxes[boxID].lenses.RemoveAt((int)lensID);
                }

                //Console.WriteLine($"After \"{entry}\":");
                //PrintBoxes(boxes);
            }

            var focalPower = CalculateFocalPower(boxes);
            return focalPower;
        }

        /// <summary>
        /// Determine the ASCII code for the current character of the string.
        /// Increase the current value by the ASCII code you just determined.
        /// Set the current value to itself multiplied by 17.
        /// Set the current value to the remainder of dividing itself by 256.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private int RunHashAlgorithm(string item)
        {
            var value = 0;

            foreach (var c in item)
            {
                value += (int)c;
                value *= 17;
                value %= 256;
            }

            return value;
        }

        private void PrintBoxes(Box[] boxes)
        {
            foreach (var i in Enumerable.Range(0, boxes.Length))
            {
                if (boxes[i] != null && boxes[i].lenses.Count > 0)
                {
                    Console.Write($"Box {i}: ");
                    foreach (var lens in boxes[i].lenses)
                    {
                        Console.Write($"{lens} ");
                    }
                    Console.Write("\n");
                }
            }
            Console.Write('\n');
        }

        private long CalculateFocalPower(Box[] boxes)
        {
            var focalPower = new List<long>();

            foreach (var boxIndex in Enumerable.Range(1, boxes.Length))
            {
                var box = boxes[boxIndex - 1];
                if (box != null && box.lenses.Count > 0)
                {
                    foreach (var lensIndex in Enumerable.Range(1, box.lenses.Count))
                    {
                        var lens = box.lenses[lensIndex - 1];
                        focalPower.Add(boxIndex * lensIndex * lens.FocalLength);
                    }
                }
            }

            return focalPower.Sum();
        }

        private sealed record Box()
        {
            public List<Lens> lenses { get; set; }
        };

        private sealed record Lens()
        {
            public required string Label { get; init; }
            public required int FocalLength { get; init; }

            public override string ToString()
            {
                return $"[{Label} {FocalLength}]";
            }
        }
    }
}
