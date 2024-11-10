namespace AoC2020
{
    public class Day10 : Solution
    {
        private List<int> jolts;
        public Day10(string file) : base(file) => jolts = Input.Select(i => int.Parse(i)).ToList( );
        public override async Task<string> SolvePart1( ) => (CalculateJoltDifferenes( ).jd1 * CalculateJoltDifferenes( ).jd3).ToString( );
        public override async Task<string> SolvePart2( )
        {
            jolts.Add(0);
            jolts.Sort( );
            var connections = new long[jolts.Count].ToList( );

            connections[0] = 1;
            for ( int i = 0 ; i < jolts.Count - 1 ; i++ )
            {
                var count = connections[i];
                if ( jolts.Contains(jolts[i] + 1) )
                    connections[jolts.IndexOf(jolts[i] + 1)] += count;
                if ( jolts.Contains(jolts[i] + 2) )
                    connections[jolts.IndexOf(jolts[i] + 2)] += count;
                if ( jolts.Contains(jolts[i] + 3) )
                    connections[jolts.IndexOf(jolts[i] + 3)] += count;
            }

            return connections.Last( ).ToString( );
        }

        public (int jd1, int jd3) CalculateJoltDifferenes( )
        {
            jolts.Add(0);
            jolts.Sort( );
            var deltas = new List<int>( );
            for ( int i = 0 ; i < jolts.Count - 1 ; i++ )
            {
                deltas.Add(jolts[i + 1] - jolts[i]);
            }

            return (deltas.Where(d => d == 1).Count( ), deltas.Where(d => d == 3).Count( ) + 1);
        }
    }
}
