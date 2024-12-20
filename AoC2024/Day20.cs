namespace AoC2024;

public class Day20 : Solution
{
	public static int MinimalTimeSaved = 100;
	
	private readonly Grid2d grid;

	public Day20(string file) : base(file) => grid = new(Input, diagonalAllowed: false);

	public override async Task<string> SolvePart1() => await GetShortCuts(cheatThreshold: 2);
	
	public override async Task<string> SolvePart2() => await GetShortCuts(cheatThreshold: 20);
	
	private async Task<string> GetShortCuts(int cheatThreshold)
	{
		var track = await GetTrack(grid.First(c => c.Character is 'S'), grid.First(c => c.Character is 'E'))
			.ContinueWith(t => t.Result.WithIndex( ).ToDictionary(n => n.Value, n => n.idx));

		var shortCuts = new Dictionary<int, int>( );

		foreach (var (n1, s1) in track)
		{
			foreach (var (n2, s2) in track.Skip(s1))
			{
				var distance = Maths.GetManhattanDistance(n1, n2);
				if (distance > cheatThreshold)
					continue;

				var length = s2 - s1 - distance;
				if (length < MinimalTimeSaved)
					continue;

				if (!shortCuts.TryAdd(length, 1))
					shortCuts[length]++;
			}
		}

		return shortCuts.Values.Sum( ).ToString( );
	}

	private async Task<List<INode>> GetTrack(INode start, INode end)
	{
		var path = await PathFinding.BreadthFirstSearch(
			start: start,
			target: end,
			constraint: (_, node1) => node1.Character is '.' or 'E',
			graph: grid,
			targetCondition: (node, node1) => node.Position == node1.Position);

		return path.Reverse( ).ToList( );
	}
}
