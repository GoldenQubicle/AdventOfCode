namespace AoC2023;

public class Day21 : Solution
{
	private readonly Grid2d grid;

	public static int Steps = 64;

	public Day21(string file) : base(file) => grid = new Grid2d(Input, diagonalAllowed: false);


	public override async Task<string> SolvePart1() => DoSteps(Steps, grid.First(c => c.Character == 'S').Position);

	public override async Task<string> SolvePart2()
	{
		var steps = 63;
		var topLeft = DoSteps(steps, (0, 0));
		var topRight = DoSteps(steps, (grid.Width-1, 0));
		var downLeft = DoSteps(steps, (0, grid.Height - 1));
		var downRight = DoSteps(steps, (grid.Width - 1, grid.Height - 1));


		// 26501365 in any direction
		// 26501365 - 65, this will get us to the border of the original grid from s
		// (26501365 - 65) / 131 = 202300 we will cross this many additional grids while walking
		var fromMid = (26501365 - 65L) / 131L;
		var axis = (fromMid * 2) + 1;
		var totalGrid = axis * axis;
		//still need to figure out how many are reached in each quadrant, e.g. start at 0,0, width,0 etc
		//then figure out how many quadrants of each type and how many full grids
		return ((totalGrid * 7689L) - (totalGrid * 3885L)).ToString();
	}

	private string DoSteps(int steps, (int x, int y) start)
	{
		
		var queue = new Queue<INode>();
		queue.Enqueue(grid[start]);
		
		for (var i = 0; i <= steps; i++)
		{
			var next = new HashSet<INode>();

			while (queue.TryDequeue(out var current))
			{
				grid.GetNeighbors(current, n => n.Character == '.')
					.ForEach(n => next.Add(n));
			}
			Console.WriteLine($"step {i}: reachable {next.Count}");
			if (i == steps )
			{
				next.ForEach(c => c.Character = 'A');
				Console.WriteLine(grid);
				return (next.Count + 1).ToString();
			}
			queue.EnqueueAll(next);
		}


		return "";
	}

	

}
