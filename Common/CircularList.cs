using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Common;

/// <summary>
/// Convenience wrapper class around a <see cref="LinkedList{T}"/>
/// </summary>
/// <typeparam name="T"></typeparam>
public class CircularList<T> : LinkedList<T>
{
	private LinkedListNode<T> current;


	/// <summary>
	/// Get the value at the current node. 
	/// </summary>
	public T Current => current.Value;


	/// <summary>
	/// Set the current node n positions to the left.
	/// </summary>
	/// <param name="n">defaults to 1 position</param>
	public void MoveLeft(int n = 1)
	{
		for (var i = 0 ;i < n ;i++)
		{
			current = current.Previous ?? Last;
		}
	}


	/// <summary>
	/// Set the current node n positions to the right.
	/// </summary>
	/// <param name="n">Defaults to 1 position</param>
	public void MoveRight(int n = 1)
	{
		for (var i = 0 ;i < n ;i++)
		{
			current = current.Next ?? First;
		}
	}


	/// <summary>
	/// Inserts a node <b>after</b> the current node by default, and sets the current node.
	/// </summary>
	/// <param name="value">The value to be inserted</param>
	/// <param name="after"></param>
	public void Insert(T value, bool after = true) => current = after
		? AddAfter(current, value)
		: AddBefore(current, value);


	/// <summary>
	/// Adds a node to the end of the list, and sets the current node.
	/// </summary>
	/// <param name="value"></param>
	public void Add(T value) => current = AddLast(value);


	/// <summary>
	/// Tries to remove a node with the given value from the list, and sets the current node to the next of the one that was removed. If there was no next node, set the current node to the previous. 
	/// </summary>
	/// <remarks><b>Note:</b> this method will be slow with big lists as it finds by value.</remarks>
	/// <param name="value"></param>
	public bool TryRemove(T value)
	{
		var remove = Find(value);
		if (remove is null) return false;

		current = remove.Next ?? remove.Previous;
		Remove(remove);
		return true;
	}


	/// <summary>
	/// Removes the current node and sets the current node to the next of the one that was removed. If there was no next node, set the current node to the previous. 
	/// </summary>
	public void RemoveCurrent()
	{
		var remove = current;
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