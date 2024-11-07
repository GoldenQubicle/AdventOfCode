using Common.Interfaces;

namespace AoC2018;

public class Day17 : Solution
{
	private readonly Grid2d grid;

	public Day17(string file) : base(file, split: "\n") => grid = Input
			.Select(l => Regex.Match(l, @"(x|y)=(\d+),\s(x|y)=(\d+)..(\d+)"))
			.Aggregate(new Grid2d(diagonalAllowed: false), (g, m) =>
			{
				for (var i = m.AsInt(4) ;i <= m.AsInt(5) ;i++)
				{
					var pos = m.AsString(1).Equals("x") ? (m.AsInt(2), i) : (i, m.AsInt(2));
					g.Add(new(pos, '#'));
				}
				return g;
			}).Pad(1);

	private enum Direction { Down, Left, Right }

	public override async Task<string> SolvePart1()
	{
		var maxy = grid.GetBounds( ).maxy;
		var start = (500, 0);
		var current = grid[start];
		var wayPoints = new Stack<(Grid2d.Cell c, Direction d)>( );
		wayPoints.Push((current, Direction.Down));
		var direction = Direction.Down;
		var count = 0;
		while (true)
		{
			count++;


			while (CanMove(current, Direction.Down, out var down))
			{
				if (current.Y == maxy -1)
				{
					//we're at the bottom. We can assume to fill from here to the last 2 way points..?

				}

				if (direction != Direction.Down)
				{
					wayPoints.Push((current, direction));
					direction = Direction.Down;
				}
				current = down;
			}

			if (CanMove(current, Direction.Left, out var left))
			{
				if (direction != Direction.Left)
				{
					wayPoints.Push((current, direction));
					direction = Direction.Left;
				}
				current = left;
				continue;
			}

			if (CanMove(current, Direction.Right, out var right) && direction != Direction.Left)
			{
				if (direction != Direction.Right)
				{
					wayPoints.Push((current, direction));
					direction = Direction.Right;
				}
				current = right;
				continue;
			}


			var lastWaypoint = wayPoints.Peek( );
			var min = current.X < lastWaypoint.c.X ? current.Position : lastWaypoint.c.Position;
			var max = min == current.Position ? lastWaypoint.c.Position : current.Position;
			
			grid.GetRange(min, max).ForEach(c => c.Character = '~');

			Console.WriteLine(grid);
			Console.WriteLine(count);
			Console.WriteLine("----------------------------------");

			if (direction != Direction.Down)
			{
				//if we're not going down, go back and check of we can go right
				current = lastWaypoint.c;
				direction = lastWaypoint.d;
			}
			else
			{
				// we were going down and cannot go left and right
				current = grid[lastWaypoint.c.Position.Add(0, -1)];
				direction = lastWaypoint.d;
				wayPoints.Pop();
				wayPoints.Push((current, direction));
			}

			
		
		}


		return string.Empty;
	}

	private bool CanMove(Grid2d.Cell current, Direction direction, out Grid2d.Cell next)
	{
		next = null;
		var position = direction switch
		{
			Direction.Down => current.Position.Add(0, 1),
			Direction.Left => current.Position.Add(-1, 0),
			Direction.Right => current.Position.Add(1, 0),
			_ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
		};

		if (grid.TryGetCell(position, out var c) && c.Character != '.')
			return false;

		next = grid[position];
		return true;

	}


	public override async Task<string> SolvePart2()
	{
		return string.Empty;
	}
}
