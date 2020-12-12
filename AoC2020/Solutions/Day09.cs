using System.Collections.Generic;
using System.Linq;

namespace AoC2020.Solutions
{
    public class Day09 : Solution<long>
    {
        private List<long> cypher;
        private int preamble;
        private long target;
        public Day09(string file) : base(file, "\r\n") => cypher = Input.Select(i => long.Parse(i)).ToList( );

        public long SolvePart1(int preamb)
        {
            preamble = preamb;

            return SolvePart1( );
        }

        public long SolvePart2(int preamb)
        {
            target = SolvePart1(preamb);
            return SolvePart2( );
        }

        public override long SolvePart1( )
        {            
            for ( int i = preamble ; i < cypher.Count ; i++ )
            {
                var sums = GetCypherSums(i, preamble);
                if ( !sums.Contains(cypher[i]) )
                {
                    return cypher[i];
                }
            }

            return 0;
        }
        public override long SolvePart2( )
        {   
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
                        for(int k = i ; k < j ; k++ )
                        {
                            digits.Add(cypher[k]);
                        }
                        return digits.Min() + digits.Max();
                    };
                }
            }
            return 0;
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
