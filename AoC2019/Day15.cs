using System;
using Common.Renders;

namespace AoC2019;

public class Day15 : Solution
{
	public Day15(string file) : base(file) { }

	private enum Direction { North = 1, South = 2, West = 3, East = 4}

	public override async Task<string> SolvePart1()
	{
		var grid = new Grid2d(diagonalAllowed: false) { new((0, 0), '.') };

		var queue = new Queue<((int x, int y) p, IntCodeComputer icc)>
			{ ((0, 0), new IntCodeComputer(Input){ BreakOnOutput = true }) };

		while (queue.TryDequeue(out var current))
		{
			var replies = Enum.GetValues<Direction>( )
				.Select(d => (d: (long)d, p: GetPosition(current.p, d)))
				.Where(t => !grid.TryGetCell(t.p, out _))
				.Select(t =>
				{
					var icc = current.icc.Copy( );
					icc.Inputs.Add(t.d);
					icc.Execute( );
					return (t.d, t.p, icc);
				}).ToList( );

			foreach (var (_, p, icc) in replies)
			{
				switch (icc.Output)
				{
					case 0:
						grid.AddOrUpdate(new(p, '#'));
						break;
					case 1:
						grid.AddOrUpdate(new(p, '.'));
						queue.Enqueue((p, icc));
						break;
					case 2:
						grid.AddOrUpdate(new(p, 'O'));
						break;
				}
			}
		}

		// for current position
		// get the 4 adjacent positions
		// foreach position
		// check if already known
		// serve remaining input to icc 
		// handle result, which contains the position checked, output icc, icc copy
		// update grid, discard wall outputs, queue up free positions

		//410 / 411... not correct
		grid[0, 0].Character = 'S';
		Console.WriteLine(grid.Pad(blank: ' '));
		var path = await PathFinding.UniformCostSearch(grid[0, 0], grid.Single(c => c.Character == 'O'),grid,  
			(_, n) => n.Character == '.', 
			(c, _) => c.Character == 'O',
			(n1, n2) => GetManhattanDistance(n1.Cast<Grid2d.Cell>().Position, n2.Cast<Grid2d.Cell>().Position));

		//252 traced it by hand
		return (path.path.Count()).ToString();
	}



	private (int x, int y) GetPosition((int x, int y) current, Direction dir, bool reverse = false) => dir switch
	{
		Direction.North => reverse ? current.Add(0, 1) : current.Add(0, -1),
		Direction.South => reverse ? current.Add(0, -1) : current.Add(0, 1),
		Direction.West => reverse ? current.Add(1, 0) : current.Add(-1, 0),
		Direction.East => reverse ? current.Add(-1, 0) : current.Add(1, 0),
	};

	public override async Task<string> SolvePart2()
	{
		return string.Empty;
	}
}
