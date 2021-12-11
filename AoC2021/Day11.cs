using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common;
using Common.Extensions;

namespace AoC2021
{
    public class Day11 : Solution
    {
        private Grid2d grid;
        public Day11(string file) : base(file) => grid = new Grid2d(Input, diagonalAllowed: true);

        public override string SolvePart1()
        {
            var flashes = 0L;
            for (var i = 0; i < 100; i++)
            {
                var hasFlashed = new HashSet<Grid2d.Cell>();

                grid.ForEach(c => c.Value++);

                while (grid.GetCells(c => c.Value > 9 && !hasFlashed.Contains(c)).Any())
                {
                    grid.GetCells(c => c.Value > 9 && !hasFlashed.Contains(c)).ForEach(c =>
                    {
                        flashes++;
                        hasFlashed.Add(c);
                        grid.GetNeighbors(c.Position).ForEach(n => n.Value++);
                    });
                }
                hasFlashed.ForEach(c => c.Value = 0);
            }
            return flashes.ToString();
        }

        public override string SolvePart2()
        {
            grid = new Grid2d(Input, diagonalAllowed: true);
            var step = 0;

            while (grid.GetCells(c => c.Value == 0).Count != 100)
            {
                var hasFlashed = new HashSet<Grid2d.Cell>();

                grid.ForEach(c => c.Value++);

                while (grid.GetCells(c => c.Value > 9 && !hasFlashed.Contains(c)).Any())
                {
                    grid.GetCells(c => c.Value > 9 && !hasFlashed.Contains(c)).ForEach(c =>
                    {
                        hasFlashed.Add(c);
                        grid.GetNeighbors(c.Position).ForEach(n => n.Value++);
                    });
                }
                hasFlashed.ForEach(c => c.Value = 0);
                step++;
            }

            return step.ToString();
        }
    }
}