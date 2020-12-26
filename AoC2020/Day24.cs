using System.Collections.Generic;
using System.Linq;
using Common;

namespace AoC2020
{
    public class Day24 : Solution
    {
        List<List<(int x, int y, int z)>> moveSet = new( );
        Dictionary<(int x, int y, int z), bool> hexgrid = new( );
        Dictionary<string, (int x, int y, int z)> directions = new( )
        {
            { "e", (1, -1, 0) },
            { "se", (0, -1, 1) },
            { "sw", (-1, 0, 1) },
            { "w", (-1, 1, 0) },
            { "nw", (0, 1, -1) },
            { "ne", (1, 0, -1) },
        };

        public Day24(string file) : base(file) =>
            moveSet = Input.Select(line =>
            {
                var moves = new List<(int x, int y, int z)>( );
                while ( line.Length > 0 )
                {
                    if ( directions.ContainsKey(line[..1]) )
                    {
                        moves.Add(directions[line[..1]]);
                        line = line[1..];
                    }
                    else
                    {
                        moves.Add(directions[line[..2]]);
                        line = line[2..];
                    }
                }
                return moves;
            }).ToList( );

        public override string SolvePart1( )
        {
            moveSet.ForEach(move =>
            {
                var pos = move.Aggregate((x: 0, y: 0, z: 0), (pos, move) =>
                    (pos.x + move.x, pos.y + move.y, pos.z + move.z));

                if ( !hexgrid.ContainsKey(pos) )
                    hexgrid.Add(pos, false);
                else
                    hexgrid[pos] = !hexgrid[pos];
            });

            return hexgrid.Values.Count(b => !b).ToString( );
        }

        public override string SolvePart2( )
        {
            hexgrid = new( );
            SolvePart1( );

            for ( int i = 0 ; i < 100 ; i++ )
            {
                var expansion = new Dictionary<(int, int, int), bool>( );
                var newState = new Dictionary<(int, int, int), bool>( );

                foreach ( var tile in hexgrid )
                {
                    var newN = GetNewNeighbors(tile.Key);
                    foreach ( var kvp in newN )
                        expansion.TryAdd(kvp.Key, kvp.Value);

                    expansion.Add(tile.Key, tile.Value);
                }

                foreach ( var tile in expansion )
                {
                    var count = GetNeighbors(tile.Key, expansion).Count(t => !t.Value); 
                    
                    if ( !tile.Value && ( count == 0 || count > 2 ) )
                        newState.Add(tile.Key, true);
                    else if ( tile.Value && count == 2 )
                        newState.Add(tile.Key, false);
                    else
                        newState.Add(tile.Key, tile.Value);
                }
                hexgrid = newState;
            }
            return hexgrid.Values.Count(b => !b).ToString( );
        }

        private Dictionary<(int, int, int), bool> GetNewNeighbors((int x, int y, int z) pos)
        {
            var neighbors = new Dictionary<(int, int, int), bool>( );
            foreach ( var (_, n) in directions )
            {
                var nPos = (pos.x + n.x, pos.y + n.y, pos.z + n.z);
                if ( !hexgrid.ContainsKey(nPos) )
                    neighbors.Add(nPos, true);
            }
            return neighbors;
        }

        private Dictionary<(int, int, int), bool> GetNeighbors((int x, int y, int z) pos,
            Dictionary<(int, int, int), bool> expansion)
        {
            var neighbors = new Dictionary<(int, int, int), bool>( );
            foreach ( var (_, n) in directions )
            {
                var nPos = (pos.x + n.x, pos.y + n.y, pos.z + n.z);
                if ( expansion.ContainsKey(nPos) )
                    neighbors.Add(nPos, expansion[nPos]);
            }
            return neighbors;
        }
    }
}
