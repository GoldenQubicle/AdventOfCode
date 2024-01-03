using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Combinatorics.Collections;
using Common.Extensions;

namespace Common;

public class Grid2d : IEnumerable<Grid2d.Cell>
{
	public int Width { get; init; }
	public int Height { get; init; }
	public int Count => Cells.Count;
	private Dictionary<(int x, int y), Cell> Cells { get; } = new( );

	private List<(int x, int y)> Offsets { get; }

	public Grid2d(bool diagonalAllowed = true)
	{
		List<(int x, int y)> filter = diagonalAllowed
			? new( ) { new(0, 0) }
			: new( ) { new(-1, -1), new(-1, 1), new(1, -1), new(1, 1), new(0, 0) };

		Offsets = new Variations<int>(new[ ] { -1, 0, 1 }, 2, GenerateOption.WithRepetition)
			.Select(v => (v[0], v[1]))
			.Where(p => !filter.Contains(p))
			.ToList( );
	}

	public Grid2d(IReadOnlyList<string> input, bool diagonalAllowed = true) : this(diagonalAllowed)
	{
		Width = input.Max(l => l.Length);
		Height = input.Count;
		DoInitializeCells(input);
	}

	public Grid2d(int width, int height, bool diagonalAllowed = true) : this(diagonalAllowed)
	{
		Width = width;
		Height = height;
		DoInitializeCells();
	}

	private void DoInitializeCells(IReadOnlyList<string> input = default)
	{
		for (var y = 0 ;y < Height ;y++)
		{
			for (var x = 0 ;x < Width ;x++) 
			{
				var gc = input == default || x >= input[y].Length 
					? new Cell((x, y))
					: new Cell((x, y), input[y][x]); //yes really input[y][x], it reads wrong but is right - still dealing with a list here. 

				Cells.Add(gc.Position, gc);
			}
		}
	}


	public Cell this[Cell c] => Cells[c.Position];

	public Cell this[(int x, int y) p] => Cells[p];
	public Cell this[int x, int y] => Cells[(x,y)];

	/// <summary>
	/// Returns the neighbors for the given Position.
	/// Does not wrap around the grid by default (i.e. a corner cell returns 3 neighbors max)
	/// For a connected cell returns 8 neighbors when diagonal is allowed (default), returns 4 neighbors otherwise. 
	/// </summary>
	/// <param name="cell"></param>
	/// <returns></returns>
	public List<Cell> GetNeighbors(Cell cell) => Offsets
		.Select(o => o.Add(cell.Position))
		.Where(IsInBounds)
		.Select(np => Cells[np]).ToList( );

	public List<Cell> GetNeighbors((int x, int y) position) => Offsets
		.Select(o => o.Add(position))
		.Where(IsInBounds)
		.Select(np => Cells[np]).ToList( );

	public List<Cell> GetNeighbors(Cell cell, Func<Cell, bool> query) =>
		GetNeighbors(cell).Where(query).ToList( );

	public List<Cell> GetNeighbors((int x, int y) p, Func<Cell, bool> query) =>
		GetNeighbors(p).Where(query).ToList( );

	public bool IsInBounds((int x, int y) toCheck) => Cells.ContainsKey(toCheck);

	/// <summary>
	/// Gets cells according to specified query.
	/// </summary>
	/// <remarks><b>NOTE:</b> There are NO checks at all, i.e. staying within bounds of the grid is responsibility of the caller!</remarks>
	/// <param name="query"></param>
	/// <returns></returns>
	public List<Cell> GetCells(Func<Cell, bool> query) => Cells.Values.Where(query).ToList( );

	public bool TryGetCell((int x, int y) p, out Cell cell)
	{
		if (Cells.TryGetValue(p, out var c))
		{
			cell = c;
			return true;
		}

		cell = null;
		return false;
	}

	public List<Cell> GetRow(int r) => 
		Cells.Values.Where(p => p.Y == r).ToList();

	public List<Cell> GetColumn(int c) => 
		Cells.Values.Where(p => p.X == c).ToList();

	public List<Cell> GetRange((int x, int y) topLeft, (int x, int y) bottomRight)
	{
		var range = new List<Cell>();
		if (topLeft.y == bottomRight.y)
		{
			for (var x = topLeft.x; x <= bottomRight.x; x++)
			{
				if (Cells.ContainsKey((x, topLeft.y)))
					range.Add(Cells[(x, topLeft.y)]);
			}

			return range;
		}

		if (topLeft.x == bottomRight.x)
		{
			for (var y = topLeft.y ;y <= bottomRight.y ;y++)
			{
				if (Cells.ContainsKey((topLeft.x, y)))
					range.Add(Cells[(topLeft.x, y)]);
			}

			return range;
		}

		for (var x = topLeft.x; x <= bottomRight.x; x++)
		{
			for (var y = topLeft.y; y <= bottomRight.y; y++)
			{
				if(Cells.ContainsKey((x,y)))
					range.Add(Cells[(x,y)]);
			}
		}

		return range;
	}

	public void Add(Cell cell) => Cells.Add(cell.Position, cell);

	public void AddOrUpdate(Cell cell)
	{
		if (!Cells.TryAdd(cell.Position, cell))
			Cells[cell.Position] = cell;
	}

	public IEnumerator<Cell> GetEnumerator() => ((IEnumerable<Cell>)Cells.Values).GetEnumerator( );

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator( );

	public List<Cell> GetShortestPath_V1(Cell start, Cell target, Func<Cell, Cell, bool> constraint, Func<Cell, Cell, bool> targetCondition)
	{
		Cells.Values.ForEach(c => c.Distance = Math.Abs(target.X - c.X) + Math.Abs(target.Y - c.Y));
		var path = new List<Cell>( );
		var visited = new Dictionary<(int x, int y), bool>( );
		var queue = new PriorityQueue<Cell, long>( );

		queue.Enqueue(start, start.GetOverallCost);

		while (queue.Count > 0)
		{
			var current = queue.Dequeue( );
			visited[current.Position] = true;

			if (targetCondition(current, target))
			{
				while (current.Parent is not null)
				{
					path.Add(current.Parent);
					current = current.Parent;
				}
				break;
			}

			GetNeighbors(current, n => !visited.ContainsKey(n.Position) && constraint(current, n))
				.Select(n => n with { Parent = current, Cost = current.Cost + 1 })
				.ForEach(n => queue.Enqueue(n, n.GetOverallCost));

		}

		return path;
	}


	/// <summary>
	/// Simple shortest path solver using a priority queue.
	/// Note it operates on the instanced grid, i.e. multiple calls are not possible and require new grid2d. Kinda sucky, I know. 
	/// </summary>
	/// <param name="start">The Start Cell</param>
	/// <param name="target">The Target Cell</param>
	/// <param name="constraint">Predicate used when getting neighbors for the dequeued cell. Current cell is the 1st argument, neighbor cell the 2nd.</param>
	/// <param name="targetCondition">Predicate used to break out of while loop. Current cell is the 1st argument, target cell the 2nd.</param>
	/// <returns></returns>
	public List<List<Cell>> GetShortestPath(Cell start, Cell target, Func<Cell, Cell, bool> constraint, Func<Cell, Cell, bool> targetCondition)
	{
		//2023 12 7 start rework
		Cells.Values.ForEach(c => c.Distance = Math.Abs(target.X - c.X) + Math.Abs(target.Y - c.Y));
		var paths = new List<List<Cell>>( );
		var visited = new Dictionary<(int x, int y), bool>( );
		var queue = new PriorityQueue<Cell, long>( );

		queue.Enqueue(start, start.GetOverallCost);

		while (queue.Count > 0)
		{
			var current = queue.Dequeue( );
			visited[current.Position] = true;

			if (targetCondition(current, target))
			{
				//we've reached the target, now backtrack how we came here
				var path = new List<Cell>( );
				while (current.Parent is not null)
				{
					path.Add(current.Parent);
					current = current.Parent;
				}
				//current has been reset to start now, so clear queue & visited
				//however, do add ALL the paths we just found to the visited list -> yeah no, doesn't work like that
				queue.Clear();
				visited.Clear();
				paths.Add(path);

				paths.ForEach(p => p.ForEach(c => visited.TryAdd(c.Position, true)));

				GetNeighbors(current, n => !visited.ContainsKey(n.Position) && constraint(current, n))
					.Select(n => n with { Parent = current, Cost = current.Cost + 1 })
					.ForEach(n => queue.Enqueue(n, n.GetOverallCost));
				continue;

			}

			GetNeighbors(current, n => !visited.ContainsKey(n.Position) && constraint(current, n))
				.Select(n => n with { Parent = current, Cost = current.Cost + 1 })
				.ForEach(n => queue.Enqueue(n, n.GetOverallCost));

		}

		return paths;
	}


	public record Cell((int, int) Position)
	{
		public (int x, int y) Position { get; set; } = Position;
		public char Character { get; set; } = ' ';
		public long Value { get; set; }
		public Cell Parent { get; init; }
		public long Cost { get; set; }
		public long Distance { get; set; }
		public long GetOverallCost => Cost + Distance;
		public int X => Position.x;
		public int Y => Position.y;

		public Cell((int, int) Position, char character) : this(Position)
		{
			Character = character;
			Value = char.IsDigit(Character) ? Character.ToLong( ) : 0;
		}

		/// <summary>
		/// returns a new Cell with same Position but new character
		/// </summary>
		/// <param name="newChar"></param>
		/// <returns></returns>
		public Cell ChangeCharacter(char newChar) => new(Position, newChar);

	}

	public override string ToString()
	{
		var minx = Cells.Values.Min(c => c.X);
		var maxx = Cells.Values.Max(c => c.X);
		var miny = Cells.Values.Min(c => c.Y);
		var maxy = Cells.Values.Max(c => c.Y);

		var sb = new StringBuilder( );

		for (var y = miny ;y <= maxy ;y++)
		{
			for (var x = minx ;x <= maxx ;x++)
			{
				var p = (x, y);
				sb.Append(Cells[p].Character);
			}
			sb.Append("\n");
		}
		return sb.ToString( );
	}

	/// <summary>
	/// Helper method to fill out the grid in case the input is sparse. 
	/// </summary>
	/// <param name="blank">The character to use for empty cells</param>
	public void Fill(char blank)
	{
		var minx = Cells.Values.Min(c => c.X);
		var maxx = Cells.Values.Max(c => c.X);
		var miny = 0;//Cells.Values.Min(c => c.Y);//2022 day 14 hack
		var maxy = Cells.Values.Max(c => c.Y);

		for (var y = miny ;y <= maxy ;y++)
		{
			for (var x = minx ;x <= maxx ;x++)
			{
				var c = new Cell(new(x, y), blank);
				Cells.TryAdd(c.Position, c);
			}
		}
	}
}