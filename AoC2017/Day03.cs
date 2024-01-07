namespace AoC2017;

public class Day03 : Solution
{
	private readonly int steps;

	private enum Direction { Right, Up, Left, Down }
	private readonly Grid2d grid = new( ) { new((0, 0), '1') };

	public Day03(string file) : base(file) => steps = int.Parse(Input.First( ));

	public Day03(List<string> input) : base(input) => steps = int.Parse(Input.First( ));

	public override async Task<string> SolvePart1() => DoSpiralGrid( );

	public override async Task<string> SolvePart2() => DoSpiralGrid(isPart2: true);

	private string DoSpiralGrid(bool isPart2 = false)
	{
		var current = (0, 0);
		var step = 1;

		while (grid.Count < steps)
		{
			if (int.IsEvenInteger(step))
			{
				if (TakeSteps(isPart2, step, Direction.Left, ref current, out var s1))
					return s1;
				if (TakeSteps(isPart2, step, Direction.Down, ref current, out var s2))
					return s2;
			}
			else
			{
				if (TakeSteps(isPart2, step, Direction.Right, ref current, out var s1))
					return s1;
				if (TakeSteps(isPart2, step, Direction.Up, ref current, out var s2))
					return s2;
			}

			step++;
		}

		return Maths.GetManhattanDistance((0, 0), grid[steps - 1].Position).ToString( );
	}

	private bool TakeSteps(bool isPart2, int step, Direction dir, ref (int, int) current, out string s)
	{
		for (var i = 0 ;i < step ;i++)
		{
			current = dir switch
			{
				Direction.Right => current.Add(1, 0),
				Direction.Up => current.Add(0, -1),
				Direction.Left => current.Add(-1, 0),
				Direction.Down => current.Add(0, 1),
			};

			var p = new Grid2d.Cell(current);

			if (isPart2)
			{
				p.Value = grid.GetNeighbors(p).Sum(c => c.Value);
				if (p.Value > steps)
				{
					s = p.Value.ToString( );
					return true;
				}
			}

			grid.Add(p);
		}
		s = string.Empty;
		return false;
	}
}
