using Common.Interfaces;

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

		await DoKnotHash(skipSize, 0);

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
			skipSize = await DoKnotHash(skipSize, i);
		}

		return List.Chunk(16)
			.Select(c => c.Skip(1).Aggregate(c[0], (s, t) => s ^ t))
			.Aggregate(new StringBuilder( ), (sb, d) => sb.Append(d.ToString("x2")))
			.ToString( );
	}

	private async Task<int> DoKnotHash(int skipSize, int cycle)
	{
		foreach (var length in lengths)
		{
			var range = List.TakeAt(length, moveCurrent: false).Reverse( ).ToList();
			
			if (IRenderState.IsActive)
			{
				await IRenderState.Update(new KnotHashRender
				{
					Cycle = cycle,
					Operation = lengths.IndexOf(length),
					Range = range,
					Jump = length + skipSize
				});
			}

			List.ReplaceRange(range);
			List.MoveRight(length + skipSize);
			skipSize++;
		}

		return skipSize;
	}

	
}
