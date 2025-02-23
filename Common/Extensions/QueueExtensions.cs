namespace Common.Extensions
{
	public static class QueueExtensions
	{
		public static void EnqueueAll<T>(this Queue<T> q, IEnumerable<T> add) => add.ForEach(q.Enqueue);

		public static void Add<T>(this Queue<T> q, T item) => q.Enqueue(item);

		public static void Add<T, C>(this PriorityQueue<T, C> q, T item, C cost) => q.Enqueue(item, cost);
	}
}
