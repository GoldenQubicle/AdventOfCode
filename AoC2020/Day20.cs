namespace AoC2020
{
    public class Day20 : Solution
    {
        public readonly Dictionary<int, Tile> tiles = new( );

        List<Action<Tile>> tileConfigurations = new List<Action<Tile>>
            {
               tile => tile.Rotate90Clockwise(),
               tile => tile.Rotate90Clockwise(),
               tile => tile.Rotate90Clockwise(),
               tile => tile.FlipVertical(),
               tile => tile.Rotate90Clockwise(),
               tile => tile.Rotate90Clockwise(),
               tile => tile.Rotate90Clockwise(),
            };

        public Day20(string file) : base(file, "\r\n\r\n")
        {
            tiles = Input.Select(i => i.Split("\r\n")).Select(i =>
               new Tile(int.Parse(i[0].Where(char.IsDigit).ToArray( )), i.Skip(1).ToList( )))
                .ToDictionary(t => t.Id, t => t);
        }

        public override async Task<string> SolvePart1( )
        {
            var corners = GetMatchedTiles( )
            .Where(m => m.matches.Count == 2).ToList( );
            return corners.Aggregate(1L, (sum, match) => sum *= match.tile.Id).ToString( );

        }

        private List<(Tile tile, List<int> matches)> GetMatchedTiles( )
        {
            var matches = new Dictionary<Tile, List<int>>( );
            var tileEdges = tiles.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.GetEdges( ));

            Action<Tile, Dictionary<int, List<string>>> addMatches = (tile, otherTiles) =>
                   matches[tile].AddRange(otherTiles
                        .Select(ot => ot.Value.Contains(tile.GetEdgeUp( )) ? ot.Key : 0));

            foreach ( var (id, tile) in tiles )
            {
                matches.Add(tile, new List<int>( ));
                var otherTiles = tileEdges.Where(kvp => kvp.Key != tile.Id).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

                addMatches(tile, otherTiles);

                tileConfigurations.ForEach(config =>
                {
                    config(tile);
                    addMatches(tile, otherTiles);
                });
            }

            return matches.Select(m => (tile: m.Key, matches: m.Value.Where(v => v != 0).ToList( ))).ToList( );
        }

        public override async Task<string> SolvePart2( )
        {
            var size = ( int ) Math.Sqrt(tiles.Count);
            var picture = new List<List<Tile>>( );
            Enumerable.Range(0, size).ToList( ).ForEach(n => picture.Add(new List<Tile>(new Tile[size])));
            var matches = GetMatchedTiles( ).ToDictionary(m => m.tile, m => m.matches);
            var placed = new List<int>( );

            void DoTileAction(Tile tile, int i) => tileConfigurations[i](tile);
            Func<Tile, Tile, bool> matchEdges = (t1, t2) => t1.GetEdgeRight( ).Equals(t2.GetEdgeLeft( ));

            //assume first corner is in top left, and the first match goes to the right
            var (topLeft, neighbors) = matches.First(kvp => kvp.Value.Count == 2);
            var rightNeighbor = matches.First(kvp => kvp.Key.Id.Equals(neighbors.First( ))).Key;

            //allign top left with its right hand side neighbor first of all
            var cornerActions = 0;
            var neighborActions = 0;
            while ( !matchEdges(topLeft, rightNeighbor) )
            {
                DoTileAction(topLeft, cornerActions);

                cornerActions++;

                if ( cornerActions == tileConfigurations.Count )
                {
                    DoTileAction(rightNeighbor, neighborActions);
                    neighborActions++;
                    cornerActions = 0;
                }
            }

            // get the other neighbor for the topleft, which MUST be below it
            neighbors.Remove(rightNeighbor.Id);
            var downNeighbor = matches.First(m => m.Key.Id.Equals(neighbors.First( ))).Key;
            var downAction = 0;

            while ( !topLeft.GetEdgeDown( ).Equals(downNeighbor.GetEdgeUp( )) )
            {
                DoTileAction(downNeighbor, downAction);
                downAction++;
                //couldn't line it up, lets flip both already in place and try again
                if ( downAction == tileConfigurations.Count )
                {
                    topLeft.FlipHorizontal( );
                    rightNeighbor.FlipHorizontal( );
                    downAction = 0;
                }
            }
            // now we have the top left corner fixed in place
            // however not placing the down neighbor to prevent special case in the while loop below
            picture[0][0] = topLeft;
            picture[0][1] = rightNeighbor;
            placed.AddRange(new List<int> { topLeft.Id, rightNeighbor.Id });


            var pos = (y: 0, x: 2);
            var current = rightNeighbor;
            var nextTiles = matches[current].Except(placed).Select(id => tiles[id]).ToList( );

            while ( nextTiles.Any( ) )
            {
                foreach ( var nextTile in nextTiles )
                {
                    var tileAction = 0;

                    while ( !matchEdges(current, nextTile) )
                    {
                        DoTileAction(nextTile, tileAction);
                        tileAction++;
                        if ( tileAction == tileConfigurations.Count ) break;
                    }

                    if ( tileAction < tileConfigurations.Count )
                    {
                        placed.Add(nextTile.Id);
                        picture[pos.y][pos.x] = nextTile;
                        current = nextTile;
                        pos.x++;
                        break;
                    }
                }

                // set matchEdges here because it can be overriden below in case of a new row
                matchEdges = (t1, t2) => t1.GetEdgeRight( ).Equals(t2.GetEdgeLeft( ));

                // reached end of the row, go to the next one
                if ( pos.x == size )
                {
                    pos.x = 0;
                    pos.y++;
                    current = picture[pos.y - 1][pos.x];
                    matchEdges = (t1, t2) => t1.GetEdgeDown( ).Equals(t2.GetEdgeUp( )); // check from the row above
                }

                nextTiles = matches[current].Except(placed).Select(id => tiles[id]).ToList( );
            }

            picture.ForEach(row => row.ForEach(t => t.TrimEdges( )));

            var tileEntries = picture[0][0].Contents.Count;

            var final = picture.Aggregate(new List<string>( ), (list, row) =>
             {
                 list.AddRange(row.Aggregate(new List<string>(new string[tileEntries]),
                      (list, tile) => list = list.Select((s, i) => s + tile.Contents[i]).ToList( )));
                 return list;
             });

            var tile = new Tile(0, final);
            var upper = new Regex(".{18}#{1}.");
            var middle = new Regex("#{1}.{4}#{2}.{4}#{2}.{4}#{3}");
            var lower = new Regex(".{1}#{1}.{2}#{1}.{2}#{1}.{2}#{1}.{2}#{1}.{2}#{1}.{3}");

            Func<Tile, int> regexMatches = tile => tile.Contents
                .Select(s => middle.Matches(s))
                .Select((m, i) => m.Count == 1 &&
                        upper.IsMatch(tile.Contents[i - 1]) &&
                        lower.IsMatch(tile.Contents[i + 1]) ? true : false)
                .Count(b => b);

            tile.Rotate90AntiClockwise( ); // total hack needed for the final picture (not the example though..)

            var actions = 0;
            var count = regexMatches(tile);
            while ( actions < tileConfigurations.Count )
            {
                count = regexMatches(tile) > count ? regexMatches(tile) : count;
                DoTileAction(tile, actions);
                actions++;
            }

            return (tile.Contents.Sum(s => s.Count(c => c == '#')) - ( count * 15 )).ToString( );
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
