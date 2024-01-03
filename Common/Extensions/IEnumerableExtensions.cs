using System;
using System.Collections.Concurrent;
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

        public static ConcurrentBag<T> ToConcurrentBag<T>(this IEnumerable<T> collection)
        {
	        var result = new ConcurrentBag<T>();
            collection.ForEach(result.Add);
	        return result;
        }
    }
}
