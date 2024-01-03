using Common;

namespace AoC2023;

public class Day10 : Solution
{
	private readonly Grid2d grid;

	public Day10(string file) : base(file) =>
		grid = new Grid2d(Input, diagonalAllowed: false);


	public override string SolvePart1()
	{
		var path = GetPath( );
		return (path.Count % 2 == 0 ? path.Count / 2 : (path.Count / 2) - 1).ToString( );
	}


	public override string SolvePart2()
	{
		var path = GetPath( );
		return grid
			.Where(c => !path.Contains(c))
			.Count(c => Maths.IsPointInsidePolygon(path.Select(p => p.Position), c.Position)).ToString( );
	}


	private List<Grid2d.Cell> GetPath()
	{
		var current = grid.First(c => c.Character == 'S');
		current.Character = 'F';
		var visited = new List<Grid2d.Cell>( );

		while (current is not null)
		{
			visited.Add(current);

			current = grid.GetNeighbors(current, n => !visited.Contains(n) && IsConnected(current, (Grid2d.Cell)n)).FirstOrDefault( ) as Grid2d.Cell;
		}

		return visited;
	}


	private static bool IsConnected(Grid2d.Cell current, Grid2d.Cell neighbor) =>
		(current.X - neighbor.X, current.Y - neighbor.Y, current.Character, neighbor.Character) switch
		{
			//ground not connected by default
			(_, _, _, '.') => false,
			//current pipes which cannot have anything to the left, right, up or down
			(1, 0, '|' or 'L' or 'F', _) => false,
			(-1, 0, '|' or 'J' or '7', _) => false,
			(0, 1, '-' or '7' or 'F', _) => false,
			(0, -1, '-' or 'L' or 'J', _) => false,
			//current pipes taking into account neighbors which cannot connect
			(1, 0, '-' or '7' or 'J', '|' or '7' or 'J') => false,
			(-1, 0, '-' or 'L' or 'F', '|' or 'L' or 'F') => false,
			(0, 1, '|' or 'L' or 'J', '-' or 'L' or 'J') => false,
			(0, -1, '|' or '7' or 'F', '-' or '7' or 'F') => false,
			//whatever remains can connect
			_ => true
		};
}
