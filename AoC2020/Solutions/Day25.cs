using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2020.Solutions
{
    public class Day25 : Solution<long>
    {
        long cardKey;
        long doorKey;
        
        public Day25(string file) : base(file)
        {
            cardKey = long.Parse(Input[0]);
            doorKey = long.Parse(Input[1]);
        }

        public override long SolvePart1( )
        {
            long transform(long v, long s) => ( v * s ) % 20201227;

            var cardLoop = 0;
            var value = 1L;

            while(value != cardKey)
            {
                value = transform(value, 7);
                cardLoop++;
            }

            var key = 1L;

            for(int i = 0 ; i < cardLoop ; i ++ )
            {
                key = transform(key, doorKey);
            }

            return key;
        }

        public override long SolvePart2( ) => 0L;
    }
}
