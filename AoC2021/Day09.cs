namespace AoC2021;

public class Day09 : Solution
{
	private readonly Grid2d grid;
	public Day09(string file) : base(file) => grid = new Grid2d(Input, diagonalAllowed: false);

	public override async Task<string> SolvePart1() => grid
		.Where(c => grid.GetNeighbors(c).All(n => n.Value > c.Value))
		.Sum(c => c.Value + 1).ToString();

	public override async Task<string> SolvePart2()
	{
		var lowPoints = grid.Where(c => grid.GetNeighbors(c).All(n => n.Value > c.Value));
		var visited = new HashSet<INode>();

		return lowPoints.Aggregate(new List<long>(), (basins, cell) =>
		{
			var count = 1;
			var cells = new Queue<INode>();
			visited.Add(cell);
			cells.Enqueue(cell);

			while (cells.Any())
			{
				var c = cells.Dequeue();
				var neighbors = grid.GetNeighbors(c, n => n.Value != 9 && !visited.Contains(n));
				neighbors.ForEach(n =>
				{
					visited.Add(n);
					cells.Enqueue(n);
				});
				count += neighbors.Count();
			}

			basins.Add(count);

			return basins;
		}).OrderByDescending(l => l).Take(3).Aggregate(1L, (sum, count) => count * sum).ToString();
	}
}