namespace AoC2024;

public class Day18 : Solution
{
	public static int Width = 71;
	public static int Height = 71;
	public static int Simulate = 1024;

	private readonly Grid2d grid;

	public Day18(string file) : base(file)
	{
		grid = new(Width, Height, diagonalAllowed: false);

		Input.Take(Simulate).Select(line => line.Split(","))
			.Select(p => (int.Parse(p[0]), int.Parse(p[1])))
			.ForEach(p => grid[p].Character = '#');
	}

	public override async Task<string> SolvePart1() => await DoBreadthFirstSearch( )
		.ContinueWith(t => t.Result.Skip(1).Count( ).ToString( ));


	public override async Task<string> SolvePart2()
	{
		var bytes = Input.Select(line => line.Split(","))
			.Select(p => (int.Parse(p[0]), int.Parse(p[1]))).Reverse( ).ToList( );

		bytes.ForEach(b => grid[b].Character = '#');

		foreach (var b in bytes)
		{
			grid[b].Character = '.';
			var path = await DoBreadthFirstSearch( );

			if (path.Count( ) != 0)
				return string.Join(",", b.Item1, b.Item2);
		}
		return string.Empty;
	}

	private async Task<IEnumerable<INode>> DoBreadthFirstSearch() => await PathFinding.BreadthFirstSearch(
		start: grid[0, 0],
		target: grid[Width - 1, Height - 1],
		graph: grid,
		constraint: d => d.neighbor.Character == '.',
		targetCondition: d => d.current.Position == d.target.Position);

}
