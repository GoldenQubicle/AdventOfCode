namespace AoC2024;

public class Day10 : Solution
{
	private readonly Grid2d grid;

	public Day10(string file) : base(file) => grid = new(Input, diagonalAllowed: false);

	public override async Task<string> SolvePart1() => HikeTrails(isPart2: false);

	public override async Task<string> SolvePart2() => HikeTrails(isPart2: true);

	private string HikeTrails(bool isPart2) => grid
		.Where(c => c.Character is '0')
		.Select(DoHike)
		.Sum(t => isPart2 ? t.rating : t.score)
		.ToString( );

	private (int score, int rating) DoHike(INode start)
	{
		var trailEnds = new HashSet<INode>( );
		var queue = new Queue<INode> { start };
		var routes = 0;

		while (queue.TryDequeue(out var pos))
		{
			var options = grid.GetNeighbors(pos, n => pos.Value + 1 == n.Value).ToList( );

			while (options.Count > 0)
			{
				if (options.Skip(1).Any( ))
					options.Skip(1).ForEach(queue.Enqueue);

				pos = options.First( );
				options = grid.GetNeighbors(pos, n => pos.Value + 1 == n.Value).ToList( );
			}

			if (pos.Value != 9)
				continue;

			routes++;
			trailEnds.Add(pos);
		}

		return (trailEnds.Count, routes);
	}
}
