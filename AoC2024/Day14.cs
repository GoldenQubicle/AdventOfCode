namespace AoC2024;

public class Day14 : Solution
{
	private readonly List<Robot> robots;
	public static int Width = 101;
	public static int Height = 103;

	public Day14(string file) : base(file) => robots = Input
		.Select(line => Regex.Match(line, @"p=(?<pos>-?\d+,-?\d+) v=(?<vel>-?\d+,-?\d+)"))
		.Select(m => new Robot(m.AsIntTuple("pos", ","), m.AsIntTuple("vel", ",")))
		.ToList( );


	public override async Task<string> SolvePart1()
	{
		for (var i = 0 ;i < 100 ;i++)
		{
			robots.ForEach(r => r.Move( ));
		}

		var q1 = robots.Count(r => r.X < Width / 2 && r.Y < Height / 2);
		var q2 = robots.Count(r => r.X > Width / 2 && r.Y < Height / 2);
		var q3 = robots.Count(r => r.X < Width / 2 && r.Y > Height / 2);
		var q4 = robots.Count(r => r.X > Width / 2 && r.Y > Height / 2);


		return (q1 * q2 * q3 * q4).ToString( );
	}

	public override async Task<string> SolvePart2()
	{
		var tick = 0;
		while (true)
		{
			var hasOverlap = robots.GroupBy(r => new Vector2(r.X, r.Y)).Any(g => g.Count( ) > 1);

			if (!hasOverlap) 
			{
				//uncomment to see the tree
				//var grid = new Grid2d(Width, Height);
				//robots.ForEach(r => grid[r.X, r.Y].Character = '#');
				//Console.WriteLine(grid);
				//await Task.Delay(2500);
				return tick.ToString();
			}

			robots.ForEach(r => r.Move( ));
			tick++;
		}
	}

	private record Robot((int x, int y) p, (int x, int y) v)
	{
		public int X { get; private set; } = p.x;
		public int Y { get; private set; } = p.y;

		public void Move()
		{
			X += v.x;
			Y += v.y;

			if (X < 0)
				X += Width;
			if (X > Width - 1)
				X -= Width;
			if (Y < 0)
				Y += Height;
			if (Y > Height - 1)
				Y -= Height;

		}
	}
}
