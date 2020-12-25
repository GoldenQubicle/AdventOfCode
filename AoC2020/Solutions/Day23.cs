using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2020.Solutions
{
    public class Day23 : Solution<string>
    {
        public int IteratePart1 { get; set; } = 100;
        public int IteratePart2 { get; set; } = 10000000;

        private int[ ] cups;
        public Day23(string file) : base(file) => cups = GetCups( );

        private int[ ] GetCups( ) => Input[0]
                    .Where(char.IsDigit)
                    .Select(c => int.Parse(c.ToString( ))).ToArray( );

        public override string SolvePart1( )
        {
            var result = Iterate( );
            var indexone = cups.ToList( ).IndexOf(1);

            return result.Skip(indexone + 1).Take(cups.Length - 1).Aggregate(string.Empty, (s, c) => s + c.ToString( )).ToString( );
        }

        public override string SolvePart2( )
        {
            var cups = GetCups( );
            cups = cups.Concat(Enumerable.Range(cups.Max( ) + 1, 1000000 - cups.Length)).ToArray( );

            var dic = new Dictionary<int, LinkedListNode<int>>( ); // using list.Find() takes FOREVER hence a dictionary for faster retrieval
            var llist = new LinkedList<int>( );
            var first = new LinkedListNode<int>(cups[0]);
            llist.AddFirst(first);
            dic.Add(cups[0], first);
            var current = first;

            for ( int i = 1 ; i < cups.Length ; i++ )
            {
                var next = new LinkedListNode<int>(cups[i]);
                dic.Add(cups[i], next);
                llist.AddAfter(current, next);
                current = next;
            }

            current = llist.First;

            for ( int i = 0 ; i < IteratePart2 ; i++ )
            {
                var setAside = new List<LinkedListNode<int>>
                {
                    current.WrapNext(),
                    current.WrapNext().WrapNext(),
                    current.WrapNext().WrapNext().WrapNext(),
                };

                setAside.ForEach(n => llist.Remove(n));

                var next = current.Value == 1 ? cups.Count( ) : current.Value - 1;
                while ( setAside.Select(n => n.Value).Contains(next) )
                {
                    next--;
                    if ( next == 0 ) next = cups.Count( );
                }

                var insert = dic[next];
                llist.AddAfter(insert, setAside[0]);
                llist.AddAfter(setAside[0], setAside[1]);
                llist.AddAfter(setAside[1], setAside[2]);

                current = current.WrapNext( );
            }

            var one = dic[1];

            return ( ( long ) one.WrapNext( ).Value * one.WrapNext( ).WrapNext( ).Value ).ToString( );
        }

        private int[ ] Iterate( )
        {
            for ( int i = 0 ; i < IteratePart1 ; i++ )
            {
                var index = i % cups.Length;
                var current = cups[index];
                var setAside = cups.Concat(cups).ToArray( )[( index + 1 )..( index + 4 )];
                var pickFrom = cups.Except(setAside).Except(new[ ] { current });

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
                var nextCups = cups.Except(setAside).ToList( );
                var insert = nextCups.IndexOf(destination) + 1;
                var offset = index + 1 + setAside.Length > cups.Length ? ( index + 1 + setAside.Length ) - cups.Length : 0;

                nextCups.InsertRange(insert, setAside);
                var nextCupsArr = nextCups.ToArray( );

                if ( insert <= index )
                    nextCupsArr = nextCupsArr[( 3 - offset )..].Concat(nextCupsArr[..( 3 - offset )]).ToArray( );

                cups = nextCupsArr;
            }
            return cups.Concat(cups).ToArray( );
        }
    }

    public static class LinkedListExtension
    {
        public static LinkedListNode<T> WrapNext<T>(this LinkedListNode<T> node) => node.Next ?? node.List.First;
    }
}
