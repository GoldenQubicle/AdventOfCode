using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Common.Extensions;

namespace AoC2020
{
    public class Day03 : Solution
    {
        public int Width;
        public int Height;
        public Day03(string file) : base(file)
        {
            Height = Input.Count;
            Width = Input[0].Length;
        }
        public override async Task<string> SolvePart1( ) => TraverseSlope((3, 1)).ToString( );

        public override async Task<string> SolvePart2( ) => new List<(int, int)>
            { (1, 1), (3, 1), (5, 1), (7, 1), (1, 2) }
            .Select(TraverseSlope)
            .Aggregate((s1, s2) => s1 * s2).ToString( );

        public long TraverseSlope((int, int) incr)
        {
            var pos = (x: 0, y: 0);
            var tiles = new List<char>( );

            while ( pos.y < Height )
            {
                tiles.Add(GetTerrainTile(pos));
                pos = pos.Add(incr);
            }
            return tiles.Count(t => t == '#');
        }

        public char GetTerrainTile((int x, int y) i) => Input[i.y][i.x >= Width ? i.x % Width : i.x];

    }
}
