using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2020.Solutions
{
    public class Day22 : Solution<int>
    {
        public Day22(string file) : base(file, "\r\n\r\n") { }

        public (Queue<int>, Queue<int>) InitializeGame( )
        {
            var p1 = new Queue<int>( );
            var p2 = new Queue<int>( );
            Input[0].Split("\r\n").Skip(1).ToList( ).ForEach(n => p1.Enqueue(int.Parse(n)));
            Input[1].Split("\r\n").Skip(1).ToList( ).ForEach(n => p2.Enqueue(int.Parse(n)));
            return (p1, p2);
        }

        public override int SolvePart1( )
        {
            var (player1, player2) = InitializeGame( );

            while ( player1.Count > 0 && player2.Count > 0 )
            {
                var c1 = player1.Dequeue( );
                var c2 = player2.Dequeue( );

                if ( c1 > c2 )
                {
                    player1.Enqueue(c1);
                    player1.Enqueue(c2);
                }
                else
                {
                    player2.Enqueue(c2);
                    player2.Enqueue(c1);
                }
            }

            return player1.Count == 0 ?
                player2.Reverse( ).Select((c, i) => c * ( i + 1 )).Sum( ) :
                player1.Reverse( ).Select((c, i) => c * ( i + 1 )).Sum( );
        }

        public override int SolvePart2( )
        {
            var (player1, player2) = InitializeGame( );
            var (player1Win, deck1, deck2) = RecurseGame(player1, player2);

            return player1Win ?
                deck1.Reverse( ).Select((c, i) => c * ( i + 1 )).Sum( ) :
                deck2.Reverse( ).Select((c, i) => c * ( i + 1 )).Sum( );
        }

        private (bool player1Wins, Queue<int>, Queue<int>) RecurseGame(Queue<int> player1, Queue<int> player2)
        {
            var state = (new List<int[ ]>( ), new List<int[ ]>( ));
            var player1Wins = false;

            while ( player1.Count > 0 && player2.Count > 0 )
            {
                if ( OrderSeenBefore(state, player1, player2) )
                {
                    return (true, player1, player2);
                }

                CaptureState(state, player1, player2);

                player1Wins = false;
                var c1 = player1.Dequeue( );
                var c2 = player2.Dequeue( );

                if ( player1.Count >= c1 && player2.Count >= c2 )
                {
                    player1Wins = RecurseGame(player1.NewDeck(c1), player2.NewDeck(c2)).player1Wins;
                }
                else if ( c1 > c2 )
                {
                    player1Wins = true;
                }

                if ( player1Wins )
                {
                    player1.Enqueue(c1);
                    player1.Enqueue(c2);
                }
                else
                {
                    player2.Enqueue(c2);
                    player2.Enqueue(c1);
                }
            }

            return player1Wins ?
                 (true, player1, player2) :
                 (false, player1, player2);
        }

        private bool OrderSeenBefore((List<int[ ]> player1, List<int[ ]> player2) state,
            Queue<int> player1, Queue<int> player2) =>
            state.player1
                .Where(s => s.Length == player1.Count)
                .Select(s => s.Select((c, i) => player1.ToArray( )[i] == c))
                .Any(s => s.All(b => b)) ||
            state.player2
                .Where(s => s.Length == player2.Count)
                .Select(s => s.Select((c, i) => player2.ToArray( )[i] == c))
                .Any(s => s.All(b => b));

        private void CaptureState((List<int[ ]> player1, List<int[ ]> player2) state,
            Queue<int> player1, Queue<int> player2)
        {
            state.player1.Add(player1.ToArray( ));
            state.player2.Add(player2.ToArray( ));
        }
    }

    public static class QueueExtension
    {
        public static Queue<int> NewDeck(this Queue<int> player, int take)
        {
            var q = new Queue<int>( );
            player.Take(take).ToList( ).ForEach(c => q.Enqueue(c));
            return q;
        }
    }
}
