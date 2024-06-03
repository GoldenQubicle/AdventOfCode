namespace AoC2019;

public class Day10 : Solution
{
	private readonly IEnumerable<Vector2> asteroids;

	public Day10(string file) : base(file, "\n") =>
		asteroids = new Grid2d(Input)
			.Where(c => c.Character == '#')
			.Select(c => c.Position.ToVector2( )).ToList( );

	public override async Task<string> SolvePart1() => GetOptimalStation( ).Max(s => s.Value).ToString( );

	public override async Task<string> SolvePart2()
	{
		var station = GetOptimalStation( ).MaxBy(s => s.Value).Key;
		var toVaporize = asteroids.Except(new[ ] { station }).ToList( );

		var angles = new Dictionary<double, Vector2>( );
		var lengths = new Dictionary<double, float>( );

		foreach (var asteroid in toVaporize)
		{
			var l = Vector2.Distance(station, asteroid);
			var d = Vector2.Subtract(station, asteroid);
			var a = MathF.Atan2(d.Y, d.X) * 57.29578; //to degrees

			//shenanigans to get the correct angle ordering
			a = a < 0 ? a + 360 : a; // to 360
			a -= 90; // offset
			a = a < 0 ? a + 360 : a; // to 360 again

			if (!lengths.TryGetValue(a, out var length))
			{
				lengths.Add(a, l);
				angles.Add(a, asteroid);
			}
			else if (l < length)
			{
				lengths[a] = l;
				angles[a] = asteroid;
			}
		}

		var bet = angles.OrderBy(kvp => kvp.Key).ToList( )[199].Value;
		return (bet.X * 100 + bet.Y).ToString( );
	}

	private Dictionary<Vector2, int> GetOptimalStation()
	{
		return asteroids.Aggregate(new Dictionary<Vector2, int>( ), (result, current) =>
		{
			var angles = asteroids.Aggregate(new HashSet<double>( ), (set, other) =>
			{
				if (current == other)
					return set;

				var d = Vector2.Subtract(current, other);
				set.Add(MathF.Atan2(d.Y, d.X));
				return set;
			});

			result.Add(current, angles.Count);
			return result;
		});
	}


}
