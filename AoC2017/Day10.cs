namespace AoC2017;

public class Day10 : Solution
{
	private List<int> lengths;

	public CircularList<int> List { get; set; } = Enumerable.Range(0, 256)
		.Aggregate(new CircularList<int>( ), (list, i) =>
		{
			list.Add(i);
			return list;
		});

	public Day10(string file) : base(file) => lengths = Input.First( )
		.Split(',', StringSplitOptions.TrimEntries)
		.Select(int.Parse).ToList( );

	public Day10(List<string> input) : base(input) { }


	public override async Task<string> SolvePart1()
	{
		List.ResetHead( );
		var skipSize = 0;

		DoKnotHash(ref skipSize);

		List.ResetHead( );
		var e1 = List.Current;
		List.MoveRight( );
		var e2 = List.Current;
		return (e1 * e2).ToString( );
	}


	public override async Task<string> SolvePart2()
	{
		List.ResetHead( );
		var skipSize = 0;

		var bytes = Input.First( ).Select(c => (int)c).ToList( );
		bytes.AddRange(new List<int> { 17, 31, 73, 47, 23 });
		lengths = bytes;

		for (var i = 0 ;i < 64 ;i++)
		{
			DoKnotHash(ref skipSize);
		}

		return List.Chunk(16)
			.Select(c => c.Skip(1).Aggregate(c[0], (s, t) => s ^ t))
			.Aggregate(new StringBuilder( ), (sb, d) => sb.Append(d.ToString("x2")))
			.ToString( );
	}

	private void DoKnotHash(ref int skipSize)
	{
		foreach (var length in lengths)
		{
			var sub = List.TakeAt(length).Reverse( );
			List.ReplaceRange(sub);
			List.MoveRight(length + skipSize);
			skipSize++;
		}
	}
}
