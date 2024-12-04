namespace Common;

public class Grid2d : IEnumerable<Grid2d.Cell>, IGraph
{
	/// <summary>
	/// When true, will automatically pad the grid with an empty, 2 wide cell padding using '.' as the character.
	/// In addition GetNeighbors will automatically expand the grid with new cells in case neighbors do not exist. 
	/// </summary>
	public bool IsInfinite { get; }

	public char EmptyCharacter { get; set; } = '.'; 
	public int Width { get; init; } //note: does not reflect any changes made after initialization
	public int Height { get; init; } //note: does not reflect any changes made after initialization
	public int Count => Cells.Count;
	private Dictionary<(int x, int y), Cell> Cells { get; } = new( );

	public List<(int x, int y)> Offsets { get; }

	public Grid2d(bool diagonalAllowed = true, bool isInfinite = false)
	{
		IsInfinite = isInfinite;

		List<(int x, int y)> filter = diagonalAllowed
			? new( ) { new(0, 0) }
			: new( ) { new(-1, -1), new(-1, 1), new(1, -1), new(1, 1), new(0, 0) };

		Offsets = new Variations<int>(new[ ] { -1, 0, 1 }, 2, GenerateOption.WithRepetition)
			.Select(v => (v[0], v[1]))
			.Where(p => !filter.Contains(p))
			.ToList( );
	}

	public Grid2d(IReadOnlyList<string> input, bool diagonalAllowed = true, bool isInfinite = false) : this(diagonalAllowed, isInfinite)
	{
		Width = input.Max(l => l.Length);
		Height = input.Count;
		DoInitializeCells(input);
	}

	public Grid2d(int width, int height, bool diagonalAllowed = true, bool isInfinite = false) : this(diagonalAllowed, isInfinite)
	{
		Width = width;
		Height = height;
		DoInitializeCells( );
	}



	private void DoInitializeCells(IReadOnlyList<string> input = default)
	{
		for (var y = 0; y < Height; y++)
		{
			for (var x = 0; x < Width; x++)
			{
				var gc = input == default || x >= input[y].Length
					? new Cell((x, y))
					: new Cell((x, y), input[y][x]); //yes really input[y][x], it reads wrong but is right - still dealing with a list here. 

				Cells.Add(gc.Position, gc);
			}
		}
	}

	/// <summary>
	/// Get the bounds of the grid as min & max x & y values. 
	/// </summary>
	/// <returns></returns>
	public (int minx, int maxx, int miny, int maxy) GetBounds() =>
		(Cells.Values.Min(c => c.X), Cells.Values.Max(c => c.X), Cells.Values.Min(c => c.Y), Cells.Values.Max(c => c.Y));


	/// <summary>
	/// Used when having sparse input. Calculates the minimum and maximum x & y bounds, and fills out the grid accordingly. 
	/// </summary>
	/// <param name="n">The margin with which to pad, defaults to 0.</param>
	/// <param name="bounds">The bounds for the padded grid, defaults to min & max for x & y.</param>
	/// <param name="blank">The character with which to pad, defaults to '.'</param>
	/// <returns></returns>
	public Grid2d Pad(int n = 0, char blank = '.', (int minx, int maxx, int miny, int maxy) bounds = default)
	{
		if (bounds == default)
			bounds = GetBounds();

		var (minx, maxx, miny, maxy) = bounds;


		for (var y = miny - n ;y <= maxy + n ;y++)
		{
			for (var x = minx - n ;x <= maxx + n ;x++)
			{
				Cells.TryAdd((x, y), new(new(x, y), blank));
			}
		}

		return this;
	}


	public Cell this[int idx] => this.ToArray( )[idx];
	public Cell this[(int x, int y) p] => Cells[p];
	public INode this[int x, int y] => Cells[(x, y)];


	/// <summary>
	/// Returns the neighbors for the given Position.
	/// Does not wrap around the grid by default (i.e. a corner cell returns 3 neighbors max)
	/// For a connected cell returns 8 neighbors when diagonal is allowed (default), returns 4 neighbors otherwise. 
	/// </summary>
	/// <param name="cell"></param>
	/// <returns></returns>
	public List<Cell> GetNeighbors(Cell cell) => Offsets
		.Select(o => o.Add(cell.Position))
		.Where(c => IsInfinite || IsInBounds(c))
		.Select(np =>
		{
			if (IsInfinite && !IsInBounds(np))
				return new(np, EmptyCharacter);

			return Cells[np];
		}).ToList( );

	public IEnumerable<INode> GetNeighbors(INode node, Func<INode, bool> query) =>
		GetNeighbors(node as Cell).Where(query).ToList( );


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
		Cells.Values.Where(p => p.Y == r).ToList( );

	public List<Cell> GetColumn(int c) =>
		Cells.Values.Where(p => p.X == c).ToList( );

	public List<Cell> GetRange((int x, int y) topLeft, (int x, int y) bottomRight)
	{
		var range = new List<Cell>( );
		if (topLeft.y == bottomRight.y)
		{
			for (var x = topLeft.x ;x <= bottomRight.x ;x++)
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

		for (var x = topLeft.x ;x <= bottomRight.x ;x++)
		{
			for (var y = topLeft.y ;y <= bottomRight.y ;y++)
			{
				if (Cells.ContainsKey((x, y)))
					range.Add(Cells[(x, y)]);
			}
		}

		return range;
	}

	public void Add(Cell cell) => Cells.TryAdd(cell.Position, cell);

	public void AddOrUpdate(Cell cell)
	{
		if (!Cells.TryAdd(cell.Position, cell))
			Cells[cell.Position] = cell;
	}

	public IEnumerator<Cell> GetEnumerator() => ((IEnumerable<Cell>)Cells.Values).GetEnumerator( );

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator( );


	public record Cell((int, int) Position) : INode
	{
		public (int x, int y) Position { get; set; } = Position;
		public char Character { get; set; } = '.';
		public long Value { get; set; }
		public long Cost { get; set; }
		public int X => Position.x;
		public int Y => Position.y;

		public Cell((int, int) Position, char character) : this(Position)
		{
			Character = character;
			Value = char.IsDigit(Character) ? Character.ToLong( ) : 0;
		}
	}

	public override string ToString()
	{
		var (minx, maxx, miny, maxy) = GetBounds();

		var sb = new StringBuilder( );
		sb.Append('\n');
		for (var y = miny ;y <= maxy ;y++)
		//for (var y = maxy ;y >= miny ;y--) //hacky solution proper visualization 2022 day 17
		{
			for (var x = minx ;x <= maxx ;x++)
			{
				var p = (x, y);
				if (Cells.TryGetValue(p, out var c))
					sb.Append(c.Character);
				else
					sb.Append(string.Empty);
			}
			sb.Append('\n');
		}
		return sb.ToString( );
	}
}