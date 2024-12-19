namespace AoC2024;

public class Day19 : Solution
{
	private readonly List<string> patterns;
	private readonly List<string> designs;

	public Day19(string file) : base(file)
	{
		patterns = Input[0].Split(",", StringSplitOptions.TrimEntries).ToList( );
		designs = Input.Skip(1).ToList( );
	}

	public override async Task<string> SolvePart1() => MatchDesigns( ).Count(r => r > 0).ToString( );

	public override async Task<string> SolvePart2() => MatchDesigns( ).Sum( ).ToString( );

	private List<long> MatchDesigns() => designs
		.Select(design => MatchPattern(design, new Dictionary<string, long>( ))).ToList( );


	private long MatchPattern(string design, Dictionary<string, long> cache)
	{
		if (string.IsNullOrEmpty(design))
			return 1;

		if (cache.TryGetValue(design, out var count))
			return count;

		var matches = patterns.Where(design.EndsWith).ToList( );

		if (matches.Count == 0)
			return 0;

		var sum = matches.Sum(m => MatchPattern(design[..^m.Length], cache));
		cache.Add(design, sum);
		return sum;
	}
}
