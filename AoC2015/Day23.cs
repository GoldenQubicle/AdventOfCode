using System.Collections.Generic;
using Common;

namespace AoC2015
{
    public class Day23 : Solution
    {
        public Day23(string file) : base(file, "\n") { }

        private readonly Dictionary<string, Stack<int>> registers = new()
        {
            { "a", new Stack<int>() },
            { "b", new Stack<int>() }
        };

        private int pointer;
        public string Answer { get; set; } = "b";
        public override string SolvePart1( )
        {
            registers["a"].Push(0);
            registers["b"].Push(0);
            RunProgram();

            return registers[Answer].Peek().ToString();

        }

        public override string SolvePart2( )
        {
            registers["a"].Push(1);
            registers["b"].Push(0);
            RunProgram();

            return registers[Answer].Peek().ToString();
        }


        private void RunProgram( )
        {
            while(pointer < Input.Count)
            {
                var instruction = Input[pointer][..3];
                var token = Input[pointer][4..5];
                var register = registers.ContainsKey(token) ? registers[token] : new Stack<int>();
                var offset = token.Equals("-") ? -1 * int.Parse(Input[pointer][5..]) :
                    token.Equals("+") ? int.Parse(Input[pointer][5..]) :
                    Input[pointer].Contains(",") ? int.Parse(Input[pointer][8..]) : 0;

                switch(instruction)
                {
                    case "hlf":
                        register.Push(register.Peek() / 2);
                        pointer++;
                        break;
                    case "tpl":
                        register.Push(register.Peek() * 3);
                        pointer++;
                        break;
                    case "inc":
                        register.Push(register.Peek() + 1);
                        pointer++;
                        break;
                    case "jmp":
                        pointer += offset;
                        break;
                    case "jie":
                        pointer += register.Peek() % 2 == 0 ? offset : 1;
                        break;
                    case "jio":
                        pointer += register.Peek() == 1 ? offset : 1;
                        break;
                }
            }
        }

    }
}