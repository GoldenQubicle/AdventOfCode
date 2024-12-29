namespace Common.Extensions;

public static class HashSetExtensions
{
	public static void AddRange<T>(this HashSet<T> set, IEnumerable<T> range)
	{
		range.ForEach(e => set.Add(e));
	}

	public static void Add<T>(this HashSet<T> set, IEnumerable<T> range)
	{
		range.ForEach(e => set.Add(e));
	}
}