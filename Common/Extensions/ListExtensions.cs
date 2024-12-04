namespace Common.Extensions
{
	public static class ListExtensions
	{
		public static bool IsEmpty<T>(this List<T> list) => list.Count == 0;

		public static void RemoveAll<T>(this List<T> list, List<T> toBeRemoved)
		{
			foreach (var item in toBeRemoved)
			{
				list.Remove(item);
			}
		}

		public static T Random<T>(this List<T> list) =>
				 list[new Random( ).Next(list.Count)];


		public static List<T> AsNew<T>(this List<T> list) => [.. list];


		/// <summary>
		/// Add an item to the list and return the expanded result as a new List&lt;<typeparamref name="T"/>&gt;
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list">the list to be expanded</param>
		/// <param name="item">the item to be added</param>
		/// <returns>A new list</returns>
		public static List<T> Expand<T>(this List<T> list, T item) => [.. list, item];


		/// <summary>
		/// Replace an item at the specified index and return the result as a new List&lt;<typeparamref name="T"/>&gt;
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <param name="idx"></param>
		/// <param name="item"></param>
		/// <returns></returns>
		public static List<T> ReplaceAt<T>(this List<T> list, int idx, T item)
		{
			var result = new List<T>(list);
			result.RemoveAt(idx);
			result.Insert(idx, item);
			return result;
		}

		/// <summary>
		/// Removes an item at specified index and return the result as a new List&lt;<typeparamref name="T"/>&gt;
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <param name="idx"></param>
		/// <returns></returns>
		public static List<T> RemoveIdx<T>(this List<T> list, int idx)
		{
			var result = new List<T>(list);
			result.RemoveAt(idx);
			return result;
		}

		/// <summary>
		/// Insert an item at the specified index and return the result as a new List&lt;<typeparamref name="T"/>&gt;
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <param name="idx"></param>
		/// <param name="item"></param>
		/// <returns></returns>
		public static List<T> InsertAt<T>(this List<T> list, int idx, T item)
		{
			var result = new List<T>(list);
			result.Insert(idx, item);
			return result;
		}
	}
}
