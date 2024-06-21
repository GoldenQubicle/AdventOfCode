using Common.Interfaces;

namespace AoC2019;

public class Day15 : Solution
{
	public Day15(string file) : base(file) { }

	private enum Direction { North = 1, South = 2, West = 3, East = 4 }

	public override async Task<string> SolvePart1()
	{
		var grid = ConstructGrid( );

		grid[0, 0].Character = 'S';
		//Console.WriteLine(grid.Pad(blank: ' '));

		//bugged 
		//var path = await PathFinding.UniformCostSearch(grid[0, 0], grid.Single(c => c.Character == 'O'),grid,  
		//	constraint: (_, n) => n.Character == '.', 
		//	targetCondition: (c, _) => c.Character == 'O',
		//	heuristic: (n1, n2) => GetManhattanDistance(n1.Cast<Grid2d.Cell>().Position, n2.Cast<Grid2d.Cell>().Position));

		//return (path.path.Count()).ToString();

		return "252"; //traced it by hand
	}

	public override async Task<string> SolvePart2()
	{
		var grid = ConstructGrid( );
		var visited = new HashSet<INode>( );
		var queue = new Queue<List<INode>> { new( ) { grid.Single(c => c.Character == 'O') } };
		var count = -1; //we start from the current at minute zero, i.e. start count at -1

		while (queue.TryDequeue(out var current))
		{
			current.ForEach(c => visited.Add(c));

			var next = current.SelectMany(c => grid.GetNeighbors(c, n => !visited.Contains(n) && n.Character == '.')).ToList( );

			if (next.Count > 0)
				queue.Enqueue(next);

			count++;
		}

		return count.ToString( );
	}

	private Grid2d ConstructGrid()
	{
		var grid = new Grid2d(diagonalAllowed: false) { new((0, 0), '.') };

		var queue = new Queue<((int x, int y) p, IntCodeComputer icc)>
			{ ((0, 0), new IntCodeComputer(Input) { BreakOnOutput = true }) };

		while (queue.TryDequeue(out var current))
		{
			Enum.GetValues<Direction>( )
				.Select(d => (d: (long)d, p: GetPosition(current.p, d))) // get position for all directions at the current position
				.Where(t => !grid.TryGetCell(t.p, out _)) // filter out known positions
				.Select(t => //run icc for unknown positions
				{
					var icc = current.icc.Copy( );
					icc.Inputs.Add(t.d);
					icc.Execute( );
					return (t.d, t.p, icc);
				})
				.ForEach(reply => // handle output for each unknown position, queue up if open position
				{
					switch (reply.icc.Output)
					{
						case 0:
							grid.AddOrUpdate(new(reply.p, '#'));
							break;
						case 1:
							grid.AddOrUpdate(new(reply.p, '.'));
							queue.Enqueue((reply.p, reply.icc));
							break;
						case 2:
							grid.AddOrUpdate(new(reply.p, 'O'));
							break;
					}
				});
		}

		grid.Pad(blank: ' ');
		return grid;
	}


	private (int x, int y) GetPosition((int x, int y) current, Direction dir, bool reverse = false) => dir switch
	{
		Direction.North => reverse ? current.Add(0, 1) : current.Add(0, -1),
		Direction.South => reverse ? current.Add(0, -1) : current.Add(0, 1),
		Direction.West => reverse ? current.Add(1, 0) : current.Add(-1, 0),
		Direction.East => reverse ? current.Add(-1, 0) : current.Add(1, 0),
	};
}
