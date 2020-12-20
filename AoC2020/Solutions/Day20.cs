using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2020.Solutions
{
    public class Day20 : Solution<long>
    {
        public readonly List<Tile> tiles = new( );

        List<Action<Tile>> tileConfigurations = new List<Action<Tile>>
            {
                tile => tile.FlipVertical( ),
                tile => tile.FlipHorizontal( ),
                tile => tile.FlipVertical( ),
                tile => tile.Rotate90Clockwise( ),
                tile => tile.FlipVertical( ),
                tile => tile.FlipHorizontal( ),
                tile => tile.FlipVertical( ),
            };

        public Day20(string file) : base(file, "\r\n\r\n")
        {
            tiles = Input.Select(i => i.Split("\r\n")).Select(i =>
               new Tile(int.Parse(i[0].Where(char.IsDigit).ToArray( )), i.Skip(1).ToList( ))).ToList( );
        }

        public override long SolvePart1( ) => GetMatchedTiles( )
            .Where(m => m.matches.Count == 2)
            .Aggregate(1L, (sum, match) => sum *= match.tileId);

        private List<(int tileId, List<int> matches)> GetMatchedTiles( )
        {
            var matches = new Dictionary<int, List<int>>( );
            var tileEdges = tiles.ToDictionary(t => t.Id, t => t.GetEdges( ));

            Action<Tile, Dictionary<int, List<string>>> addMatches = (tile, otherTiles) =>
                   matches[tile.Id].AddRange(otherTiles
                        .Select(ot => ot.Value.Contains(tile.GetEdgeUp( )) ? ot.Key : 0));

            foreach ( var tile in tiles )
            {
                matches.Add(tile.Id, new List<int>( ));
                var otherTiles = tileEdges.Where(kvp => kvp.Key != tile.Id).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

                addMatches(tile, otherTiles);

                tileConfigurations.ForEach(config =>
                {
                    config(tile);
                    addMatches(tile, otherTiles);
                });
            }

            return matches.Select(m => (tileId: m.Key, matches: m.Value.Where(v => v != 0).ToList( ))).ToList( );
        }

        public override long SolvePart2( )
        {
            var matches = GetMatchedTiles( );
            var topLeft = tiles.First(t => t.Id.Equals(matches.First(m => m.matches.Count == 2).tileId));
            var neighbor = tiles.First(t => t.Id.Equals(matches.First(m => m.matches.Count == 2).matches.First( )));

            void DoTileAction(Tile tile, int i) => tileConfigurations[i](tile);

            var cornerActions = 0;
            var neighborActions = 0;

            while ( !topLeft.GetEdgeRight( ).Equals(neighbor.GetEdgeLeft( )) )
            {
                DoTileAction(topLeft, cornerActions);

                cornerActions++;

                if ( cornerActions == tileConfigurations.Count )
                {
                    DoTileAction(neighbor, neighborActions);
                    neighborActions++;
                    cornerActions = 0;
                }
            }
            return 0;

        }

    }

    public class Tile
    {
        public int Id { get; }
        public List<string> Contents { get; set; }
        private int width;
        private int height;
        public Tile(int id, List<string> contents)
        {
            Id = id;
            Contents = contents;
            width = Contents[0].Length - 1;
            height = Contents.Count - 1;
        }


        public void TrimEdges( )
        {
            Contents = Contents.Skip(1).Take(height - 1).Select(s => s[1..width]).ToList( );
            width = Contents[0].Length - 1;
            height = Contents.Count - 1;
        }

        public void Rotate90AntiClockwise( )
        {
            var rotated = new List<string>( );
            for ( int i = width ; i >= 0 ; i-- )
            {
                rotated.Add(new string(Contents.Select(s => s[i]).ToArray( )));
            }
            Contents = rotated;
        }

        public void Rotate90Clockwise( )
        {
            var rotated = new List<string>( );
            for ( int i = 0 ; i <= width ; i++ )
            {
                rotated.Add(new string(Contents.Select(s => s[i]).ToArray( )));
            }
            Contents = rotated.Select(s => new string(s.Reverse( ).ToArray( ))).ToList( );
        }

        public void FlipHorizontal( ) => Contents.Reverse( );
        public void FlipVertical( ) => Contents = Contents.Select(s => new string(s.Reverse( ).ToArray( ))).ToList( );
        public string GetEdgeUp( ) => Contents.First( );
        public string GetEdgeDown( ) => Contents.Last( );
        public string GetEdgeLeft( ) => new string(Contents.Select(s => s[0]).ToArray( ));
        public string GetEdgeRight( ) => new string(Contents.Select(s => s[width]).ToArray( ));
        public List<string> GetEdges( ) => new List<string>
        {
            GetEdgeUp(),
            GetEdgeRight(),
            GetEdgeDown(),
            GetEdgeLeft()
        };
    }
}
