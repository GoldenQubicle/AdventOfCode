namespace AoC2024;

public class Day01 : Solution
{
	private readonly (List<long> left, List<long> right) locations;

	public Day01(string file) : base(file) => locations = Input
		.Select(line => line.Split("   "))
		.Aggregate((left: new List<long>( ), right: new List<long>( )), (t, s) =>
		{
			t.left.Add(long.Parse(s[0]));
			t.right.Add(long.Parse(s[1]));
			return t;
		});


	public override async Task<string> SolvePart1( ) => 
		locations.left
			.Order( )
			.Zip(locations.right.Order( ), (left, right) => Math.Abs(left - right))
			.Sum( ).ToString( );


	public override async Task<string> SolvePart2( )
	{
		var count = locations.right
			.GroupBy(id => id)
			.ToDictionary(g => g.Key, g => g.Count());

		return locations.left
			.Sum(id => count.TryGetValue(id, out var n) ? id * n : 0).ToString();
    }
}
