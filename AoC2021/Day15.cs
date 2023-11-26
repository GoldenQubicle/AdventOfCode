using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Extensions;
namespace AoC2021
{
    public class Day15 : Solution
    {
        private Grid2d grid;
        public Day15(string file) : base(file) =>
            grid = new Grid2d(Input, diagonalAllowed: false);

        public override string SolvePart1()
        {
            var queue = new PriorityQueue<(Grid2d.Cell ptr, List<Grid2d.Cell> visited), long>();
            var paths = new List<(List<Grid2d.Cell> path, long risk)>();
            var end = grid[(grid.Width- 1, grid.Height - 1)];

            var start = (ptr: grid[(0, 0)], visited: new List<Grid2d.Cell>());
            queue.Enqueue(start, 0l);

            while (queue.Count > 0)
            {
                queue.TryDequeue(out var current, out var risk);

                if (current.ptr == end)
                {
                    return risk.ToString();
                }

                grid.GetNeighbors(current.ptr, n => !current.visited.Contains(n))
                    .ForEach( n => queue.Enqueue((n, current.visited.Expand(current.ptr)), risk + n.Value));
                
            }

            return string.Empty;
        }

        public override string SolvePart2() => string.Empty;
    }
}