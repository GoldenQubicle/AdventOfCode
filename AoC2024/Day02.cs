namespace AoC2024;

public class Day02 : Solution
{
	private readonly List<List<int>> reports;

	public Day02(string file) : base(file) => reports = Input
		.Select(line => line.Split(" ", StringSplitOptions.TrimEntries)
			.Select(int.Parse).ToList( ))
		.ToList( );


	public override async Task<string> SolvePart1() =>
		reports.Count(r => IsSafe(r)).ToString( );

	public override async Task<string> SolvePart2() =>
		reports.Count(r => IsSafe(r, isPart1: false)).ToString( );

	private static bool IsSafe(List<int> report, bool isPart1 = true)
	{
		var delta = GetDelta(report);
		var isSafe = delta.All(i => int.Sign(i) == int.Sign(delta[0]) && Math.Abs(i) >= 1 && Math.Abs(i) <= 3);

		if (isPart1 || isSafe)
			return isSafe;

		var idx = 0;

		while (idx++ < report.Count - 1 && !isSafe)
			isSafe = IsSafe(report.RemoveIdx(idx));

		return isSafe;
	}

	private static List<int> GetDelta(List<int> report) => report
		.WithIndex( )
		.Skip(1)
		.Select(l => l.Value - report[l.idx - 1])
		.ToList( );

}
