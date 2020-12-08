using System.Collections.Generic;
using System.Linq;

namespace AoC2020.Solutions
{
    public class Day08 : Solution<int>
    {
        private List<Instruction> instructions;
        public int accumulator = 0;
        public int idx = 0;
        public List<int> visited = new List<int>();
        public Day08(string file) : base(file, "\r\n")
        {
            instructions = Input.Select(i => new Instruction
            {
                operation = i.Substring(0, 3),
                argument = (i.Substring(4, 1), int.Parse(i.Where(char.IsDigit).ToArray( )))
            }).ToList( );
        }

        public override int SolvePart1( )
        {            
            while ( !visited.Contains(idx) )
            {
                visited.Add(idx);
                idx = ExecuteInstruction(instructions[idx], idx);
            }

            return accumulator;
        }

        public override int SolvePart2( )
        {
            return 0;
        }

        public int ExecuteInstruction(Instruction i, int idx)
        {
            if ( i.operation.Equals("acc") )
            {
                accumulator = i.argument.sign.Equals("+") ?
                    accumulator + i.argument.value : accumulator - i.argument.value;
                idx += 1;
            }

            if ( i.operation.Equals("jmp") )
            {
                idx = i.argument.sign.Equals("+") ? idx + i.argument.value :
                    idx - i.argument.value;
            }

            if ( i.operation.Equals("nop") )
            {
                idx += 1;
            }
            return idx;
        }

        
    }



    public record Instruction
    {
        public string operation { get; init; }
        public (string sign, int value) argument { get; init; }
    }
}
