namespace AoC2021;

public class Day25 : Solution
{
	private Grid2d grid;

	public Day25(string file) : base(file) => grid = new Grid2d(Input, diagonalAllowed: false);

	private const char East = '>';
	private const char South = 'v';

	public override async Task<string> SolvePart1()
	{
		//Console.WriteLine(grid);

		var steps = 0;
		while (true)
		{
			var movedEast = TryMove(East);
			var movedSouth = TryMove(South);
			steps++;

			if (!movedEast && !movedSouth)
				break;
		}
	
	
		return steps.ToString();
	}

	private bool TryMove(char direction)
	{
		var move = grid.Where(c => c.Character == direction && CanMove(c, direction)).ToList( );
		move.ForEach(c =>
		{
			grid[c.Position].Character = '.';
			grid[GetNextPos(c, direction)].Character = direction;

		});
		
		return move.Count > 0;
	}

	private bool CanMove(Grid2d.Cell cell, char direction)
	{
		var pos = GetNextPos(cell, direction);

		return grid[pos].Character == '.';

	}

	private (int, int) GetNextPos(Grid2d.Cell cell, char direction)
	{
		var pos = direction == East ? cell.Position.Add(1, 0) : cell.Position.Add(0, 1);

		if (!grid.IsInBounds(pos))
			pos = direction == East ? (0, cell.Y) : (cell.X, 0);

		return pos;
	}

	public override async Task<string> SolvePart2()
	{
		return string.Empty;
	}
}
