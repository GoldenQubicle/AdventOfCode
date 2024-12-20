namespace AoC2015
{
    public class Day07 : Solution
    {
        private Dictionary<string, ushort> wires = new( );
        private List<(string a, string b, string gate, string output)> instructions;

        public Day07(string file) : base(file)
        {
            var expression = new Regex("((?<a>\\w{1,}.)?(?<gate>RSHIFT.|LSHIFT.|OR.|AND.|NOT.))?(?<b>\\w{1,})(.->.)(?<output>\\w{1,})");
            instructions = Input
                .Select(line => expression.Match(line))
                .Select(match => (
                        a: match.Groups["a"].Value.Trim( ),
                        b: match.Groups["b"].Value.Trim( ),
                        gate: match.Groups["gate"].Value.Trim( ),
                        output: match.Groups["output"].Value.Trim( )
                )).ToList( );
        }

        public override async Task<string> SolvePart1( ) => Solve( );

        public override async Task<string> SolvePart2( )
        {
            var newBValue = SolvePart1( ).Result;
            wires = new( );
            instructions.RemoveAt(334);
            instructions.Insert(334, (a: "", b: newBValue, gate: "", output: "b"));
            return Solve( );
        }

        private string Solve( )
        {
            var start = instructions[6];
            var stack = new Stack<(string a, string b, string gate, string output)>( );
            stack.Push(start);

            while ( stack.Count > 0 )
            {
                var i = stack.Peek( );

                // figure out if the input wire(s) for this gate have value
                var hasValue = i.gate switch
                {
                    "AND" or "OR" => ( char.IsDigit(i.a[0]) || wires.ContainsKey(i.a) ) &&
                                     ( char.IsDigit(i.b[0]) || wires.ContainsKey(i.b) ),
                    "LSHIFT" or "RSHIFT" => char.IsDigit(i.a[0]) || wires.ContainsKey(i.a),
                    "NOT" => char.IsDigit(i.b[0]) || wires.ContainsKey(i.b),
                    _ => char.IsDigit(i.b[0]) || wires.ContainsKey(i.b)
                };

                if ( !hasValue )
                {
                    instructions
                        .Where(n => n.output.Equals(i.a) || n.output.Equals(i.b))
                        .ForEach(n => stack.Push(n));
                }
                else
                {
                    var a = string.IsNullOrEmpty(i.a) ? ushort.Parse("0") : char.IsDigit(i.a[0]) ? ushort.Parse(i.a) : wires[i.a];
                    var b = string.IsNullOrEmpty(i.b) ? ushort.Parse("0") : char.IsDigit(i.b[0]) ? ushort.Parse(i.b) : wires[i.b];

                    if ( wires.ContainsKey(i.output) )
                        wires[i.output] = Connect(a, b, i.gate);
                    else
                        wires.Add(i.output, Connect(a, b, i.gate));

                    stack.Pop( );
                }
            }
            return wires["a"].ToString( );
        }

        public ushort Connect(ushort a, ushort b, string gate) => gate switch
        {
            "AND" => ( ushort ) ( a & b ),
            "OR" => ( ushort ) ( a | b ),
            "NOT" => ( ushort ) ~b,
            "RSHIFT" => ( ushort ) ( a >> b ),
            "LSHIFT" => ( ushort ) ( a << b ),
            _ => b
        };
    }
}