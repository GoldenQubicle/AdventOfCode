using System;
using System.Collections.Generic;

namespace Common.Extensions
{
    public static class IEnumerableExtensions
    {
        public static void ForEach<T> (this IEnumerable<T> collection, Action<T> action)
        {
            foreach(var item in collection )
            {
                action(item);
            }
        }
    }
}
