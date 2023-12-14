using System.Collections.Generic;
using System.Linq.Expressions;
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

	public override string SolvePart1() => grids.Select(g => (g,
			c: Enumerable.Range(0, g.Width - 1).Select(c => IsMirrorLine(g, c, c + 1, isRow: false) ? c + 1 : 0),
			r: Enumerable.Range(0, g.Height - 1).Select(c => IsMirrorLine(g, c, c + 1, isRow: true) ? c + 1 : 0)))
			.Sum(t => t.c.Sum( ) + t.r.Sum(i => i * 100)).ToString( );


	public override string SolvePart2()
	{
		foreach (var grid in grids)
		{
			var r = SmudgedRows(grid);
			var c = SmudgedColumns(grid);
			if (r.Count() > 0 )
			{
				grid.GetRow(r[0].a)[r[0].c.IndexOf(false)].Character = '#';
				grid.GetRow(r[0].b)[r[0].c.IndexOf(false)].Character = '#';
			}

			//if (c.Count( ) > 0)
			//{
			//	grid.GetColumn(c[0].a)[c[0].c.IndexOf(false)].Character = '.';
			//	grid.GetColumn(c[0].b)[c[0].c.IndexOf(false)].Character = '.';
			//}
		}

		return SolvePart1( );
	}

	private static List<(int a, int b, List<bool> c)> SmudgedColumns(Grid2d grid)
	{
		var result = new List<(int a, int b, List<bool>)>();
		for (var a = 0 ;a < grid.Width - 2 ;a++)
		{
			for (var b = 0 ;b < grid.Width - 1 ;b++)
			{
				var matchCols = Match(grid, a, b, isRow: false).ToList( );

				if (matchCols.Count(t => !t) == 1)
				{
					result.Add((a, b, matchCols));
				}
			}
		}

		return result;
	}

	private static List<(int a, int b, List<bool> c)> SmudgedRows(Grid2d grid)
	{
		var result = new List<(int a, int b, List<bool>)>( );
		for (var a = 0; a < grid.Height - 2; a++)
		{
			for (var b = 0; b < grid.Height - 1; b++)
			{
				var matchRow = Match(grid, a, b, isRow: true).ToList();
				
				if (matchRow.Count(t => !t) == 1)
				{
					result.Add((a, b, matchRow));
				}
			}
		}

		return result;
	}

	public static bool IsMirrorLine(Grid2d g, int a, int b, bool isRow)
	{
		bool Exit(int a, int b) => isRow
				? a >= 0 && b < g.Height
				: a >= 0 && b < g.Width;

		var result = new List<bool>( );
		while (Exit(a, b))
		{
			result.Add(Match(g, a, b, isRow).All(t => t));
			a--;
			b++;
		}

		return result.All(m => m);
	}

	public static IEnumerable<bool> Match(Grid2d grid, int a, int b, bool isRow) => isRow
		? grid.GetRow(a).Zip(grid.GetRow(b), (a, b) => a.Character == b.Character)
		: grid.GetColumn(a).Zip(grid.GetColumn(b), (a, b) => a.Character == b.Character);




}
