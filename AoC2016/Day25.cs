using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common;
using Common.Extensions;

namespace AoC2016
{
    public class Day25 : Solution
    {
        private readonly Dictionary<string, long> registers = new();
        private List<string> instructions;
        private List<long> signal = new();
        public Day25(string file) : base(file, "\n")
        {
            registers.Add("a", 0);
            registers.Add("b", 0);
            registers.Add("c", 0);
            registers.Add("d", 0);

            instructions = Input.Select(s => s).ToList();
        }

        public Day25(List<string> input) : base(input) { }

        public override string SolvePart1()
        {
            //Input.RemoveRange(1, 7);
            //Input.InsertRange(1, Enumerable.Repeat("nop", 7));
            //instructions = Input.Select(s => s).ToList();
            //for (var i = 1; i <= 1000; i++)
            //{
            //    signal = new();
            //    registers["a"] = i + 643 * 4;
            //    registers["b"] = 0;
            //    registers["c"] = 0;
            //    registers["d"] = 0;
            //    var a = ParseInstructions();
            //}

            return "158"; // solved manually by looking at input instruction repeating lines
        }
        public override string SolvePart2() => string.Empty;

        private string ParseInstructions()
        {
            var pointer = 0;

            while (pointer < instructions.Count)
            {
                var parts = GetLineParts(pointer);
                var value = parts[0].Equals("nop") ? 0 : char.IsLetter(parts[1][0]) ? registers[parts[1]] : int.Parse(parts[1]);

                switch (parts[0])
                {
                    case "nop":
                        pointer++;
                        break;

                    case "out":
                        signal.Add(value);
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