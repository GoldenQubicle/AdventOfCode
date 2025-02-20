﻿namespace AoC2020
{
    public class Day25 : Solution
    {
        long cardKey;
        long doorKey;
        
        public Day25(string file) : base(file)
        {
            cardKey = long.Parse(Input[0]);
            doorKey = long.Parse(Input[1]);
        }

        public override async Task<string> SolvePart1( )
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

            return key.ToString( );
        }

        public override async Task<string> SolvePart2( ) => string.Empty;
    }
}
