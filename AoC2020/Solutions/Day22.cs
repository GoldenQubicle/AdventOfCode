using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2020.Solutions
{
    public class Day22 : Solution<int>
    {
        public Day22(string file) : base(file, "\r\n\r\n") { }

        private (Queue<int>, Queue<int>) InitializeGame( ) =>
            (new Queue<int>(Input[0].Split("\r\n").Skip(1).Select(int.Parse)),
             new Queue<int>(Input[1].Split("\r\n").Skip(1).Select(int.Parse)));

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

            return player1.Count > 0 ? player1.GetScore( ) : player2.GetScore( );
        }

        public override int SolvePart2( )
        {
            var (player1, player2) = InitializeGame( );            
            return RecurseGame(player1, player2) ? player1.GetScore( ) : player2.GetScore( );
        }

        private bool RecurseGame(Queue<int> player1, Queue<int> player2)
        {
            var state = (new List<int[ ]>( ), new List<int[ ]>( ));
            var player1Wins = false;

            while ( player1.Count > 0 && player2.Count > 0 )
            {
                if ( OrderSeenBefore(state, player1, player2) ) return true;
             
                CaptureState(state, player1, player2);

                player1Wins = false;
                var c1 = player1.Dequeue( );
                var c2 = player2.Dequeue( );

                if ( player1.Count >= c1 && player2.Count >= c2 )
                {
                    player1Wins = RecurseGame(player1.NewDeck(c1), player2.NewDeck(c2));
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

            return player1Wins;
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
        public static Queue<int> NewDeck(this Queue<int> deck, int amount) => new Queue<int>(deck.Take(amount));

        public static int GetScore(this Queue<int> deck) => deck.Reverse( ).Select((c, i) => c * ( i + 1 )).Sum( );
    }
}
