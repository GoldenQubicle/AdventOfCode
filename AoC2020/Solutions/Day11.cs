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

        public Day11(string file) : base(file) => seatingState = Input.Select(i => i.ToCharArray( )).ToList( );

        public override int SolvePart1( ) => Solve(Input.Select(i => i.ToCharArray( )).ToList( ), true);

        public override int SolvePart2( ) => Solve(Input.Select(i => i.ToCharArray( )).ToList( ), false);

        private int Solve(List<char[ ]> state, bool part1)
        {
            var newState = GetNewState(state, part1 ? GetNeighbors : GetNeighborsInSight, part1 ? 4 : 5);

            while ( !SameState(state, newState) )
            {
                state = newState;
                newState = GetNewState(state, part1 ? GetNeighbors : GetNeighborsInSight, part1 ? 4 : 5);
            }

            return state.Sum(c => c.Count(s => s == '#'));
        }

        private List<char[ ]> GetNewState(List<char[ ]> current,
            Func<int, int, List<char[ ]>, List<char>> getNeighbors,
            int threshold) => current.Select((row, y) => row.Select((c, x) =>
            {
                var neighbors = getNeighbors(x, y, current);
                return c switch
                {
                    'L' when !neighbors.Any(c => c == '#') => '#',
                    '#' when neighbors.Count(c => c == '#') >= threshold => 'L',
                    _ => c
                };
            }).ToArray( )).ToList( );


        public bool SameState(List<char[ ]> old, List<char[ ]> update) => old
                .Zip(update, (old, update) => old.Select((c, i) => c == update[i]))
                .SelectMany(b => b)
                .All(b => b);

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
                    if ( state[ty][tx] != '.' )
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
