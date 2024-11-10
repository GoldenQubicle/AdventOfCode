namespace AoC2015
{
    public class Day12 : Solution
    {
        public Day12(string file) : base(file) { }

        public Day12(List<string> input) : base(input) { }

        public override async Task<string> SolvePart1( )
        {
            var matches = Regex.Matches(Input.First( ), @"(?<positive>\d{1,})|(?<negative>-\d{1,})");
            var result = 0;
            foreach ( Match match in matches )
            {
                result += match.Groups["positive"].Success ?
                    int.Parse(match.Groups["positive"].Value) :
                    int.Parse(match.Groups["negative"].Value);
            }
            return result.ToString( );
        }

        public override async Task<string> SolvePart2( )
        {
            var openings = new Stack<(char type, int idx, bool isIgnore)>( );
            var tobeIgnored = new List<IEnumerable<int>>( );

            for ( int i = 0 ; i < Input[0].Length ; i++ )
            {
                var c = Input[0][i];

                if ( c == '{' || c == '[' )
                    openings.Push((c, i, false));

                if ( c == 'r' && Input[0][i + 1] == 'e' && Input[0][i + 2] == 'd' && openings.Peek( ).type == '{' )
                {
                    var opening = openings.Pop( );
                    openings.Push((opening.type, opening.idx, true));
                }

                if ( c == '}' || c == ']' )
                {
                    var opening = openings.Pop( );
                    if ( opening.isIgnore )
                        tobeIgnored.Add(Enumerable.Range(opening.idx, i - opening.idx));
                }
            };

            var matches = Regex.Matches(Input.First( ), @"(?<positive>\d{1,})|(?<negative>-\d{1,})");
            var result = matches.Aggregate(0, (result, match) =>
            {
                if ( !tobeIgnored.Any(r => r.Contains(match.Index)) )
                {
                    result += match.Groups["positive"].Success ?
                        int.Parse(match.Groups["positive"].Value) :
                        int.Parse(match.Groups["negative"].Value);
                }
                return result;
            });
            
            return result.ToString( );
        }
    }
}