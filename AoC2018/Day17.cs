namespace AoC2018;

public class Day17 : Solution
{
	private readonly Grid2d grid;

	public Day17(string file) : base(file, split: "\n") => grid = Input
			.Select(l => Regex.Match(l, @"(x|y)=(\d+),\s(x|y)=(\d+)..(\d+)"))
			.Aggregate(new Grid2d(diagonalAllowed: false), (grid, m) =>
			{
				for (var i = m.AsInt(4) ;i <= m.AsInt(5) ;i++)
				{
					var pos = m.AsString(1).Equals("x") ? (m.AsInt(2), i) : (i, m.AsInt(2));
					grid.Add(new(pos, '#'));
				}
				return grid;
			}).Pad(0);

	public override async Task<string> SolvePart1()
	{
		Console.WriteLine(grid);
		return string.Empty;
	}

	public override async Task<string> SolvePart2()
	{
		return string.Empty;
	}


}
