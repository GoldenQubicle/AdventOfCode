namespace AoC2024;

public class Day15 : Solution
{
	private Grid2d grid;
	private readonly string instructions;

	public Day15(string file) : base(file, doRemoveEmptyLines: false)
	{
		grid = new Grid2d(Input.TakeWhile(line => !string.IsNullOrEmpty(line)).ToList( ), diagonalAllowed: false);
		instructions = Input.SkipWhile(line => line.StartsWith('#') || string.IsNullOrEmpty(line))
			.Aggregate(new StringBuilder( ), (sb, line) => sb.Append(line)).ToString( );
	}


	public override async Task<string> SolvePart1()
	{
		var robot = grid.First(c => c.Character is '@').Position;

		foreach (var dir in instructions)
		{
			var move = GetDirection(dir);

			if (!CanMovePart1(robot, move, out var boxes))
				continue;

			boxes.ForEach(b => grid[b.Add(move)].Character = 'O');

			MoveRobot(ref robot, move);
		}

		return grid.Where(c => c.Character is 'O').Sum(c => 100 * c.Y + c.X).ToString( );
	}


	public override async Task<string> SolvePart2()
	{
		grid = ToWideGrid( );

		var robot = grid.First(c => c.Character is '@').Position;

		foreach (var dir in instructions)
		{
			if (!CanMovePart2(robot, dir, out var boxes))
				continue;

			var move = GetDirection(dir);

			if (dir is '<' or '>')
				boxes.ForEach(b => grid[b.p.Add(move)].Character = b.c);

			if (dir is '^' or 'v')
				boxes.ForEach(b =>
				{
					grid[b.p.Add(move)].Character = b.c;
					grid[b.p].Character = '.';
				});

			MoveRobot(ref robot, move);
		}
		return grid.Where(c => c.Character is '[').Sum(c => 100 * c.Y + c.X).ToString( );
	}


	private bool CanMovePart2((int x, int y) pos, char dir, out List<((int x, int y) p, char c)> boxes)
	{
		boxes = new( );
		var move = GetDirection(dir);
		pos = pos.Add(move);

		if (dir is '<' or '>')
		{
			while (grid[pos].Character is '[' or ']')
			{
				boxes.Add((pos, grid[pos].Character));
				pos = pos.Add(move);
			}

			return grid[pos].Character is '.';
		}

		if (!IsBox(pos))
			return grid[pos].Character is '.';

		//The problem is when moving up or down, the boxes can be a weird shape and get stuck not directly above or below the robot.
		//Go over all boxes until either we hit a wall, or all boxes have available space in the direction we're going.

		var queue = new Queue<(int, int)> { pos };

		while (queue.TryDequeue(out var current))
		{
			if (grid[current].Character is '#')
				return false;

			if (!IsBox(current))
				continue;

			var neighbor = grid[current].Character is '['
				? current.Add(1, 0)
				: current.Add(-1, 0);

			queue.Enqueue(current.Add(move));
			queue.Enqueue(neighbor.Add(move));

			boxes.Add((current, grid[current].Character));
			boxes.Add((neighbor, grid[neighbor].Character));
		}

		boxes.Reverse( ); // reverse in order to update cells properly

		return true;
	}


	private bool CanMovePart1((int x, int y) pos, (int x, int y) move, out List<(int x, int y)> boxes)
	{
		boxes = new( );
		pos = pos.Add(move);

		while (grid[pos].Character is 'O')
		{
			boxes.Add(pos);
			pos = pos.Add(move);
		}

		return grid[pos].Character is '.';
	}


	private bool IsBox((int x, int y) pos) =>
		grid[pos].Character is '[' or ']';


	private void MoveRobot(ref (int x, int y) robot, (int x, int y) moveTo)
	{
		grid[robot].Character = '.';
		robot = robot.Add(moveTo);
		grid[robot].Character = '@';
	}


	private static (int x, int y) GetDirection(char direction) => direction switch
	{
		'>' => (1, 0),
		'<' => (-1, 0),
		'^' => (0, -1),
		'v' => (0, 1),
		_ => throw new ArgumentOutOfRangeException( )
	};


	private Grid2d ToWideGrid()
	{
		var wideGrid = new Grid2d(grid.Width * 2, grid.Height);

		for (var c = 0 ;c < grid.Width ;c++)
		{
			foreach (var cell in grid.GetColumn(c))
			{
				var x1 = c * 2;
				var x2 = x1 + 1;

				switch (cell.Character)
				{
					case '#':
						wideGrid[x1, cell.Y].Character = '#';
						wideGrid[x2, cell.Y].Character = '#';
						continue;
					case 'O':
						wideGrid[x1, cell.Y].Character = '[';
						wideGrid[x2, cell.Y].Character = ']';
						continue;
					case '.':
						wideGrid[x1, cell.Y].Character = '.';
						wideGrid[x2, cell.Y].Character = '.';
						continue;
					case '@':
						wideGrid[x1, cell.Y].Character = '@';
						wideGrid[x2, cell.Y].Character = '.';
						continue;
				}
			};
		}
		return wideGrid;
	}
}
