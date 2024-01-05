using Common;

namespace AoC2023;

public class Day13 : Solution
{
	private readonly List<Grid2d> grids = new( );
	public Day13(string file) : base(file, doRemoveEmptyLines: false)
	{
		var idx = 0;
		while (idx < Input.Count)
		{
			var grid = new Grid2d(Input.Skip(idx).TakeWhile(l => l != string.Empty).ToList( ));
			grids.Add(grid);
			idx += grid.Height + 1;
		}
	}


	public override async Task<string> SolvePart1() => grids.Select(g => GetMirrorLines(g, isSmudged: false))
			.Sum(t => t.c.Sum( ) + t.r.Sum(i => i * 100)).ToString( );


	public override async Task<string> SolvePart2() => grids.Select(g => GetMirrorLines(g, isSmudged: true))
		.Sum(t => t.c.Sum( ) + t.r.Sum(i => i * 100)).ToString( );


	private (IEnumerable<int> c, IEnumerable<int> r) GetMirrorLines(Grid2d g, bool isSmudged) =>
		(c: Enumerable.Range(0, g.Width - 1).Select(c => IsMirrorLine(g, c, c + 1, isRow: false, isSmudged) ? c + 1 : 0).ToList( ),
		r: Enumerable.Range(0, g.Height - 1).Select(r => IsMirrorLine(g, r, r + 1, isRow: true, isSmudged) ? r + 1 : 0).ToList( ));


	public static bool IsMirrorLine(Grid2d g, int a, int b, bool isRow, bool isSmudged)
	{
		var matches = new List<List<bool>>( );

		while (Exit(a, b))
		{
			matches.Add(Match(g, a, b, isRow));
			a--;
			b++;
		}

		return isSmudged 
			? matches.SelectMany(m => m).Count(m => !m) == 1
			: matches.SelectMany(m => m).All(m => m);


		bool Exit(int a, int b) => isRow
			? a >= 0 && b < g.Height
			: a >= 0 && b < g.Width;
	}


	public static List<bool> Match(Grid2d grid, int a, int b, bool isRow) => isRow
		? grid.GetRow(a).Zip(grid.GetRow(b), (a, b) => a.Character == b.Character).ToList( )
		: grid.GetColumn(a).Zip(grid.GetColumn(b), (a, b) => a.Character == b.Character).ToList( );

}
