using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extensions
{
    public static class HashSetExtensions
    {
        public static HashSet<T> Expand<T>(this HashSet<T> set, T item)
        {
            set.Add(item);
            return set;
        }
    }
}
