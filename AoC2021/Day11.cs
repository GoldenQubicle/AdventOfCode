using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Extensions;

namespace AoC2021
{
    public class Day11 : Solution
    {
        private readonly Grid2d grid;
        public Day11(string file) : base(file) => grid = new Grid2d(Input, diagonalAllowed: true);

        public override string SolvePart1() => Enumerable.Range(0, 100).Aggregate(0L, (flashes, _) => flashes += DoFlashes()).ToString();

        public override string SolvePart2()
        {
            var step = 0;

            while (grid.GetCells(c => c.Value == 0).Count != 100)
            {
                DoFlashes();
                step++;
            }

            return step.ToString();
        }

        private int DoFlashes()
        {
            var hasFlashed = new HashSet<Grid2d.Cell>();
            grid.ForEach(c => c.Value++);
            var flashers = grid.GetCells(c => c.Value > 9 && !hasFlashed.Contains(c));

            while (flashers.Any())
            {
                flashers.ForEach(c =>
                {
                    hasFlashed.Add(c);
                    grid.GetNeighbors(c.Position).ForEach(n => n.Value++);
                });
                flashers = grid.GetCells(c => c.Value > 9 && !hasFlashed.Contains(c));
            }

            hasFlashed.ForEach(c => c.Value = 0);
            return hasFlashed.Count;
        }
    }
}