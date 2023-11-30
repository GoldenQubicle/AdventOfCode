using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Common.Extensions;


namespace Common;

public class CircularList<T> : LinkedList<T>
{
	private LinkedListNode<T> current;


	/// <summary>
	/// Get the value at the current pointer position.
	/// </summary>
	public T Current => current.Value;


	/// <summary>
	/// Moves the pointer n positions to the left.
	/// </summary>
	/// <param name="n">defaults to 1 position</param>
	public void MoveLeft(int n = 1)
	{
		for (var i = 0 ;i < n  ;i++)
		{
			current = current.Previous ?? Last;
		}
	}


	/// <summary>
	/// Moves the pointer n positions to the right.
	/// </summary>
	/// <param name="n">Defaults to 1 position</param>
	public void MoveRight(int n = 1)
	{
		for (var i = 0 ;i < n  ;i++)
		{
			current = current.Next ?? First;
		}
	}



	/// <summary>
	/// Inserts an item <b>after</b> by default, and sets the current node.
	/// </summary>
	/// <param name="value">The value to be inserted</param>
	/// <param name="after"></param>
	public void Insert(T value, bool after = true)
	{
		current = after
			? AddAfter(current, value)
			: AddBefore(current, value);
	}


	/// <summary>
	/// Adds an item at the end of the list, and sets the current node.
	/// </summary>
	/// <param name="value"></param>
	public void Add(T value)
	{
		current = AddLast(value);
	}

	/// <summary>
	/// Removes an item from the list, and sets the current node to the next of the one that was removed. If there was no next node, set the current node to the previous. 
	/// </summary>
	/// <param name="value"></param>
	public void Remove(T value)
	{
		var remove = Find(value);
		current = remove.Next ?? remove.Previous;
		Remove(remove);
	}

	/// <summary>
	/// Sets the current node to the first node by default. 
	/// </summary>
	public void ResetHead(bool toFirst = true) => current = toFirst ? First : Last;


	public override string ToString() => this.Aggregate(new StringBuilder( ), (sb, value) =>
		sb.Append(value).Append(',')).ToString( );

}