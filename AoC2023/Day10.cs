using System.Drawing;
using System.Drawing.Drawing2D;
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
		var poly = path.Select(p => new Point(p.X, p.Y)).ToArray( );

		return grid
			.Where(c => !path.Contains(c))
			.Count(c => IsPointInsidePolygon(poly, new Point(c.X, c.Y))).ToString( );
	}

	private List<Grid2d.Cell> GetPath()
	{
		var start = grid.First(c => c.Character == 'S');
		var visited = new List<Grid2d.Cell>( );
		var queue = new Queue<Grid2d.Cell>( );
		queue.Enqueue(start);

		while (queue.Count > 0)
		{
			var current = queue.Dequeue( );
			visited.Add(current);
			
			var neighbor = grid.GetNeighbors(current, n => !visited.Contains(n) && IsConnected(current, n)).FirstOrDefault( );
			
			if (neighbor != default)
				queue.Enqueue(neighbor);
		}

		return visited;
	}


	private static bool IsConnected(Grid2d.Cell current, Grid2d.Cell neighbor) =>
		(current.X - neighbor.X, current.Y - neighbor.Y, current.Character, neighbor.Character) switch
		{
			(_, _, _, '.') => false,
			(1, 0, '|', _) or (-1, 0, '|', _) => false,
			(0, 1, '-', _) or (0, -1, '-', _) => false,
			(1, 0, 'L', _) or (0, -1, 'L', _) => false,
			(-1, 0, 'J', _) or (0, -1, 'J', _) => false,
			(-1, 0, '7', _) or (0, 1, '7', _) => false,
			(1, 0, 'F', _) or (0, 1, 'F', _) => false,
			(0, 1, ('|' or 'L' or 'J'), ('-' or 'L' or 'J')) or (0, -1, ('|' or '7' or 'F'), ('-' or '7' or 'F')) => false,
			(1, 0, ('-' or '7' or 'J'), ('|' or '7' or 'J')) or (-1, 0, ('-' or 'L' or 'F'), ('|' or 'L' or 'F')) => false,
			_ => true
		};


	//shamelessly copied from https://stackoverflow.com/a/67403631
	private static bool IsPointInsidePolygon(Point[ ] polygon, Point point)
	{
		var path = new GraphicsPath( );
		path.AddPolygon(polygon);

		var region = new Region(path);
		return region.IsVisible(point);
	}
}
