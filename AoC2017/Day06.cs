namespace AoC2017;

public class Day06 : Solution
{
	private readonly List<int> banks;

	public Day06(string file) : base(file) => banks = Input[0].Split('\t').Select(int.Parse).ToList( );

	public override async Task<string> SolvePart1() => DoCycles( );

	public override async Task<string> SolvePart2() => DoCycles(true);

	private string DoCycles(bool isPart2 = false)
	{
		var states = new HashSet<string>( );
		var seenBefore = false;

		while (true)
		{
			var state = banks.Aggregate(new StringBuilder( ), (sb, i) => sb.Append(i)).ToString( );

			if (!states.Add(state))
			{
				if (isPart2 && !seenBefore)
				{
					states = new( ) { state };
					seenBefore = true;
				}
				else
					break;
			}

			var max = banks.WithIndex( )
				.GroupBy(b => b.Value)
				.OrderByDescending(g => g.Key)
				.ThenBy(g => g.MinBy(b => b.idx))
				.First( ).First( );

			banks[max.idx] = 0;

			for (var i = 0 ;i < max.Value ;i++)
			{
				var idx = (max.idx + 1 + i) % banks.Count;
				banks[idx]++;
			}
		}

		return states.Count.ToString( );
	}
}
