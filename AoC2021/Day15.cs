using System.Collections.Generic;
using System.Linq;
using Common;

namespace AoC2021
{
    public class Day15 : Solution
    {
        private Grid2d grid;
        public Day15(string file) : base(file) => grid = new Grid2d(Input, diagonalAllowed: false);

        public override string SolvePart1()
        {
            var risks = new List<long>();
            var queue = new List<(Grid2d.Cell cell, long risk)>();
            var visited = new List<Grid2d.Cell>();
            var start = grid.First(c => c.Position.Values[0] == 0 && c.Position.Values[1] == 0);
            var end = grid.First(c => c.Position.Values[0] == grid.Dimensions.x - 1 && c.Position.Values[1] == grid.Dimensions.y - 1);
            queue.Add((start, 0L));

            while (queue.Any())
            {
                var current = queue.First();
                queue.RemoveAt(0);
                visited.Add(current.cell);

                if (current.cell == end)
                {
                    risks.Add(current.risk);
                    continue;
                }
                grid.GetNeighbors(current.cell, n => !visited.Contains(n))
                    .ForEach(n => queue.Add((n, current.risk + n.Value)));

                queue = queue.OrderBy(q => q.risk).ToList();
            }

            return risks.Min().ToString();
        }

        public override string SolvePart2() => null;
    }
}