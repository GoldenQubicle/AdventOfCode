namespace Common.Extensions;

public static class IEnumerableExtensions
{
	public static IEnumerable<(int idx1, int idx2)> GetIndexPairs<T>(this IEnumerable<T> collection) =>
		Enumerable.Range(0, collection.Count() - 1)
			.SelectMany(a1 => Enumerable.Range(a1 + 1, collection.Count( ) - 1 - a1).Select(a2 => (a1, a2)));

	
	public static IEnumerable<T> Expand<T>(this IEnumerable<T> list, T item) => [.. list, item];

	public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
	{
		foreach (var item in collection)
		{
			action(item);
		}
	}

	public static CircularList<T> ToCircularList<T>(this IEnumerable<T> collection)
	{
		var result = new CircularList<T>( );
		collection.ForEach(result.Add);
		result.ResetHead( );
		return result;
	}

	//used in sharp ray for visualisations
	public static ConcurrentBag<T> ToConcurrentBag<T>(this IEnumerable<T> collection)
	{
		var result = new ConcurrentBag<T>( );
		collection.ForEach(result.Add);
		return result;
	}

	//used in sharp ray for visualisations
	public static ConcurrentDictionary<TKey, TValue> ToConcurrentDictionary<TSource, TKey, TValue>(
		this IEnumerable<TSource> collection, Func<TSource, TKey> keySelector, Func<TSource, TValue> valueSelector)
	{
		var result = new ConcurrentDictionary<TKey, TValue>( );
		foreach (var item in collection)
		{
			result.TryAdd(keySelector(item), valueSelector(item));
		}

		return result;
	}

	public static IEnumerable<(T Value, int idx)> WithIndex<T>(this IEnumerable<T> collection) =>
		collection.Select((e, idx) => (e, idx));
}