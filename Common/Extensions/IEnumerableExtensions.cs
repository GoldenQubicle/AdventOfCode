using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

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

        public static CircularList<T> ToCircularList<T>(this IEnumerable<T> collection)
        {
	        var result = new CircularList<T>();
            collection.ForEach(result.Add);
            result.ResetHead();
            return result;
        }

        public static ConcurrentBag<T> ToConcurrentBag<T>(this IEnumerable<T> collection)
        {
	        var result = new ConcurrentBag<T>();
            collection.ForEach(result.Add);
	        return result;
        }

        public static ConcurrentDictionary<TKey, TValue> ToConcurrentDictionary<TSource, TKey, TValue>(
	        this IEnumerable<TSource> collection, Func<TSource, TKey> keySelector, Func<TSource, TValue> valueSelector)
        {
	        var result = new ConcurrentDictionary<TKey, TValue>();
	        foreach (var item in collection)
	        {
		        result.TryAdd(keySelector(item), valueSelector(item));
	        }

	        return result;
        }

        public static IEnumerable<(T Value, int idx)> WithIndex<T>(this IEnumerable<T> collection) =>
	        collection.Select((e, idx) => (e, idx));
    }
}
