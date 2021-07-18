using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Common;
using Common.Extensions;

namespace AoC2016
{
    public class Day24 : Solution
    {
        private Grid2d Grid { get; }
        public Day24(string file) : base(file, "\n")
        {
            Grid = new Grid2d(Input, diagonalAllowed: false);
        }

        public Day24(List<string> input) : base(input) { }

        public override string SolvePart1()
        {
            //foreach digit find paths to all other digits (if possible)
            //keep track of path length 
            //for all found paths go over all valid combinations (i.e. digit visited once) and calculated total length

            var paths = GetPaths();

            var pathLengths = new List<int>();

            var start = '0';
            var queue = new Queue<(char cell, List<char> visited, int length)>();
            queue.Enqueue((start, new List<char> { start }, 0));

            while (queue.Count > 0)
            {
                var (cell, visited, length) = queue.Dequeue();
                var options = paths[cell].Where(p => !visited.Contains(p.end)).ToList();

                if (options.Any())
                    foreach (var option in options)
                        queue.Enqueue((option.end, visited.Expand(option.end), length + option.length));
                else
                    pathLengths.Add(length);
            }


            return pathLengths.Min().ToString();
        }

        private Dictionary<char, List<(char end, int length)>> GetPaths()
        {
            var digitCells = Grid.QueryCells(c => char.IsDigit(c.Character));
            var paths = new List<(char start, char end, int length)>();

            foreach (var digitCell in digitCells)
            {
                var visited = new HashSet<Grid2d.GridCell>();
                var queue = new Queue<(Grid2d.GridCell cell, int steps)>();
                queue.Enqueue((digitCell, 0));

                while (queue.Count > 0)
                {
                    var (cell, length) = queue.Dequeue();
                    var openCells = Grid.GetNeighbors(cell.Position, c => !visited.Contains(c) && c.Character != '#');
                    foreach (var openCell in openCells)
                    {
                        if (char.IsDigit(openCell.Character))
                            paths.Add((digitCell.Character, openCell.Character, length + 1));

                        queue.Enqueue((openCell, length + 1));
                        visited.Add(openCell);
                    }
                }
            }

            return paths.GroupBy(path => path.start)
                .ToDictionary(group => group.Key, group => group.GroupBy(p => p.end).Select(g => (end: g.Key, length: g.Min(p => p.length))).ToList()); ;
        }

        public override string SolvePart2() => null;
    }
}