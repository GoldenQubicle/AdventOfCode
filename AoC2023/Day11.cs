using Common;
using Common.Extensions;

namespace AoC2023;

public class Day11 : Solution
{
	private readonly Grid2d grid;
	public Day11(string file) : base(file) => grid = new Grid2d(Input, diagonalAllowed: false);


	public override string SolvePart1() => GetDistanceSum(2);


	public override string SolvePart2() => GetDistanceSum(1000000);


	public string GetDistanceSum(int expansionFactor)
	{
		var emptyRows = Enumerable.Range(0, grid.Height)
			.Select(n => (r: grid.Where(c => c.Position.y == n).All(c => c.Character == '.'), i: n))
			.Where(r => r.r).ToList( );

		var emptyCols = Enumerable.Range(0, grid.Width)
			.Select(n => (r: grid.Where(c => c.Position.x == n).All(c => c.Character == '.'), i: n))
			.Where(c => c.r).ToList( );

		var galaxies = grid.Where(c => c.Character == '#').ToList( );

		foreach (var galaxy in galaxies)
		{
			var offsetX = emptyCols.Count(c => galaxy.Position.x > c.i) * (expansionFactor - 1);
			var offsetY = emptyRows.Count(r => galaxy.Position.y > r.i) * (expansionFactor - 1);
			galaxy.Position = galaxy.Position.Add(offsetX, offsetY);
		}

		return Enumerable.Range(0, galaxies.Count - 1)
			.SelectMany(i => Enumerable.Range(i + 1, galaxies.Count - i - 1)
				.Select(k => (start: galaxies[i].Position.ToLong( ), end: galaxies[k].Position.ToLong( ))))
			.Sum(p => Maths.GetManhattanDistance(p.start, p.end))
			.ToString( );

	}
}
