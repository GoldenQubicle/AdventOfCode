using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Extensions;

namespace AoC2021
{
    public class Day09 : Solution
    {
        private readonly Grid2d grid;
        public Day09(string file) : base(file) => grid = new Grid2d(Input, false);

        public override string SolvePart1( ) => grid
            .Where(c => grid.GetNeighbors(c.Position).All(n => n.Character.ToInt() > c.Character.ToInt()))
            .Sum(c => c.Character.ToInt() + 1).ToString();

        public override string SolvePart2()
        {
            var lowPoints = grid.Where(c => grid.GetNeighbors(c.Position).All(n => n.Character.ToInt() > c.Character.ToInt()));
            var visited = new HashSet<Position>();

            return lowPoints.Aggregate(new List<long>(), (basins, cell) =>
            {
                var count = 1;
                var cells = new Queue<Grid2d.Cell>();
                visited.Add(cell.Position);
                cells.Enqueue(cell);

                while (cells.Any())
                {
                    var c = cells.Dequeue();
                    var neighbors = grid.GetNeighbors(c.Position)
                        .Where(n => n.Character.ToInt() != 9 && !visited.Contains(n.Position)).ToList();

                    count += neighbors.Count;
                    neighbors.ForEach(n =>
                    {
                        visited.Add(n.Position);
                        cells.Enqueue(n);
                    });
                }
                basins.Add(count);
                return basins;
            }).OrderByDescending(l => l).Take(3).Aggregate(1L, (sum, count) => count * sum).ToString();
        }
    }
}