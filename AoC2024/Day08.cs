namespace AoC2024;

public class Day08 : Solution
{
	private readonly Grid2d grid;

	public Day08(string file) : base(file) => grid = new(Input);


	public override async Task<string> SolvePart1() => CountAntiNodes(isPart2: false);


	public override async Task<string> SolvePart2() => CountAntiNodes(isPart2: true);
	

	private string CountAntiNodes(bool isPart2) => GetGroupedAntennas( )
		.Aggregate(new HashSet<(int, int)>( ), (set, list) => GetAntiNodes(list, set, isPart2))
		.Count.ToString( );


	private HashSet<(int, int)> GetAntiNodes(List<(int x, int y)> list, HashSet<(int, int)> set, bool isPart2 = false)
	{
		GetIndexPairs(list.Count)
			.ForEach(pair => set.AddRange(GetAntiNodesForPair(list[pair.a1], list[pair.a2], isPart2)));
		return set;
	}
	
	
	private IEnumerable<(int, int)> GetAntiNodesForPair((int x, int y) a1, (int x, int y) a2, bool isPart2)
	{
		var antinodes = new List<(int, int)>( );
		var delta = a1.Subtract(a2);
		var n1 = a1.Add(delta);
		var n2 = a2.Subtract(delta);

		if (isPart2)
		{
			while (grid.IsInBounds(n1))
			{
				antinodes.Add(n1);
				n1 = n1.Add(delta);
			}

			while (grid.IsInBounds(n2))
			{
				antinodes.Add(n2);
				n2 = n2.Subtract(delta);
			}

			antinodes.Add(a1);
			antinodes.Add(a2);
			return antinodes;
		}

		if (grid.IsInBounds(n1))
			antinodes.Add(n1);

		if (grid.IsInBounds(n2))
			antinodes.Add(n2);

		return antinodes;
	}


	private static IEnumerable<(int a1, int a2)> GetIndexPairs(int count) =>
		Enumerable.Range(0, count - 1)
			.SelectMany(a1 => Enumerable.Range(a1 + 1, count - 1 - a1).Select(a2 => (a1, a2)));


	private List<List<(int x, int y)>> GetGroupedAntennas() => grid
		.Where(c => c.Character is not '.')
		.GroupBy(c => c.Character)
		.Where(g => g.Count( ) >= 2)
		.Select(g => g.Select(c => c.Position).ToList( )).ToList( );
}
