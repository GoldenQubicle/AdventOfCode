namespace AoC2023;

public class Day02 : Solution
{
	private readonly Dictionary<int, List<(int r, int g, int b)>> games;

	public Day02(string file) : base(file) => games = Input.WithIndex()
		.Select(l => (l.idx, l.Value.Split(':')[1]))
		.ToDictionary(t => t.idx + 1, t => t.Item2.Split(';')
			.Select(s => Regex.Matches(s, @"(?<r>\d+.(?=red))|(?<g>\d+.(?=green))|(?<b>\d+.(?=blue))"))
			.Select(m => (r: m.GetGroupAsInt("r"), g: m.GetGroupAsInt("g"), b: m.GetGroupAsInt("b"))).ToList( ));


	public override async Task<string> SolvePart1() => games
		.Where(g => !g.Value.Any(s => s.r > 12 || s.g > 13 || s.b > 14))
		.Sum(g => g.Key).ToString( );


	public override async Task<string> SolvePart2() => games.Values
		.Select(sets => (r: sets.Max(s => s.r), g: sets.Max(s => s.g), b: sets.Max(s => s.b)))
		.Select(s => s.r * s.g * s.b)
		.Sum( ).ToString( );
}