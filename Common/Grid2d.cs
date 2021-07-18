using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Combinatorics.Collections;
using Microsoft.VisualBasic.CompilerServices;

namespace Common
{
    public class Grid2d
    {
        private Dictionary<Position, GridCell> Cells { get; } = new();

        private List<Position> Offsets { get; }

        public Grid2d(List<string> input, bool diagonalAllowed = true)
        {
            for (var y = 0; y < input.Count; y++)
            {
                for (var x = 0; x < input[y].Length; x++)
                {
                    var gc = new GridCell { Position = new Position(x, y), Character = input[y][x] };
                    Cells.Add(gc.Position, gc);
                }
            }

            var filter = diagonalAllowed
                ? new List<Position> { new(0, 0) }
                : new List<Position> { new(-1, -1), new(-1, 1), new(1, -1), new(1, 1), new(0, 0) };

            Offsets = new Variations<int>(new[] { -1, 0, 1 }, 2, GenerateOption.WithRepetition)
                .Select(v => new Position(v[0], v[1]))
                .Where(p => !filter.Contains(p))
                .ToList();

        }

        public List<GridCell> GetNeighbors(Position pos, Func<GridCell, bool> query) =>
            GetNeighbors(pos).Where(query).ToList();

        public List<GridCell> GetNeighbors(Position pos) => Offsets
            .Select(o => o + pos)
            .Where(np => Cells.ContainsKey(np))
            .Select(np => Cells[np]).ToList();

        public List<GridCell> QueryCells(Func<GridCell, bool> query) => Cells.Values.Where(query).ToList();

        public void AddGridCell(GridCell gridCell) => Cells.Add(gridCell.Position, gridCell);


        public struct GridCell : IEquatable<GridCell>
        {
            public Position Position { get; init; }
            public char Character { get; init; }

            public GridCell ChangeCharacter(char newChar) => new() { Position = Position, Character = newChar };

            public bool Equals(GridCell other) => Equals(Position, other.Position) && Character == other.Character;

            public override bool Equals(object obj) => obj is GridCell other && Equals(other);

            public override int GetHashCode() => HashCode.Combine(Position, Character);

            public static bool operator ==(GridCell left, GridCell right) => EqualityComparer<GridCell>.Default.Equals(left, right);
            public static bool operator !=(GridCell left, GridCell right) => !(left == right);

        }


    }
}
