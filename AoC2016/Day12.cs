using System.Collections.Generic;
using System.Linq;
using Common;

namespace AoC2016
{
    public class Day12 : Solution
    {
        private readonly Dictionary<string, long> registers = new();
        public Day12(string file) : base(file, "\n")
        {
            registers.Add("a", 0);
            registers.Add("b", 0);
            registers.Add("c", 0);
            registers.Add("d", 0);
        }

        public override string SolvePart1( ) => ParseInstructions();

        public override string SolvePart2()
        {
            registers["c"] = 1;
            return ParseInstructions();
        }


        private string ParseInstructions()
        {
            var pointer = 0;

            while (pointer < Input.Count)
            {
                var line = Input[pointer];
                var parts = line.Split(" ").Select(p => p.Trim()).ToList();
                var value = char.IsLetter(parts[1][0]) ? registers[parts[1]] : int.Parse(parts[1]);

                switch (parts[0])
                {
                    case "cpy":
                        registers[parts[2]] = value;
                        pointer++;
                        break;
                    case "jnz":
                        var offset = int.Parse(new string(parts[2].Where(char.IsDigit).ToArray()));
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
    }
}