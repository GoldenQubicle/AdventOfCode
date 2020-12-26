using System.Collections.Generic;
using System.Linq;
using Common;

namespace AoC2020
{
    public class Day15 : Solution<long>
    {
        private List<long> numbers;
        public Day15(string file) : base(file, ",") => numbers = Input.Select(long.Parse).ToList( );

        public override long SolvePart1( ) => MemoryGame(2020);
        public override long SolvePart2( ) => MemoryGame(30000000);

        private long MemoryGame(int iterate )
        {
            var turns = numbers.Select((n, i) => (n, i)).ToDictionary(n => n.n, n => new List<int> { n.i });
            var lastNumber = numbers.Last( );

            for ( int i = numbers.Count ; i < iterate ; i++ )
            {
                if ( turns[lastNumber].Count == 1 )
                {
                    lastNumber = 0;

                    turns[lastNumber].Add(i);
                }
                else
                {
                    lastNumber = turns[lastNumber][turns[lastNumber].Count - 1] - turns[lastNumber][turns[lastNumber].Count - 2];

                    if ( turns.ContainsKey(lastNumber) )
                    {
                        turns[lastNumber].Add(i);
                    }
                    else
                    {
                        turns.Add(lastNumber, new List<int> { i });
                    }
                }
            }
            return lastNumber;
        }      
    }
}
