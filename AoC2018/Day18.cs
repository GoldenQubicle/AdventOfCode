namespace AoC2018;

public class Day18(string file) : Solution(file, split: "\n")
{
	public static class Acre
	{
		public const char Open = '.';
		public const char Trees = '|';
		public const char Yard = '#';
	}

	public override async Task<string> SolvePart1()
	{
		var ca = new CellularAutomaton2d(Input, Rules);

		ca.Iterate(10);

		var wood = ca.CountCells(Acre.Trees);
		var yards = ca.CountCells(Acre.Yard);

		return (wood * yards).ToString( );
	}

	public override async Task<string> SolvePart2()
	{
		var ca = new CellularAutomaton2d(Input, Rules);
		var cycle = ca.DetectCycle( );

		//modulo of cycle length for however many iterations are left once the cycle started gives the number of iterations to end on
		var end = (1000000000 - cycle.Start) % cycle.Length;

		ca = new CellularAutomaton2d(Input, Rules);

		ca.Iterate(cycle.Start + end);

		var wood = ca.CountCells(Acre.Trees);
		var yards = ca.CountCells(Acre.Yard);

		return (wood * yards).ToString( );
	}


	private static Grid2d.Cell Rules(Grid2d.Cell cell, IReadOnlyCollection<Grid2d.Cell> neighbors) => cell.Character switch
	{
		Acre.Open when neighbors.Count(c => c.Character == Acre.Trees) >= 3 => cell with { Character = Acre.Trees },
		Acre.Trees when neighbors.Count(c => c.Character == Acre.Yard) >= 3 => cell with { Character = Acre.Yard },
		Acre.Yard when !(neighbors.Any(c => c.Character == Acre.Yard) && neighbors.Any(c => c.Character == Acre.Trees)) => cell with { Character = Acre.Open },
		_ => cell
	};

}
