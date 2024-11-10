namespace AoC2016
{
    public class Day23 : Solution
    {
        private readonly Dictionary<string, long> registers = new();
        private List<string> instructions;
        public Day23(string file) : base(file, "\n")
        {
            registers.Add("a", 0);
            registers.Add("b", 0);
            registers.Add("c", 0);
            registers.Add("d", 0);
            instructions = Input.Select(s => s).ToList();
        }


        public override async Task<string> SolvePart1()
        {
            registers["a"] = 7;
            return ParseInstructions();
        }


        public override async Task<string> SolvePart2()
        {
            registers["a"] = 12;
            registers["b"] = 0;
            registers["c"] = 0;
            registers["d"] = 0;
            //so basically there are repeating loops in input lines 5,6,7, and later 10 which increment register A a LOT of times
            //to circumvent long running operation we simply replace the looping instructions with new nope instruction
            //and at the end do a multiplication instead (also new)
            //could probably not bother with nope instructions at all but offset jumps are too fiddly to figure that one out
            Input.RemoveRange(2, 8);
            Input.InsertRange(2, Enumerable.Repeat("nop", 7));
            instructions = Input.InsertAt(9, "mul b a a");
            return ParseInstructions();
        }

        private string ParseInstructions()
        {
            var pointer = 0;

            while (pointer < instructions.Count)
            {
                var parts = GetLineParts(pointer);
                var value = parts[0].Equals("nop") ? 0 :  char.IsLetter(parts[1][0]) ? registers[parts[1]] : int.Parse(parts[1]);

                switch (parts[0])
                {
                    case "nop":
                        pointer++;
                        break;

                    case "mul":
                        registers[parts[3]] = registers[parts[1]] * registers[parts[2]];
                        pointer++;
                        break;

                    case "tgl":
                        var idx = pointer + (int)registers[parts[1]];
                        if (idx >= instructions.Count)
                        {
                            pointer++;
                            continue;
                        }

                        var p = GetLineParts(idx);
                        var newOp = p.Length switch
                        {
                            2 => p[0].Equals("inc") ? "dec" : "inc",
                            3 => p[0].Equals("jnz") ? "cpy" : "jnz",
                            _ => string.Empty
                        };
                        instructions[idx] = instructions[idx].ReplaceAt(0, newOp);
                        pointer++;
                        break;

                    case "cpy":
                        registers[parts[2]] = value;
                        pointer++;
                        break;

                    case "jnz":
                        var offset = parts[2].HasInteger() ?
                            int.Parse(new string(parts[2].Where(char.IsDigit).ToArray())) :
                            (int)registers[parts[2]];

                        var jump = parts[2].Contains("-") ? -1 * offset : offset;
                        pointer += value != 0 ? jump : 1;
                        break;

                    case "inc":
                        registers[parts[1]]++;
                        pointer++;
                        break;

                    case "dec":
                        registers[parts[1]]--;
                        pointer++;
                        break;
                }
            }
            return registers["a"].ToString();
        }

        private string[] GetLineParts(int idx) => instructions[idx].Split(" ").Select(p => p.Trim()).ToArray();
    }
}