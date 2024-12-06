namespace AoC2024;

public class Day06 : Solution
{

	private Grid2d grid;

	private readonly List<(int, int)> offsets = [(0, -1), (1, 0), (0, 1), (-1, 0)];

	public Day06(string file) : base(file) => grid = new(Input, diagonalAllowed: false);

	public override async Task<string> SolvePart1() => DoPatrol( ).visited.Count.ToString( );


	public override async Task<string> SolvePart2() => BruteForceCheck();
	//{
		/*
		 to get stuck in a loop there MUST be 4 obstacles forming a rectangle
		 since the guard can only move right, these rectangles will have the same relative layout
		 .#..
		 ...#
		 #...
		 ..#.

		since the guard starts facing up, the obstacles encountered follow a repeating pattern following the corners of said rectangle
		so the idea; get all obstacles. Per group of 3 try fit the missing corner. If success, that's a loop
		per group of 3 obstacles fit; bottom left, top left, top right, bottom right

		shit.. nested loop.. same principle? take 7.. fit leftdown minx -2 but there's eight options to check...
		and for triple nested loops, take 11, 12 options to check
		 */

		//var (visited, _) = DoPatrol( );
		//var placed = new List<(int, int)>();
		//for (var idx = 0 ;idx <= obstacles.Count - 3 ;idx++)
		//{
		//	var check = idx % 4;
		//	var corners = obstacles[idx..(idx + 3)];
		//	var tryFit = GetCorner((Corner)check, corners);

		//	if (grid.IsInBounds(tryFit) && visited.Contains(tryFit))
		//	{
		//		placed.Add(tryFit);
		//		grid[tryFit].Character = 'O';
		//		Console.WriteLine(grid);
		//	}
		//}


	//	return ;
	//}

	private string BruteForceCheck()
	{
		var loops = 0;
		var visited = DoPatrol().visited;

		foreach (var pos in visited.Skip(1))
		{
			grid = new(Input) { [pos] = { Character = '#' } };
			var current = grid.First(c => c.Character == '^').Position;

			var dir = 0;
			var loopFound = false;
			var obstacles = new HashSet<((int, int), int)>( );

			while (true)
			{
				var next = current.Add(offsets[dir]);

				if (!grid.IsInBounds(next))
					break;

				while (grid[next].Character == '#')
				{
					if (!obstacles.Add((next, dir)))
					{
						loopFound = true;
						break;
					}
					
					dir = (dir + 1) % 4;
					next = current.Add(offsets[dir]);
				}

				if (loopFound)
				{
					var idx = obstacles.WithIndex().First(o => o.Value == (next, dir)).idx;
					
					Console.WriteLine($"Found loop {loops} with {obstacles.Count - idx} obstacles");
					loops++;
					break;
				}

				current = next;
			}
		}

		return loops.ToString( );
	}

	private (int x, int y) GetCorner(Corner check, List<(int x, int y)> corners) => check switch
	{
		Corner.LeftDown => (corners.Min(c => c.x) - 1, corners.Max(c => c.y) - 1),
		Corner.LeftUp => (corners.Min(c => c.x) + 1, corners.Min(c => c.y) - 1),
		Corner.RightUp => (corners.Max(c => c.x) + 1, corners.Min(c => c.y) + 1),
		Corner.RightDown => (corners.Max(c => c.x) - 1, corners.Max(c => c.y) + 1),

	};


	private enum Corner { LeftDown, LeftUp, RightUp, RightDown }

	private (HashSet<(int, int)> visited, List<(int, int)> obstacles) DoPatrol()
	{
		var visited = new HashSet<(int, int)>( );
		var obstacles = new List<(int, int)>( );
		var current = grid.First(c => c.Character == '^').Position;
		var dir = 0;

		while (true)
		{
			visited.Add(current);
			var next = current.Add(offsets[dir]);

			if (!grid.IsInBounds(next))
				break;

			if (grid[next].Character == '#')
			{
				obstacles.Add(next);
				dir = (dir + 1) % 4;
				next = current.Add(offsets[dir]);
			}

			current = next;
		}

		return (visited, obstacles);
	}


}
