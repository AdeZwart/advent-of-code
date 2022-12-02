using Tidy.AdventOfCode;

namespace AzW.AdventOfCode2021.Year2021
{
    public class Day24 : Day<List<Instruction>>
    {
        public override List<Instruction> ParseInput(string rawInput)
        {
            var splittedInput = rawInput.Split('\n');

            var instructions = new List<Instruction>();
            foreach (var input in splittedInput)
            {
                instructions.Add(new Instruction(input.Split(' ')));
            }

            return instructions;
        }

        public override object ExecutePart1()
        {
            var alu = new Alu();

            for (long i = 99999991199927; i >= 11111111111111; i--)
            {
                Console.WriteLine($"Validating model number: {i}");
                var modelNumber = new List<int>();
                foreach (var c in i.ToString())
                {
                    modelNumber.Add(int.Parse($"{c}"));
                }

                alu = ProcessInstructions(alu, modelNumber, Input);

                //Console.WriteLine($"W={alu.W}, X={alu.X}, Y={alu.Y} Z={alu.Z}");

                if (alu.Z == 0)
                {
                    // Found a valid model Number
                    return i;
                }
            }

            return base.ExecutePart1();
        }

        public Alu ProcessInstructions(Alu alu, List<int> modelNumber, List<Instruction> instructions)
        {
            foreach (var instruction in instructions)
            {
                //Console.WriteLine($"{instruction.Operation} {instruction.Target} {instruction.Value}");
                switch (instruction.Operation)
                {
                    case "inp":
                        alu.W = modelNumber.First();
                        modelNumber.RemoveAt(0);
                        break;

                    case "add":
                        alu = Add(alu, instruction.Target, instruction.Value);
                        break;

                    case "mul":
                        alu = Mul(alu, instruction.Target, instruction.Value);
                        break;

                    case "div":
                        alu = Div(alu, instruction.Target, instruction.Value);
                        break;

                    case "mod":
                        alu = Mod(alu, instruction.Target, instruction.Value);
                        break;

                    case "eql":
                        alu = Eql(alu, instruction.Target, instruction.Value);
                        break;
                }
            }

            return alu;
        }

        public Alu Mul(Alu alu, string target, string value)
        {
            if (!long.TryParse(value, out long val))
            {
                var sourceProp = alu.GetType().GetProperty(value.ToUpper());
                if (sourceProp != null)
                {
                    val = long.Parse($"{sourceProp.GetValue(alu, null)}");
                }
            }

            var targetProp = alu.GetType().GetProperty(target.ToUpper());
            if (targetProp != null && targetProp.CanWrite)
            {
                var targetValue = long.Parse($"{targetProp.GetValue(alu)}");
                // mul a b - Multiply the value of a by the value of b, then store the result in variable a.
                targetProp.SetValue(alu, targetValue * val, null);
            }

            return alu;
        }

        public Alu Add(Alu alu, string target, string value)
        {
            if (!long.TryParse(value, out long val))
            {
                var sourceProp = alu.GetType().GetProperty(value.ToUpper());
                if (sourceProp != null)
                {
                    val = long.Parse($"{sourceProp.GetValue(alu, null)}");
                }
            }

            var targetProp = alu.GetType().GetProperty(target.ToUpper());
            if (targetProp != null && targetProp.CanWrite)
            {
                var targetValue = long.Parse($"{targetProp.GetValue(alu)}");
                // add a b - Add the value of a to the value of b, then store the result in variable a
                targetProp.SetValue(alu, targetValue + val, null);
            }

            return alu;
        }

        public Alu Div(Alu alu, string target, string value)
        {
            if (!long.TryParse(value, out long val))
            {
                var sourceProp = alu.GetType().GetProperty(value.ToUpper());
                if (sourceProp != null)
                {
                    val = long.Parse($"{sourceProp.GetValue(alu, null)}");
                }
            }

            var targetProp = alu.GetType().GetProperty(target.ToUpper());
            if (targetProp != null && targetProp.CanWrite)
            {
                var targetValue = long.Parse($"{targetProp.GetValue(alu)}");
                // div a b - Divide the value of a by the value of b, truncate the result to an integer, then store the result in variable a.
                // (Here, "truncate" means to round the value toward zero.)
                targetProp.SetValue(alu, targetValue / val, null);
            }

            return alu;
        }

        public Alu Mod(Alu alu, string target, string value)
        {
            if (!long.TryParse(value, out long val))
            {
                var sourceProp = alu.GetType().GetProperty(value.ToUpper());
                if (sourceProp != null)
                {
                    val = long.Parse($"{sourceProp.GetValue(alu, null)}");
                }
            }

            var targetProp = alu.GetType().GetProperty(target.ToUpper());
            if (targetProp != null && targetProp.CanWrite)
            {
                var targetValue = long.Parse($"{targetProp.GetValue(alu)}");
                // mod a b - Divide the value of a by the value of b, then store the remainder in variable a. (This is also called the modulo operation.)
                targetProp.SetValue(alu, targetValue % val, null);
            }

            return alu;
        }

        public Alu Eql(Alu alu, string target, string value)
        {
            if (!long.TryParse(value, out long val))
            {
                var sourceProp = alu.GetType().GetProperty(value.ToUpper());
                if (sourceProp != null)
                {
                    val = long.Parse($"{sourceProp.GetValue(alu, null)}");
                }
            }

            var targetProp = alu.GetType().GetProperty(target.ToUpper());
            if (targetProp != null && targetProp.CanWrite)
            {
                var targetValue = long.Parse($"{targetProp.GetValue(alu)}");
                // eql a b - If the value of a and b are equal, then store the value 1 in variable a. Otherwise, store the value 0 in variable a.
                var newVal = (targetValue == val) ? 1 : 0;                
                targetProp.SetValue(alu, newVal, null);
            }

            return alu;
        }

    }

    public class Alu
    {
        public long W { get; set; }
        public long X { get; set; }
        public long Y { get; set; }
        public long Z { get; set; }

        public Alu()
        {
            W = 0L;
            X = 0L;
            Y = 0L;
            Z = 0L;
        }
    }

    public class Instruction
    {
        public string Operation { get; set; }
        public string Target { get; set; }
        public string Value { get; set; }

        public Instruction(string[] input)
        {
            Operation = input.First();
            Target = input.Skip(1).First();
            Value = (input.Length > 2) ? input.Last() : String.Empty;
        }
    }
}
