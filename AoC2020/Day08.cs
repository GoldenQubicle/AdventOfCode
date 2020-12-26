using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace AoC2020
{
    public class Day08 : Solution
    {
        private List<Instruction> instructions;
        public int accumulator = 0;
        public int idx = 0;
        public List<int> visited = new List<int>();
        public Day08(string file) : base(file)
        {
            instructions = GetInstructions( );
        }

        public List<Instruction> GetInstructions() => Input.Select(i => new Instruction
        {
            operation = i.Substring(0, 3),
            argument = (i.Substring(4, 1), int.Parse(i.Where(char.IsDigit).ToArray( )))
        }).ToList( );

        public override string SolvePart1( )
        {            
            while ( !visited.Contains(idx) )
            {
                visited.Add(idx);
                idx = ExecuteInstruction(instructions[idx], idx);
            }
            return accumulator.ToString( );
        }

        public override string SolvePart2( )
        {
            var changed = new List<int>( );
            var hasChanged = false;
            
            while ( idx < instructions.Count )
            {
                var current = instructions[idx];

                if(!changed.Contains(idx) && !hasChanged &&
                    (current.operation.Equals("jmp") ||
                     current.operation.Equals("nop") ))
                {
                    hasChanged = true;
                    changed.Add(idx);
                    instructions[idx] = current.operation switch
                    {
                        "jmp" => current with { operation = "nop" },
                        "nop" => current with { operation = "jmp" }
                    };
                }

                if ( !visited.Contains(idx) )
                {
                    visited.Add(idx);
                    idx = ExecuteInstruction(instructions[idx], idx);
                } else
                {
                    idx = 0;
                    visited.Clear( );
                    hasChanged = false;
                    instructions = GetInstructions( );
                    accumulator = 0;
                }                
            }

            return accumulator.ToString( );            
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
