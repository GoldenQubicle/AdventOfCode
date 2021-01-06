using System.Collections.Generic;
using System.Linq;
using Common;

namespace AoC2015
{
    public class Day03 : Solution
    {
        public Day03(string file) : base(file) { }

        public Day03(List<string> input) : base(input) { }

        public override string SolvePart1( )
        {
            var current = (x: 0, y: 0);
            var locations = new List<(int, int)>( );
            locations.Add(current);

            for ( int i = 0 ; i < Input[0].Length ; i++ )
            {
                current = Input[0][i] switch
                {
                    '^' => (current.x, current.y + 1),
                    '>' => (current.x + 1, current.y),
                    'v' => (current.x, current.y - 1),
                    '<' => (current.x - 1, current.y)
                };
                locations.Add(current);
            }
            return locations.Distinct( ).Count( ).ToString( );
        }

        public override string SolvePart2( )
        {
            var agg = Input[0]
            .Select((c, i) => c switch
            {
                '^' => (x: 0, y: 1, i: i),
                '>' => (x: 1, y: 0, i: i),
                'v' => (x: 0, y: -1, i: i),
                '<' => (x: -1, y: 0, i: i),
            }).Aggregate((sl: new List<(int x, int y)> { (0, 0) }, rl: new List<(int x, int y)> { (0, 0) }),
                (a, i) => i.i % 2 == 0 ?
                (sl: a.sl.Expand((a.sl.Last( ).x + i.x, a.sl.Last( ).y + i.y)), rl: a.rl) :
                (sl: a.sl, rl: a.rl.Expand((a.rl.Last( ).x + i.x, a.rl.Last( ).y + i.y))));

            agg.rl.AddRange(agg.sl);

            return agg.rl.Distinct( ).Count( ).ToString( );
        }
    }
}