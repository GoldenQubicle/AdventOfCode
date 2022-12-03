using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Common
{
    public class Position : IEquatable<Position>
    {
        public int[ ] Values { get; init; }
        private readonly int dimensions;

        public Position(params int[ ] values)
        {
            Values = values;
            dimensions = Values.Length;
        }

        public bool Equals(Position other)
        {
            if (other?.Values is null) return false;

            if (other.dimensions != dimensions) return false;
            
            var equalValue = true;

            for (var i = 0; i < dimensions; i++)
            {
                if (other.Values[i] == Values[i]) continue;

                equalValue = false;
                break;
            }
            return equalValue;
        }

        public override bool Equals(object obj) => obj is Position other && Equals(other);
        
        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                foreach (var item in Values)
                {
                    hash = 31 * hash + item.GetHashCode();
                }
                return hash;
            }
        }

        public static bool operator ==(Position left, Position right) => EqualityComparer<Position>.Default.Equals(left, right);
        public static bool operator !=(Position left, Position right) => !(left == right);
        public static Position operator +(Position left, Position right) => new(left.Values.Select((v, i) => v + right.Values[i]).ToArray());

       public static bool operator <(Position left, Position right) => left.Values[0] < right.Values[0] && left.Values[1] < right.Values[1];
       public static bool operator >(Position left, Position right) => left.Values[0] > right.Values[0] && left.Values[1] > right.Values[1];

       public static bool operator <=(Position left, Position right) =>left.Values[0] <= right.Values[0] && left.Values[1] <= right.Values[1];
       public static bool operator >=(Position left, Position right) => left.Values[0] >= right.Values[0] && left.Values[1] >= right.Values[1];

    }
}