namespace Common.Extensions;

public static class StackExtensions
{
	public static void Add<T>(this Stack<T> stack, T item) => stack.Push(item);

	public static void PushAll<T>(this Stack<T> stack, IEnumerable<T> items) =>
		items.ForEach(stack.Push);

}