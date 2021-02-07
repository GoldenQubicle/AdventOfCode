using Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public class CellularAutomaton
    {
        private Dictionary<Position, int> Grid { get; } = new();
        private CombinatorResult<int> Offsets { get; }
        private CellularAutomatonOptions Options { get; }

        public CellularAutomaton(CellularAutomatonOptions options)
        {
            Options = options;
            Offsets = Combinator.Generate(new List<int> { -1, 0, 1 },
                new CombinatorOptions { Length = Options.Dimensions });

        }

        public void Iterate(int steps)
        {
            for(var i = 0 ; i < steps ; i++)
            {
                Grid.ForEach(cell =>
                {
                    var neighors = GetNeighbors(cell.Key);
                });
            }
        }

        private List<int> GetNeighbors(Position pos)
        {
            var n = Offsets.Select(offset => new Position(pos.Values.Select((i, v) => v + offset[i]).ToArray()));
            //TODO check for wrap around


            return new List<int>();
        }

        public void GenerateGrid(List<string> input)
        {
            switch(Options.Dimensions)
            {
                case 2:
                    for(var y = 0 ; y < input.Count ; y++)
                    {
                        for(var x = 0 ; x < input[y].Length ; x++)
                        {
                            Grid.Add(new Position(new int[ ] { x, y }), input[y][x]);
                        }
                    }
                    break;

                default: throw new ArgumentOutOfRangeException();
            }
        }
        public string CountCells(char v) => Grid.Values.Count(c => c == v).ToString();

    }

    public class Position : IEquatable<Position>
    {
        public int[ ] Values { get; init; }
        private int Dimensions;

        public Position(int[ ] values)
        {
            Values = values;
            Dimensions = Values.Length;
        }

        public bool Equals(Position other)
        {
            if(other.Dimensions != Dimensions) return false;

            bool EqualValue = true;
            for(var i = 0 ; i < Dimensions ; i++)
            {
                if(other.Values[i] != Values[i])
                {
                    EqualValue = false;
                    break;
                }
            }
            return EqualValue;
        }

        public override bool Equals(object obj)
        {
            if(obj == null || obj == default) return false;

            if(obj.GetType() != typeof(Position)) return false;

            return Equals((Position) obj);
        }

        public override int GetHashCode( )
        {
            unchecked
            {
                int hash = 17;
                foreach(var item in Values)
                {
                    hash = 31 * hash + item.GetHashCode();
                }
                return hash;
            }
        }

        public static bool operator ==(Position left, Position right) => EqualityComparer<Position>.Default.Equals(left, right);
        public static bool operator !=(Position left, Position right) => !(left == right);
    }
}
