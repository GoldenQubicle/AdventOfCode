namespace AoC2018;

public class Day03 : Solution
{
	private readonly List<Rect> rects;

	private record Rect(int Id, (int x, int y) Pos, (int w, int h) Dim)
	{
		public IEnumerable<(int, int)> GetSurface() =>
			Enumerable.Range(Pos.x, Dim.w).SelectMany(x =>
				Enumerable.Range(Pos.y, Dim.h).Select(y => (x, y)));
	};
	
	public Day03(string file) : base(file)
	{
		rects = Input
			.Select(s => Regex.Match(s, @"(?<=#)(?<id>\d+).*(?<=@.)(?<pos>\d+,\d+).*(?<=:.)(?<dim>\d+x\d+)"))
			.Select(m => new Rect(m.AsInt("id"), m.AsIntTuple("pos", ","), m.AsIntTuple("dim", "x"))).ToList();

	}

	public override async Task<string> SolvePart1() => rects
		.Select(r => r.GetSurface())
		.Aggregate(new Dictionary<(int x, int y), int>(), (grid, s) =>
		{
			s.ForEach(c =>
			{
				if (grid.ContainsKey(c))
					grid[c]++;
				else
					grid.Add(c, 1);
			});

			return grid;
		})
		.Count(kvp => kvp.Value > 1)
		.ToString();


	public override async Task<string> SolvePart2() => rects
		.Aggregate(new Dictionary<(int x, int y), List<int>>(), (grid, r) =>
		{
			r.GetSurface().ForEach(c =>
			{
				if (grid.ContainsKey(c))
					grid[c].Add(r.Id);
				else
					grid.Add(c, new() { r.Id });
			});

			return grid;
		})
		.Where(kvp => kvp.Value.Count == 1)
		.GroupBy(kvp => kvp.Value.First())
		.First(g => g.Count() == rects.Single(r => r.Id == g.Key).GetSurface().Count()).Key.ToString();

}