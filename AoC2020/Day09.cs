using System.Collections.Generic;
using System.Linq;
using Common;

namespace AoC2020
{
    public class Day09 : Solution
    {
        private List<long> cypher;
        public int Preamble { get; set; } = 25;
        private long target;
        public Day09(string file) : base(file) => cypher = Input.Select(i => long.Parse(i)).ToList( );

        public override string SolvePart1( )
        {
            for ( int i = Preamble ; i < cypher.Count ; i++ )
            {
                var sums = GetCypherSums(i, Preamble);
                if ( !sums.Contains(cypher[i]) )
                {
                    return cypher[i].ToString( );
                }
            }
            return string.Empty;
        }

        public override string SolvePart2( )
        {
            target = long.Parse(SolvePart1( ));

            for ( int i = 0 ; i < cypher.Count ; i++ )
            {
                var sum = cypher[i];

                for ( int j = i + 1 ; j < cypher.Count ; j++ )
                {
                    sum += cypher[j];
                    if ( sum > target ) break;
                    if ( sum == target )
                    {
                        var digits = new List<long>( );
                        for ( int k = i ; k < j ; k++ )
                        {
                            digits.Add(cypher[k]);
                        }
                        return ( digits.Min( ) + digits.Max( ) ).ToString( );
                    };
                }
            }
            return string.Empty;
        }

        public List<long> GetCypherSums(int i, int preamble)
        {
            var sums = new List<long>( );
            for ( int j = i - preamble ; j < i ; j++ )
            {
                for ( int k = j + 1 ; k < i ; k++ )
                {
                    sums.Add(cypher[j] + cypher[k]);
                }
            }
            return sums.Distinct( ).ToList( );
        }
    }
}
