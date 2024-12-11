namespace AoC2024;

public class Day11 : Solution
{
	private List<string> stones;
	public Day11(string file) : base(file) => stones = Input[0].Split(" ").ToList( );

	public override async Task<string> SolvePart1() => stones.Sum(s => ApplyRules(s, 25, new( ))).ToString();
	
	public override async Task<string> SolvePart2() => stones.Sum(s => ApplyRules(s, 75, new( ))).ToString( );
	

	private long ApplyRules(string stone, int blink, Dictionary<(string, int), long> cache)
	{
		if (blink == 0)
			return 1;

		var key = (stone, blink);

		if (cache.TryGetValue(key, out var result))
			return result;

		if (stone == "0")
		{
			cache[key] = ApplyRules("1", blink - 1, cache);
		} 
		else if (stone.Length % 2 == 0)
		{
			var first = stone[..(stone.Length / 2)];
			var second = stone[(stone.Length / 2)..];

			while (second.StartsWith("0") && second.Length > 1)
				second = second[1..];

			cache[key] = ApplyRules(first, blink - 1, cache) + ApplyRules(second, blink - 1, cache);
		}
		else
		{
			cache[key] = ApplyRules((stone.ToLong( ) * 2024L).ToString( ), blink - 1, cache);
		}

		return cache[key];
	}
}
