using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2020.Solutions
{
    public class Day23 : Solution<string>
    {
        private int[ ] cups;
        private int iterations;
        public Day23(string file) : base(file) =>
            cups = Input[0]
                    .Where(char.IsDigit)
                    .Select(c => int.Parse(c.ToString( ))).ToArray( );

        public string SolvePart1(int it)
        {
            iterations = it;

            var result = Iterate( );
            var indexone = cups.ToList( ).IndexOf(1);

            return result.Skip(indexone + 1).Take(cups.Length - 1).Aggregate(string.Empty, (s, c) => s + c.ToString( )).ToString( );
        }

        public string SolvePart2(int it)
        {
            iterations = it;
            cups = cups.Concat(Enumerable.Range(cups.Max( ), 1000000 - cups.Length)).ToArray( );
            var result = Iterate( );
            var indexone = cups.ToList( ).IndexOf(1);

            return ( result[indexone + 1] * result[indexone + 2] ).ToString( );
        }

        private int[ ] Iterate( )
        {

            var main = new Stopwatch( );
            var loop = new Stopwatch( );

            for ( int i = 0 ; i < iterations ; i++ )
            {
                main.Start( );
                var index = i % cups.Length;
                var current = cups[index];
                var setAside = cups.Concat(cups).ToArray( )[( index + 1 )..( index + 4 )];
                var pickFrom = cups.Except(setAside).Except(new[ ] { current });
                loop.Start( );
       
                var destination = -1;

                while ( destination == -1 )
                {
                    if ( !pickFrom.Contains(current) )
                        current--;
                    else
                        destination = current;

                    if ( current == 0 )
                        destination = pickFrom.Max( );
                }
                loop.Stop( );
                var nextCups = cups.Except(setAside).ToList( );
                var insert = nextCups.IndexOf(destination) + 1;
                var offset = index + 1 + setAside.Length > cups.Length ? ( index + 1 + setAside.Length ) - cups.Length : 0;

                nextCups.InsertRange(insert, setAside);
                var nextCupsArr = nextCups.ToArray( );

                if ( insert <= index )
                    nextCupsArr = nextCupsArr[( 3 - offset )..].Concat(nextCupsArr[..( 3 - offset )]).ToArray( );

                cups = nextCupsArr;
                main.Stop( );

                Console.WriteLine($"main iteration took: {main.ElapsedMilliseconds}, while loop took: {loop.ElapsedMilliseconds}");
                main.Reset( );
                loop.Reset( );
            }

            return cups.Concat(cups).ToArray( );
        }

        public override string SolvePart1( ) => throw new NotImplementedException( );

        public override string SolvePart2( ) => throw new NotImplementedException( );
    }
}
