using System.Collections.Generic;
using System.Linq;

namespace AoC2020.Solutions
{
    public class Day03 : Solution<long>
    {
        public int Width;
        public int Height;
        public Day03(string file) : base(file)
        {
            Height = Input.Count;
            Width = Input[0].Length;
        }
        public override long SolvePart1( ) => TraverseSlope(3, 1);

        public override long SolvePart2( )
        {
            var slope1 = TraverseSlope(1, 1);
            var slope2 = TraverseSlope(3, 1);
            var slope3 = TraverseSlope(5, 1);
            var slope4 = TraverseSlope(7, 1);
            var slope5 = TraverseSlope(1, 2);
            return slope1 * slope2 * slope3 * slope4 * slope5;
        }

        public long TraverseSlope(int xIncr, int yIncr)
        {
            var pos = (x: 1, y: 1);
            var tiles = new List<char>( );

            while ( pos.y <= Height )
            {
                tiles.Add(GetTerrainTile(pos.x, pos.y));
                pos.x += xIncr;
                pos.y += yIncr;
            }
            return tiles.Count(t => t == '#');
        }

        public char GetTerrainTile(int x, int y)
        {
            var r = x >= Width ? x % Width : x;
            return Input[y - 1][r == 0 ? Width - 1 : r - 1];
        }
    }
}
