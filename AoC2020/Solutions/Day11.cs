using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2020.Solutions
{
    public class Day11 : Solution<int>
    {
        public List<char[ ]> seatingState;
        private List<(int x, int y)> offsets = new List<(int, int)>
        {
            (-1, -1), (0, -1), (1, -1),
            (-1, 0),           (1, 0),
            (-1, 1),  (0, 1),  (1, 1)
        };

        public Day11(string file) : base(file, "\r\n") => seatingState = Input.Select(i => i.ToCharArray( )).ToList( );

        public override int SolvePart1( )
        {
            var newState = GetNewStatePart1(seatingState);

            while ( !SameState(seatingState, newState) )
            {
                seatingState = newState;
                newState = GetNewStatePart1(seatingState);
            }

            return seatingState.Sum(c => c.Count(s => s == '#'));
        }

        public override int SolvePart2( )
        {
            seatingState = Input.Select(i => i.ToCharArray( )).ToList( );
            var newState = GetNewStatePart2(seatingState);

            while ( !SameState(seatingState, newState) )
            {
                seatingState = newState;
                newState = GetNewStatePart2(seatingState);
            }

            return seatingState.Sum(c => c.Count(s => s == '#'));
        }

        private List<char[ ]> GetNewStatePart2(List<char[ ]> current)
        {
            var newState = current.Select(s => new char[s.Length]).ToList( );
            for ( int y = 0 ; y < current.Count( ) ; y++ )
            {
                for ( int x = 0 ; x < current[0].Count( ) ; x++ )
                {
                    var neighbors = GetNeighborsInSight(x, y, current);

                    newState[y][x] = current[y][x];

                    if ( current[y][x] == 'L' && !neighbors.Any(c => c == '#'))
                        newState[y][x] = '#';
                    if ( current[y][x] == '#' && neighbors.Count(c => c == '#') > 4 )
                        newState[y][x] = 'L';
                }
            }
            return newState;
        }

        private List<char[ ]> GetNewStatePart1(List<char[ ]> current)
        {
            var newState = current.Select(s => new char[s.Length]).ToList( );
            for ( int y = 0 ; y < current.Count( ) ; y++ )
            {
                for ( int x = 0 ; x < current[0].Count( ) ; x++ )
                {
                    var neighbors = GetNeighbors(x, y, current);
                    newState[y][x] = current[y][x];
                    if ( current[y][x] == 'L' && !neighbors.Any(c => c == '#') )
                        newState[y][x] = '#';
                    if ( current[y][x] == '#' && neighbors.Count(c => c == '#') >= 4 )
                        newState[y][x] = 'L';
                }
            }
            return newState;
        }

        public bool SameState(List<char[ ]> old, List<char[ ]> update)
        {
            for ( int y = 0 ; y < old.Count( ) ; y++ )
            {
                for ( int x = 0 ; x < old[0].Count( ) ; x++ )
                {
                    if ( old[y][x] != update[y][x] )
                        return false;
                }
            }
            return true;
        }

        public List<char> GetNeighborsInSight(int x, int y, List<char[ ]> state)
        {
            var neighbors = new List<char>( );
            
            offsets.ForEach(o =>
            {
                var tx = x;
                var ty = y;
                while ( tx + o.x >= 0 && tx + o.x < state[0].Count( ) &&
                 ty + o.y >= 0 && ty + o.y < state.Count )
                {
                    tx += o.x;
                    ty += o.y;
                    if(state[ty][tx] != '.')
                    {
                        neighbors.Add(state[ty][tx]);
                        break;
                    }                        
                }
            });
            return neighbors;
        }

        public List<char> GetNeighbors(int x, int y, List<char[ ]> state)
        {
            var neighbors = new List<char>( );
            offsets.ForEach(o =>
            {
                if ( x + o.x >= 0 && x + o.x < state[0].Count( ) &&
                 y + o.y >= 0 && y + o.y < state.Count )
                {
                    neighbors.Add(state[y + o.y][x + o.x]);
                }
            });
            return neighbors;
        }
    }
}
