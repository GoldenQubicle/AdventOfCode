using System.Collections.Generic;

namespace Common.Extensions;

public static class StackExtensions
{
	public static void Add<T>(this Stack<T> stack, T item) => stack.Push(item);

}