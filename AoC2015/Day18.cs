using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Extensions;

namespace AoC2015
{
    public class Day18 : Solution
    {
        private Dictionary<(int x, int y), char> grid;
        private readonly List<(int x, int y)> offsets;

        public int Steps { get; set; } = 100;

        public Day18(string file) : base(file, "\n")
        {
            grid = Input.SelectMany((line, y) => line.Select((c, x) => new KeyValuePair<(int, int), char>((x, y), c)))
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            offsets = Combinator.Generate(new List<int> { -1, 0, 1 }, new CombinatorOptions { Length = 2 })
                .Where(r => !r.All(v => v == 0)).Select(r => (r[0], r[1])).ToList();
        }

        public override string SolvePart1( )
        {
            for(var s = 0 ; s < Steps ; s++)
            {
                var newState = new Dictionary<(int, int), char>();
                grid.ForEach(cell =>
                {
                    var neigbors = offsets.Select(o =>
                    {
                        var p = (cell.Key.x + o.x, cell.Key.y + o.y);

                        if(grid.ContainsKey(p)) return grid[p];
                        else return '.';
                    });

                    var active = neigbors.Count(c => c == '#');
                    var state = active switch
                    {
                        < 2 or > 3 when cell.Value == '#' => '.',
                        3 when cell.Value == '.' => '#',
                        _ => cell.Value
                    };
                    newState.Add(cell.Key, state);
                });
                grid = newState;
            }

            return grid.Values.Count(v => v == '#').ToString();
        }

        public override string SolvePart2( ) => null;
    }
}