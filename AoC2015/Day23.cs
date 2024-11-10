namespace AoC2015
{
    public class Day23 : Solution
    {
        public Day23(string file) : base(file, "\n") { }

        private readonly Dictionary<string, long> registers = new()
        {
            { "a", 0 },
            { "b", 0 }
        };

        private int pointer;
        public string Answer { get; set; } = "b";
        public override async Task<string> SolvePart1( )
        {
            RunProgram();

            return registers[Answer].ToString();

        }

        public override async Task<string> SolvePart2( )
        {
            registers["a"] = 1;

            RunProgram();

            return registers[Answer].ToString();
        }


        private void RunProgram( )
        {
            while(pointer < Input.Count)
            {
                var instruction = Input[pointer][..3];
                var token = Input[pointer][4..5];
                var offset = token.Equals("-") ? -1 * int.Parse(Input[pointer][5..]) :
                    token.Equals("+") ? int.Parse(Input[pointer][5..]) :
                    Input[pointer].Contains(",") ? int.Parse(Input[pointer][8..]) : 0;

                switch(instruction)
                {
                    case "hlf":
                        registers[token] /= 2;
                        pointer++;
                        break;
                    case "tpl":
                        registers[token] *= 3;
                        pointer++;
                        break;
                    case "inc":
                        registers[token] += 1;
                        pointer++;
                        break;
                    case "jmp":
                        pointer += offset;
                        break;
                    case "jie":
                        pointer += registers[token] % 2 == 0 ? offset : 1;
                        break;
                    case "jio":
                        pointer += registers[token] == 1 ? offset : 1;
                        break;
                }
                Console.WriteLine($"register a {registers["a"]}");
            }
        }

    }
}