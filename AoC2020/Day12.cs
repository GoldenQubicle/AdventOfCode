namespace AoC2020
{
    public class Day12 : Solution
    {
        private List<Instruction> instructions;
        public Day12(string file) : base(file) =>
            instructions = Input.Select(i => new Instruction
            {
                Action = i.First(char.IsLetter),
                Value = int.Parse(i.Where(char.IsDigit).ToArray( ))
            }).ToList( );

        public override async Task<string> SolvePart1( ) => instructions.Aggregate(
            (x: 0, y: 0, d: 90), (p, i) => i.Action switch
                {
                    'N' => (p.x, p.y + i.Value, p.d),
                    'E' => (p.x + i.Value, p.y, p.d),
                    'S' => (p.x, p.y - i.Value, p.d),
                    'W' => (p.x - i.Value, p.y, p.d),
                    'L' => (p.x, p.y, ( p.d - i.Value + 360 ) % 360),
                    'R' => (p.x, p.y, ( p.d + i.Value ) % 360),
                    'F' => p.d switch
                    {
                        0 => (p.x, p.y + i.Value, p.d),
                        90 => (p.x + i.Value, p.y, p.d),
                        180 => (p.x, p.y - i.Value, p.d),
                        270 => (p.x - i.Value, p.y, p.d),
                    },
                },
                pos => Math.Abs(pos.x) + Math.Abs(pos.y)).ToString( );

        public override async Task<string> SolvePart2( ) => instructions.Aggregate(
            (x: 0, y: 0, wx: 10, wy: 1), (p, i) => i switch
               {
                   ('N', var value) => (p.x, p.y, p.wx, p.wy + value),
                   ('E', var value) => (p.x, p.y, p.wx + value, p.wy),
                   ('S', var value) => (p.x, p.y, p.wx, p.wy - value),
                   ('W', var value) => (p.x, p.y, p.wx - value, p.wy),
                   ('L', 90) or ('R', 270) => (p.x, p.y, -p.wy, p.wx),
                   ('R', 90) or ('L', 270) => (p.x, p.y, p.wy, -p.wx),
                   ('R', 180) or ('L', 180) => (p.x, p.y, -p.wx, -p.wy),
                   ('F', var value) => (p.x + ( p.wx * value ), p.y + ( p.wy * value ), p.wx, p.wy)
               },
              pos => Math.Abs(pos.x) + Math.Abs(pos.y)).ToString( );


        public record Instruction
        {
            public char Action;
            public int Value;

            public void Deconstruct(out char a, out int v)
            {
                a = Action;
                v = Value;
            }
        }
    }
}
