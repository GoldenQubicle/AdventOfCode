using Common.Renders;

namespace AoC2019;

public class Day15 : Solution
{
	public Day15(string file) : base(file) { }

	private enum Direction { North = 1, South = 2, West = 3, East = 4, None = 99 }

	public override async Task<string> SolvePart1()
	{
		var current = (0, 0);
		var grid = new Grid2d(diagonalAllowed: false, isInfinite: true) { new(current, 'D') };
		var commands = new Stack<Direction> { Direction.North };
		var seen = new HashSet<(int x, int y)> ();

		var icc = new IntCodeComputer(Input)
		{
			BreakOnOutput = true,
			Inputs = new( ) { (long)Direction.North }
		};
		var seed = new Random().Next(int.MaxValue);
		var rnd = new Random(1545725704); // 1282718435, 1545725704
		while (icc.Execute( ))
		{
			var reply = icc.Output;
			if (reply == 0) //hit a wall
			{
				var wall = GetPosition(current, commands.Pop( ));
				grid.AddOrUpdate(new(wall, '#'));
				seen.Add(wall);
			}

			if (reply == 1)
			{
				//seen.Add(current);
				var moveTo = GetPosition(current, commands.Peek( ));
				grid[current].Character = '.';
				grid.AddOrUpdate(new(moveTo, 'D'));
				current = moveTo;
			}

			if (reply == 2)
			{
				grid[current].Character = '0';
				Console.WriteLine(grid);
				break;
			}

			var options = GetOptions(grid, current, seen);
			//if there's no options we need to backtrack.. i.e. pop command, get the inverse direction, sub from current and get neighbors until an opening is found.
			
			while (options.Count == 0)
			{
				var pc = commands.TryPop(out var pd) ? pd : Direction.None;
				if (pc == Direction.None)
				{
					seen.ForEach(s => grid[s].Character = grid[s].Character == '#' ? '#' : 'S');
					grid[current].Character = 'D';
					Console.WriteLine(grid);
				}

				var prev = GetPosition(current, pc, reverse: true);
				options = GetOptions(grid, prev, seen);
				grid[current].Character = '.';
				current = prev;
				grid[current].Character = 'D';

			}

			// issue: randomly taking first vs last option gives different results... 
			// and just always taking first or last option does not find the oxygen cell...
			// sooo what I reckon the issue is, I do not backtrack the icc
			// so the memory state still is at the current position, while the movement is execute presuming a different location..

			var n = rnd.Next(0, 2);
			var next = n == 0 ? options.Last().Position : options.First( ).Position;
			var dir = GetDirection(current, next);
			commands.Push(dir);
			seen.Add(current);
			icc.Inputs.Add((long)dir);

			grid.Pad(blank: ' ');
			Console.WriteLine(grid);
		}

		var path = PathFinding.BreadthFirstSearch(grid[0, 0], grid[current], grid, (c1, c2) => c2.Character != '#', (c1, c2) => c1.X == c2.X && c1.Y == c2.Y);

		return $"We ran this with seed {seed}";
	}

	private static List<Grid2d.Cell> GetOptions(Grid2d grid, (int, int) current, HashSet<(int x, int y)> seen) =>
		grid.GetNeighbors(new(current)).Where(c => !seen.Contains(c.Position)).ToList( );


	private Direction GetDirection((int x, int y) current, (int x, int y) next) =>
		(next.x - current.x , next.y - current.y) switch
		{
			(-1, 0) => Direction.West,
			(1, 0) => Direction.East,
			(0, -1) => Direction.North,
			(0, 1) => Direction.South,
		};

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
