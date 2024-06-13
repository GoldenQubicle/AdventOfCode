namespace AoC2019;

public class Day13 : Solution
{
	public Day13(string file) : base(file, "\n") { }

	public override async Task<string> SolvePart1()
	{
		var grid = new Grid2d( );
		var icc = new IntCodeComputer(Input)
		{
			BreakOnOutput = true
		};

		var instructions = new List<int>( );

		while (icc.Execute( ))
		{
			instructions.Add((int)icc.Output);
			if (instructions.Count < 3)
				continue;

			grid.Add(new((instructions[0], instructions[1]), instructions[2] switch
			{
				0 => '.',
				1 => 'W',
				2 => '#',
				3 => '_',
				4 => 'o',
			}));
			instructions.Clear( );
		}

		return grid.Count(c => c.Character == '#').ToString( );
	}

	public override async Task<string> SolvePart2()
	{
		var score = 0L;
		var instructions = new List<int>( );
		var grid = new Grid2d( );
		var icc = new IntCodeComputer(Input, m => m[0] = 2)
		{
			BreakOnOutput = true,
			GetExternalInput = () =>
			{
				var paddle = grid.Single(c => c.Character == '_');
				var ball = grid.Single(c => c.Character == 'o');
				return ball.X == paddle.X ? 0 : ball.X < paddle.X ? -1 : 1;
			}
		};
		
		while (icc.Execute( ))
		{
			instructions.Add((int)icc.Output);
			if (instructions.Count < 3)
				continue;

			var pos = (instructions[0], instructions[1]);
			var c = instructions[2] switch
			{
				0 => '.',
				1 => 'W',
				2 => '#',
				3 => '_',
				4 => 'o',
				_ => instructions[2]
			};

			if (pos == (-1, 0))
				score = c;
			else
				grid.AddOrUpdate(new Grid2d.Cell(pos, (char)c));
			
			instructions.Clear( );

		}

		return score.ToString();
	}
}
