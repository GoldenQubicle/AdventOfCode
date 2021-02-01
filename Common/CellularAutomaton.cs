using System;
using System.Collections.Generic;

namespace Common
{
    public class CellularAutomaton
    {
        Dictionary<Position, int> grid;

        public CellularAutomaton(CellularAutomatonOptions options)
        {

        }
    }

    public class Position : IEquatable<Position>
    {
        public int[ ] Values { get; init; }
        private int Dimensions;

        public Position(int[] values)
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

            return Equals((Position)obj);
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
