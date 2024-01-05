using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common;

namespace AoC2016
{
    public class Day13 : Solution
    {
        public int FavoriteNumber { get; set; } = 1364;
        public (int x, int y) Target { get; set; } = (31, 39);
        public (int rows, int columns) Grid { get; set; } = (50, 50);

        private List<(int x, int y)> offsets = new()
        {
            (-1, 0),
            (0, -1),
            (1, 0),
            (0, 1)
        };

        public Day13(string file) : base(file) { }

        public Day13(List<string> input) : base(input) { }

        public override async Task<string> SolvePart1( )
        {
            var layout = ConstructLayout(Grid.rows, Grid.columns);
            var current = (pos: (x: 1, y: 1), steps: 0);
            var visited = new List<(int, int)> { current.pos };
            var queue = new Queue<((int, int), int)>();
            queue.Enqueue(current);

            while(current.pos != Target)
            {
                current = queue.Dequeue();
                visited.Add(current.pos);
                var neighbors = GetNeighbors(current, layout, visited);
                neighbors.ForEach(n => queue.Enqueue(n));
            }
            return current.steps.ToString();
        }

        public override async Task<string> SolvePart2( )
        {
            var layout = ConstructLayout(Grid.rows, Grid.columns);
            var current = (pos: (x: 1, y: 1), steps: 0);
            var visited = new List<(int, int)> { };
            var reached = new HashSet<((int, int), int)>();
            var queue = new Queue<((int, int), int)>();
            queue.Enqueue(current);

            while(queue.Any())
            {
                current = queue.Dequeue();
                reached.Add(current);
                visited.Add(current.pos);
                var neighbors = GetNeighbors(current, layout, visited);
                neighbors.ForEach(n =>
                {
                    queue.Enqueue(n);
                });
            }

            return reached.Count(t => t.Item2 <= 50).ToString();
        }


        private List<((int x, int y) pos, int steps)> GetNeighbors(((int x, int y) pos, int steps) current, List<List<char>> layout, List<(int, int)> visited)
        {
            var neighbors = new List<((int, int), int)>();
            foreach(var offset in offsets)
            {
                var x = current.pos.x + offset.x;
                var y = current.pos.y + offset.y;
                if(x >= 0 && x < layout.First().Count && y >= 0 && y < layout.Count)
                {
                    if(layout[y][x] == '.' && !visited.Contains((x, y)))
                        neighbors.Add(((x, y), current.steps + 1));
                }
            }

            return neighbors;
        }


        public List<List<char>> ConstructLayout(int rows, int columns)
        {
            var layout = new List<List<char>>();
            for(var row = 0 ; row < rows ; row++)
            {
                layout.Add(new List<char>());

                for(var column = 0 ; column < columns ; column++)
                {
                    layout[row].Add(IsOpenSpace(column, row) ? '.' : '#');
                }
            }
            return layout;
        }

        private bool IsOpenSpace(int x, int y) => Convert.ToString(x * x + 3 * x + 2 * x * y + y + y * y + FavoriteNumber, 2).Count(b => b == '1') % 2 == 0;

    }
}