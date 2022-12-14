using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Combinatorics.Collections;
using Common.Extensions;

namespace Common
{
    public class Grid2d : IEnumerable<Grid2d.Cell>
    {
        public int Width { get; init; }
        public int Height { get; init; }
        public int Count => Cells.Count;
        private Dictionary<Position, Cell> Cells { get; } = new();

        private List<Position> Offsets { get; }

        public Grid2d(bool diagonalAllowed = true)
        {
            List<Position> filter = diagonalAllowed
                ? new() { new(0, 0) }
                : new() { new(-1, -1), new(-1, 1), new(1, -1), new(1, 1), new(0, 0) };

            Offsets = new Variations<int>(new[] { -1, 0, 1 }, 2, GenerateOption.WithRepetition)
                .Select(v => new Position(v[0], v[1]))
                .Where(p => !filter.Contains(p))
                .ToList();
        }

        public Grid2d(IReadOnlyList<string> input, bool diagonalAllowed = true) : this(diagonalAllowed)
        {
            Width = input[0].Length;
            Height = input.Count;

            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++) // assuming all lines are equal length
                {
                    var gc = new Cell(new(x, y), input[y][x]); //yes really input[y][x], it reads wrong but is right - still dealing with a list here. 
                    Cells.Add(gc.Position, gc);
                }
            }
        }

        public Cell this[int x, int y] => Cells[new(x, y)];
        public Cell this[Cell c] => Cells[c.Position];

        public Cell this[Position p] => Cells[p];
        /// <summary>
        /// Returns the neighbors for the given position.
        /// Does not wrap around the grid by default (i.e. a corner cell returns 3 neighbors max)
        /// For a connected cell returns 8 neighbors when diagonal is allowed (default), returns 4 neighbors otherwise. 
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public List<Cell> GetNeighbors(Cell cell) => Offsets
            .Select(o => o + cell.Position)
            .Where(np => Cells.ContainsKey(np))
            .Select(np => Cells[np]).ToList();

        public List<Cell> GetNeighbors(Position position) => Offsets
            .Select(o => o + position)
            .Where(np => Cells.ContainsKey(np))
            .Select(np => Cells[np]).ToList();

        public List<Cell> GetNeighbors(Cell cell, Func<Cell, bool> query) =>
            GetNeighbors(cell).Where(query).ToList();

        public List<Cell> GetNeighbors(Position p, Func<Cell, bool> query) =>
            GetNeighbors(p).Where(query).ToList();

        public List<Cell> GetCells(Func<Cell, bool> query) => Cells.Values.Where(query).ToList();

        public void Add(Cell cell) => Cells.Add(cell.Position, cell);

        public void AddOrUpdate(Cell cell)
        {
            if (Cells.ContainsKey(cell.Position))
                Cells[cell.Position] = cell;
            else
                Cells.Add(cell.Position, cell);
        }

        public IEnumerator<Cell> GetEnumerator() => ((IEnumerable<Cell>)Cells.Values).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Simple shortest path solver using a priority queue.
        /// Note it operates on the instanced grid, i.e. multiple calls are not possible and require new grid2d. Kinda sucky, I know. 
        /// </summary>
        /// <param name="start">The Start Cell</param>
        /// <param name="target">The Target Cell</param>
        /// <param name="constraint">Predicate used when getting neighbors for the dequeued cell. Current cell is the 1st argument, neighbor cell the 2nd.</param>
        /// <param name="targetCondition">Predicate used to break out of while loop. Current cell is the 1st argument, target cell the 2nd.</param>
        /// <returns></returns>
        public List<Cell> GetShortestPath(Cell start, Cell target, Func<Cell, Cell, bool> constraint, Func<Cell, Cell, bool> targetCondition)
        {
            Cells.Values.ForEach(c => c.Distance = Math.Abs(target.X - c.X) + Math.Abs(target.Y - c.Y));
            var path = new List<Cell>();
            var visited = new Dictionary<Position, bool>();
            var queue = new PriorityQueue<Cell, long>();

            queue.Enqueue(start, start.GetOverallCost);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                visited[current.Position] = true;

                if (targetCondition(current, target))
                {
                    while (current.Parent is not null)
                    {
                        path.Add(current.Parent);
                        current = current.Parent;
                    }
                    break;
                }

                GetNeighbors(current, n => !visited.ContainsKey(n.Position) && constraint(current, n))
                    .Select(n => n with { Parent = current, Cost = current.Cost + 1 })
                    .ForEach(n => queue.Enqueue(n, n.GetOverallCost));

            }

            return path;
        }


        public record Cell
        {
            public Position Position { get; init; }
            public char Character { get; init; }
            public long Value { get; set; }
            public Cell Parent { get; init; }
            public long Cost { get; init; }
            public long Distance { get; set; }
            public long GetOverallCost => Cost + Distance;
            public int X => Position[0];
            public int Y => Position[1];

            public Cell(Position position, char character)
            {
                Position = position;
                Character = character;
                Value = char.IsDigit(Character) ? Character.ToLong() : -1;
            }

            /// <summary>
            /// returns a new Cell with same position but new character
            /// </summary>
            /// <param name="newChar"></param>
            /// <returns></returns>
            public Cell ChangeCharacter(char newChar) => new(Position, newChar);

        }

        public override string ToString()
        {
            var minx = Cells.Values.Min(c => c.X);
            var maxx = Cells.Values.Max(c => c.X);
            var miny = Cells.Values.Min(c => c.Y);
            var maxy = Cells.Values.Max(c => c.Y);

            var sb = new StringBuilder();

            for (var y = miny; y <= maxy; y++)
            {
                for (var x = minx; x <= maxx; x++)
                {
                    var p = new Position(x, y);
                    sb.Append(Cells[p].Character);
                }
                sb.Append("\n");
            }
            return sb.ToString();
        }

        /// <summary>
        /// Helper method to fill out the grid in case the input is sparse. 
        /// </summary>
        /// <param name="blank">The character to use for empty cells</param>
        public void Fill(char blank)
        {
            var minx = Cells.Values.Min(c => c.X);
            var maxx = Cells.Values.Max(c => c.X);
            var miny = 0;//Cells.Values.Min(c => c.Y);//2022 day 14 hack
            var maxy = Cells.Values.Max(c => c.Y);

            for (var y = miny; y <= maxy; y++)
            {
                for (var x = minx; x <= maxx; x++)
                {
                    var c = new Cell(new(x, y), blank);
                    Cells.TryAdd(c.Position, c);
                }
            }
        }
    }
}