namespace AoC2024;

public class Day06(string file) : Solution(file)
{

	private readonly List<(int, int)> offsets = [(0, -1), (1, 0), (0, 1), (-1, 0)];

	public override async Task<string> SolvePart1() => DoPatrol( ).Count.ToString( );

	public override async Task<string> SolvePart2()
	{
		var loops = 0;
		var visited = DoPatrol( );

		Parallel.ForEach(visited.Skip(1), pos =>
		{
			var grid = new Grid2d(Input) { [pos] = { Character = '#' } };
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
						loopFound = true;
				
					dir = (dir + 1) % 4;
					next = current.Add(offsets[dir]);
				}

				if (loopFound)
				{
					loops++;
					break;
				}

				current = next;
			}
		});

		return loops.ToString( );
	}


	private HashSet<(int, int)>  DoPatrol()
	{
		var grid = new Grid2d(Input);
		var visited = new HashSet<(int, int)>( );
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
				dir = (dir + 1) % 4;
				next = current.Add(offsets[dir]);
			}

			current = next;
		}

		return visited;
	}
}
