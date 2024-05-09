namespace AoC2022;

public class Day18 : Solution
{
	private readonly List<Vector3> grid;

	private readonly List<Vector3> offsets = new( )
	{
		new (1, 0, 0),  new (-1, 0, 0),
		new (0, 1, 0),  new (0, -1, 0),
		new (0, 0, 1),  new (0, 0, -1),
	};


	public Day18(string file) : base(file, split: "\n") =>
		grid = Input.Select(l => Regex.Match(l, @"(?<x>\d+),(?<y>\d+),(?<z>\d+)").AsVector3( )).ToList( );


	public override async Task<string> SolvePart1() => grid
		.Aggregate(0, (totalSides, cube) => totalSides + offsets
			.Aggregate(6, (sides, offset) => grid.Contains(cube + offset) ? sides - 1 : sides)).ToString( );


	public override async Task<string> SolvePart2()
	{
		return string.Empty;
	}
}
