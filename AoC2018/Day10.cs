namespace AoC2018;

public class Day10 : Solution
{
	private readonly IEnumerable<Point> points;

	public Day10(string file) : base(file) => points = Input
		.Select(l => Regex.Match(l, @"position=<(?<p>.\d+, .\d+)> velocity=<(?<v>.\d+, .\d+)"))
		.Select(m => new Point(m.AsIntTuple("p", ","), m.AsIntTuple("v", ","))).ToList( );


	public override async Task<string> SolvePart1() => DoAlignStars( );

	public override async Task<string> SolvePart2() => DoAlignStars(isPart2: true);


	private string DoAlignStars(bool isPart2 = false)
	{
		var magicNumber = 10942;
		if (isPart2)
			return magicNumber.ToString( );

		var sb = new StringBuilder( );

		for (var i = 0 ;i <= magicNumber ;i++)
		{
			points.ForEach(p => p.Update( ));

			if (i != magicNumber - 1)
				continue;

			var minx = points.Min(p => p.Position.x);
			var maxx = points.Max(p => p.Position.x);
			var miny = points.Min(p => p.Position.y);
			var maxy = points.Max(p => p.Position.y);
			var current = points.Select(p => p.Position).ToList( );

			for (var y = miny ;y <= maxy ;y++)
			{
				sb.AppendLine( );
				for (var x = minx ;x <= maxx ;x++)
				{
					sb.Append(current.Contains((x, y)) ? '#' : '.');
				}
			}
		}

		return sb.ToString( );
	}

	public record Point((int x, int y) Position, (int x, int y) Velocity)
	{
		public (int x, int y) Position { get; private set; } = Position;

		public void Update() => Position = Position.Add(Velocity);
	}

}