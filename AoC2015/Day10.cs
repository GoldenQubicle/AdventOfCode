namespace AoC2015
{
    public class Day10 : Solution
    {
        private readonly Regex pattern = new Regex(@"(?<1>[1]+)|(?<2>[2]+)|(?<3>[3]+)");
        
        public Day10(string file) : base(file) { }

        public Day10(List<string> input) : base(input) { }

        public override async Task<string> SolvePart1( ) => Iterate(40);

        public override async Task<string> SolvePart2( ) => Iterate(50);

        private string Iterate(int iterations )
        {
            var current = Input[0];

            for ( var i = 0 ; i < iterations ; i++ )
            {
                current = LookAndSay(current);
            }
            return current.Length.ToString( );
        }

        public string LookAndSay(string input)
        {
            var result = new StringBuilder( );
            foreach ( Match match in pattern.Matches(input) )
            {
                if ( match.Groups["1"].Success )
                {
                    result.Append(match.Length);
                    result.Append(1);
                }
                if ( match.Groups["2"].Success )
                {
                    result.Append(match.Length);
                    result.Append(2);
                }
                if ( match.Groups["3"].Success )
                {
                    result.Append(match.Length);
                    result.Append(3);
                }
            }
            return result.ToString( );
        }
    }
}