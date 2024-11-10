namespace AoC2023;

public class Day11 : Solution
{
	private readonly Grid2d grid;
	public Day11(string file) : base(file) => grid = new Grid2d(Input, diagonalAllowed: false);


	public override async Task<string> SolvePart1() => GetDistanceSum(2);


	public override async Task<string> SolvePart2() => GetDistanceSum(1000000);


	public string GetDistanceSum(int expansionFactor)
	{
		var emptyRows = Enumerable.Range(0, grid.Height)
			.Select(n => (isEmpty: grid.GetRow(n).All(c => c.Character == '.'), idx: n))
			.Where(r => r.isEmpty).ToList( );

		var emptyCols = Enumerable.Range(0, grid.Width)
			.Select(n => (isEmpty: grid.GetColumn(n).All(c => c.Character == '.'), idx: n))
			.Where(c => c.isEmpty).ToList( );

		var galaxies = grid.Where(c => c.Character == '#')
			.Select(g =>
			{
				var offsetX = emptyCols.Count(c => g.Position.x > c.idx) * (expansionFactor - 1);
				var offsetY = emptyRows.Count(r => g.Position.y > r.idx) * (expansionFactor - 1);
				return g.Position.Add(offsetX, offsetY).ToLong();
			}).ToList( );

		return Enumerable.Range(0, galaxies.Count - 1)
			.SelectMany(i => Enumerable.Range(i + 1, galaxies.Count - i - 1)
				.Select(k => (start: galaxies[i], end: galaxies[k])))
			.Sum(p => Maths.GetManhattanDistance(p.start, p.end))
			.ToString( );
	}
}
