using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Common.Extensions;

namespace AoC2021
{
    public class Day02 : Solution
    {
        private readonly List<(string op, int v)> instructions;

        public Day02(string file) : base(file)
        {
            instructions = Input.Select(i =>
            {
                var parts = i.Split(" ");
                return (parts[0], int.Parse(parts[1]));
            }).ToList();
        }

        public override async Task<string> SolvePart1() => instructions.Aggregate((h: 0, d: 0),
            (position, instruction) => instruction.op switch
            {
                "forward" => position.Add(instruction.v, 0),
                "up" => position.Add(0, -instruction.v),
                "down" => position.Add(0, instruction.v)
            }).MultiplyComponents().ToString();


        public override async Task<string> SolvePart2()
        {
            var final = instructions.Aggregate((h: 0, d: 0, a: 0),
                (position, instruction) => instruction.op switch
                {
                    "forward" => position.Add(instruction.v, position.a * instruction.v, 0),
                    "up" => position.Add(0, 0, -instruction.v),
                    "down" => position.Add(0, 0, instruction.v)
                });

            return (final.h * final.d).ToString();
        }
    }
}