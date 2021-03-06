﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Combinatorics.Collections;

namespace Common
{
    public class Grid2d : IEnumerable<Grid2d.Cell>
    {
        private Dictionary<Position, Cell> Cells { get; } = new();

        private List<Position> Offsets { get; }

        public Grid2d(bool diagonalAllowed = true)
        {
            var filter = diagonalAllowed
                ? new List<Position> { new(0, 0) }
                : new List<Position> { new(-1, -1), new(-1, 1), new(1, -1), new(1, 1), new(0, 0) };

            Offsets = new Variations<int>(new[] { -1, 0, 1 }, 2, GenerateOption.WithRepetition)
                .Select(v => new Position(v[0], v[1]))
                .Where(p => !filter.Contains(p))
                .ToList();
        }

        public Grid2d(List<string> input, bool diagonalAllowed = true) : this(diagonalAllowed)
        {
            for (var y = 0; y < input.Count; y++)
            {
                for (var x = 0; x < input[y].Length; x++)
                {
                    var gc = new Cell { Position = new Position(x, y), Character = input[y][x] };
                    Cells.Add(gc.Position, gc);
                }
            }
        }
        
        public List<Cell> GetNeighbors(Position pos, Func<Cell, bool> query) =>
            GetNeighbors(pos).Where(query).ToList();

        public List<Cell> GetNeighbors(Position pos) => Offsets
            .Select(o => o + pos)
            .Where(np => Cells.ContainsKey(np))
            .Select(np => Cells[np]).ToList();

        public List<Cell> QueryCells(Func<Cell, bool> query) => Cells.Values.Where(query).ToList();

        public void Add(Cell cell) => Cells.Add(cell.Position, cell);

        public IEnumerator<Cell> GetEnumerator()
        {
            foreach (var cell in Cells.Values)
                yield return cell;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public struct Cell : IEquatable<Cell>
        {
            public Position Position { get; init; }
            public char Character { get; init; }

            /// <summary>
            /// returns a new Cell with same position but new character
            /// </summary>
            /// <param name="newChar"></param>
            /// <returns></returns>
            public Cell ChangeCharacter(char newChar) => new() { Position = Position, Character = newChar };

            public bool Equals(Cell other) => Equals(Position, other.Position) && Character == other.Character;

            public override bool Equals(object obj) => obj is Cell other && Equals(other);

            public override int GetHashCode() => HashCode.Combine(Position, Character);

            public static bool operator ==(Cell left, Cell right) => EqualityComparer<Cell>.Default.Equals(left, right);
            public static bool operator !=(Cell left, Cell right) => !(left == right);

        }
    }
}
