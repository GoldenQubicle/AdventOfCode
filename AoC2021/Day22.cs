namespace AoC2021;

public class Day22 : Solution
{
	private readonly List<Cuboid> cuboids;
	private Dictionary<Vector3, bool> reactor = new( );

	public Day22(string file) : base(file, split: "\n")
	{
		cuboids = Input
			.Select(l => Regex.Match(l, @"(?<state>on|off) x=(?<x>-?\d+..-?\d+),y=(?<y>-?\d+..-?\d+),z=(?<z>-?\d+..-?\d+)"))
			.Select(m => new Cuboid(m.AsString("state"), ToRange(m.AsString("x")), ToRange(m.AsString("y")), ToRange(m.AsString("z"))))
			.ToList( );
	}


	public override async Task<string> SolvePart1()
	{
		cuboids.ForEach(s =>
		{
			foreach (var point in s.GetPoints( ))
			{
				if (reactor.ContainsKey(point))
					reactor[point] = s.GetState( );
				else
					reactor.Add(point, s.GetState( ));
			}
		});

		return reactor.Count(kvp => kvp.Value).ToString( );
	}


	public override async Task<string> SolvePart2()
	{
		return string.Empty;
	}

	private (int min, int max) ToRange(string match)
	{
		var parts = match.Split("..", StringSplitOptions.TrimEntries).Select(int.Parse).ToList( );
		var min = parts[0] < parts[1] ? parts[0] : parts[1];
		var max = min == parts[0] ? parts[1] : parts[0];
		return (min, max);
	}

	public static bool Intersects(Cuboid c1, Cuboid c2)
	{
		if (!(c1.X.min <= c2.X.max && c1.X.max >= c2.X.min))
			return false;

		if (!(c1.Y.min <= c2.Y.max && c1.Y.max >= c2.Y.min))
			return false;

		if (!(c1.Z.min <= c2.Z.max && c1.Z.max >= c2.Z.min))
			return false;

		return true;
	}

	public static Cuboid GetIntersectVolume(Cuboid c1, Cuboid c2)
	{
		var min_x = Math.Max(c1.X.min, c2.X.min);
		var max_x = Math.Min(c1.X.max, c2.X.max);
		var min_y = Math.Max(c1.Y.min, c2.Y.min);
		var max_y = Math.Min(c1.Y.max, c2.Y.max);
		var min_z = Math.Max(c1.Z.min, c2.Z.min);
		var max_z = Math.Min(c1.Z.max, c2.Z.max);

		var state = c2.GetState( ) ? "off" : "on";

		return new Cuboid(state, (min_x, max_x), (min_y, max_y), (min_z, max_z));
	}

	public record Cuboid(string State, (int min, int max) X, (int min, int max) Y, (int min, int max) Z)
	{
		public List<Vector3> GetPoints()
		{
			if (!(OverlapsRegion(X) && OverlapsRegion(Y) && OverlapsRegion(Z)))
				return new List<Vector3>( );

			return Enumerable.Range(X.min, X.max - X.min + 1)
				.SelectMany(x => Enumerable.Range(Y.min, Y.max - Y.min + 1)
					.SelectMany(y => Enumerable.Range(Z.min, Z.max - Z.min + 1)
							.Select(z => new Vector3(x, y, z)))).ToList( );
		}

		public bool GetState() => State switch
		{
			"on" => true,
			"off" => false,
			_ => throw new ArgumentException( )
		};

		public int GetVolume() => Math.Abs(X.max - X.min) * Math.Abs(Y.max - Y.min) * Math.Abs(Z.max - Z.min);

		private bool OverlapsRegion((int min, int max) range) =>
			range.min is >= -50 and <= 50 ||
			range.max is >= -50 and <= 50 ||
			range is { min: < -50, max: > 50 };
	}
}
