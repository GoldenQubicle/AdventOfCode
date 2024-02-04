using System;
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

	public T this[int i] => this.ToArray( )[i];


	/// <summary>
	/// Set the current node n positions to the left.
	/// </summary>
	/// <param name="n">defaults to 1 position</param>
	public void MoveLeft(int n = 1)
	{
		if (n < 0)
			throw new ArgumentException($"Cannot move {n} steps to the left, use MoveRight instead");
		for (var i = 0 ;i < n ;i++)
			current = current.Previous ?? Last;

	}


	/// <summary>
	/// Set the current node n positions to the right.
	/// </summary>
	/// <param name="n">Defaults to 1 position</param>
	public void MoveRight(int n = 1)
	{
		if (n < 0)
			throw new ArgumentException($"Cannot move {n} steps to the right, use MoveLeft instead");
		for (var i = 0 ;i < n ;i++)
			current = current.Next ?? First;
	}


	/// <summary>
	/// Starting at, and including the current node, takes the next amount of elements. 
	/// <br>When end of list is reached, it will wrap around to the start.</br>
	/// <br>The current node position moves by default to the next node after take amount.</br>
	/// </summary> 
	/// <remarks>Note: if the amount to take is larger than list size, the returned collection will contain duplicate elements!</remarks>
	/// <param name="amount"></param>
	/// <returns></returns>
	public IEnumerable<T> TakeAt(int amount, bool moveCurrent = true)
	{
		var result = new List<T>( );
		for (var i = 0 ;i < amount ;i++)
		{
			result.Add(Current);
			MoveRight( );
		}
	
		if(!moveCurrent)
			MoveLeft(amount);

		return result;
	}


	/// <summary>
	/// Replaces the values starting from the current node for the values in the range. The current node position remains the same.  
	/// </summary>
	/// <param name="range"></param>
	public void ReplaceRange(IEnumerable<T> range)
	{
		var l = range.ToList( );
		foreach (var v in l)
		{
			ReplaceCurrent(v);
			MoveRight( );
		}
		MoveLeft(l.Count);
	}

	/// <summary>
	/// Replaces the value of the current node with the value given. 
	/// </summary>
	/// <param name="value"></param>
	public void ReplaceCurrent(T value)
	{
		current.Value = value;
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
		if (remove is null)
			return false;

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


	/// <summary>
	/// Sets the current node to the node specified by the index.
	/// </summary>
	/// <param name="idx"></param>
	public void SetHeadByIndex(int idx) => current = ToNodeArray( )[idx];


	public int GetIndex() => ToNodeArray( ).ToList( ).IndexOf(current);

	private LinkedListNode<T>[] nodeArray;

	private LinkedListNode<T>[ ] ToNodeArray()
	{
		if (Count == 0)
			return Array.Empty<LinkedListNode<T>>( );

		if (nodeArray is not null && nodeArray.Length == Count)
			return nodeArray;

		nodeArray = new LinkedListNode<T>[Count];
		var node = First;
		var index = 0;

		do
		{
			nodeArray[index++] = node;
			node = node.Next;
		} while (node != null);


		return nodeArray;
	}

	public override string ToString() => this.Aggregate(new StringBuilder( ), (sb, value) =>
		sb.Append(value).Append(',')).ToString( );


}