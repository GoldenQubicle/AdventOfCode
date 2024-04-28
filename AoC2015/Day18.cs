using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Common.Extensions;

namespace AoC2015;

public class Day18 : Solution
{
	private const char Active = '#';
	private const char InActive = '.';
	public int Steps { get; set; } = 100;

	public Day18(string file) : base(file, "\n")
	{
		var wut = file;
		var t = Input;

	}

	public override async Task<string> SolvePart1()
	{
		var ca = new CellularAutomaton2d(Input, DoApplyRules );
		ca.Iterate(Steps);
		return ca.CountCells(Active).ToString( );
	}

	public override async Task<string> SolvePart2()
	{
		var (ul, ur, bl, br) = GetCorners( );

		var ca = new CellularAutomaton2d(Input, (c, n) => IsCorner(c) ? c : DoApplyRules(c, n));

		ca.Iterate(Steps);
		
		return ca.CountCells(Active).ToString( );

		bool IsCorner(Grid2d.Cell c) =>
			c.Position == ul || c.Position == ur || c.Position == bl || c.Position == br;
	}

	private ((int x, int y) ul, (int x, int y) ur, (int x, int y) bl, (int x, int y) br) GetCorners()
	{
		var dim = Input.Count - 1;

		Input[0] = Input[0].ReplaceAt(0, Active);
		Input[0] = Input[0].ReplaceAt(dim, Active);
		Input[dim] = Input[dim].ReplaceAt(0, Active);
		Input[dim] = Input[dim].ReplaceAt(dim, Active);

		return ((0, 0), (dim, 0), (0, dim), (dim, dim));
	}


	private static Grid2d.Cell DoApplyRules(Grid2d.Cell cell, IReadOnlyCollection<Grid2d.Cell> neighbors) =>
		neighbors.Count(c => c.Character == Active) switch
		{
			< 2 or > 3 when cell.Character == Active => cell with { Character = InActive },
			3 when cell.Character == InActive => cell with { Character = Active },
			_ => cell
		};

}