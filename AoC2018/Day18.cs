namespace AoC2018;

public class Day18 : Solution
{
	private Grid2d grid;

	public static class Acre
	{
		public const char Open = '.';
		public const char Trees = '|';
		public const char Yard = '#';
	}

	public Day18(string file) : base(file, split: "\n") => grid = new Grid2d(Input);

	public override async Task<string> SolvePart1()
	{
		var ca = new CellularAutomaton2d(Input) { Rules = DoApplyRules };

		ca.Iterate(10);

		var wood = ca.CountCells(Acre.Trees);
		var yards = ca.CountCells(Acre.Yard);

		return (wood * yards).ToString( );
	}

	public override async Task<string> SolvePart2()
	{
		var iteration = 0;
		var state = new Dictionary<string, int>();

		while (true)
		{
			iteration++;
			grid  = DoIteration(grid);

			//break when the same state has been added, assuming start of cycle
			if (!state.TryAdd(grid.ToString(), iteration))
				break;
		}

		var start = state[grid.ToString()];
		//modulo of cycle length for however many iterations are left once the cycle started gives the number of iterations to end on
		var end = (1000000000 - start) % (iteration - start); 

		grid = new Grid2d(Input);

		Iterate(start + end);

		var wood = grid.Count(c => c.Character == Acre.Trees);
		var yards = grid.Count(c => c.Character == Acre.Yard);

		return (wood * yards).ToString( );
	}

	public void Iterate(int steps)
	{
		for (var i = 0 ;i < steps ;i++)
		{
			grid = DoIteration(grid);
		}
	}

	private static Grid2d DoIteration(Grid2d g) => g.Aggregate(new Grid2d( ), (newGrid, cell) =>
	{
		newGrid.Add(DoApplyRules(cell, g.GetNeighbors(cell)));
		return newGrid;
	});


	private static Grid2d.Cell DoApplyRules(Grid2d.Cell cell, IReadOnlyCollection<Grid2d.Cell> neighbors) => cell.Character switch
	{
		Acre.Open when neighbors.Count(c => c.Character == Acre.Trees) >= 3 => cell with { Character = Acre.Trees },
		Acre.Trees when neighbors.Count(c => c.Character == Acre.Yard) >= 3 => cell with { Character = Acre.Yard },
		Acre.Yard when !(neighbors.Any(c => c.Character == Acre.Yard) && neighbors.Any(c => c.Character == Acre.Trees)) => cell with { Character = Acre.Open },
		_ => cell
	};

}
