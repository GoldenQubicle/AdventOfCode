using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2020.Solutions
{
    public class Day15 : Solution<long>
    {
        private List<long> numbers;
        public Day15(string file) : base(file, ",") => numbers = Input.Select(long.Parse).ToList( );
        public override long SolvePart1( )
        {
            for ( int i = numbers.Count - 1 ; i < 2020 - 1 ; i++ )
            {
                var lastNumber = numbers.Last( );
                if ( numbers.Count(n => n == lastNumber) == 1 )
                    numbers.Add(0);
                else
                {
                    var occurence = numbers.Select((n, i) => (n, i)).Where(n => n.n == lastNumber).OrderBy(n => n.i).ToList( );
                    numbers.Add(occurence[occurence.Count( ) - 1].i - occurence[occurence.Count( ) - 2].i);
                }
            }

            return numbers.Last( );
        }
        public override long SolvePart2( )
        {
            var m = 0;

            for ( int i = numbers.Count - 1 ; i < 30000000 - 1 ; i++ )
            {
                Console.WriteLine(i);
                if ( i % 1000000 == 0 )
                {
                    m++;
                    Console.WriteLine($"{DateTime.Now.ToShortTimeString()}: passed {i} million, only {30 - i} to go!");
                }

                var lastNumber = numbers.Last( );
                if ( numbers.Count(n => n == lastNumber) == 1 )
                    numbers.Add(0);
                else
                {
                    var occurence = numbers.Select((n, i) => (n, i)).Where(n => n.n == lastNumber).OrderBy(n => n.i).ToList( );
                    numbers.Add(occurence[occurence.Count( ) - 1].i - occurence[occurence.Count( ) - 2].i);
                }
            }

            return numbers.Last( );
        }
    }
}
