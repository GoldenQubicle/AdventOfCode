namespace AoC2021;

public class Day22 : Solution
{
	private List<Cuboid> cuboids;

	public Day22(string file) : base(file, split: "\n") => cuboids = Input
		.Select(l => Regex.Match(l, @"(?<state>on|off) x=(?<x>-?\d+..-?\d+),y=(?<y>-?\d+..-?\d+),z=(?<z>-?\d+..-?\d+)"))
		.Select(m => new Cuboid(m.AsString("state"), ToRange(m.AsString("x")), ToRange(m.AsString("y")), ToRange(m.AsString("z"))))
		.ToList( );

	public override async Task<string> SolvePart1() => DoReactorReboot(isPart1: true);

	public override async Task<string> SolvePart2() => DoReactorReboot(isPart1: false);

	private string DoReactorReboot(bool isPart1)
	{
		var volumes = new List<Cuboid>( );
		var region = new Cuboid("", (-50, 50), (-50, 50), (-50, 50));
		cuboids = isPart1 ? cuboids.Where(c => c.HasOverlap(region)).ToList( ) : cuboids;

		foreach (var cuboid in cuboids)
		{
			if (cuboid.State == "on")
				volumes.Add(cuboid);

			var overlaps = volumes.Where(c => c.HasOverlap(cuboid)).ToList( );
			if (overlaps.Count == 0)
				continue;

			volumes.AddRange(overlaps.Select(c => c.GetOverlap(cuboid)));
		}

		return volumes.Aggregate(0L, (v, c) => c.State == "on" ? v + c.Volume : v - c.Volume).ToString( );
	}

	private static (long min, long max) ToRange(string match)
	{
		var parts = match.Split("..", StringSplitOptions.TrimEntries).Select(long.Parse).ToList( );
		var min = parts[0] < parts[1] ? parts[0] : parts[1];
		var max = min == parts[0] ? parts[1] : parts[0];
		return (min, max);
	}

	public record Cuboid(string State, (long min, long max) X, (long min, long max) Y, (long min, long max) Z)
	{
		public long Volume = (Math.Abs(X.max - X.min) + 1) * (Math.Abs(Y.max - Y.min) + 1) * (Math.Abs(Z.max - Z.min) + 1);

		public bool HasOverlap(Cuboid other)
		{
			if (other == this)
				return false;

			if (!(X.min <= other.X.max && X.max >= other.X.min))
				return false;

			if (!(Y.min <= other.Y.max && Y.max >= other.Y.min))
				return false;

			if (!(Z.min <= other.Z.max && Z.max >= other.Z.min))
				return false;

			return true;
		}

		public Cuboid GetOverlap(Cuboid other)
		{
			var min_x = Math.Max(X.min, other.X.min);
			var max_x = Math.Min(X.max, other.X.max);
			var min_y = Math.Max(Y.min, other.Y.min);
			var max_y = Math.Min(Y.max, other.Y.max);
			var min_z = Math.Max(Z.min, other.Z.min);
			var max_z = Math.Min(Z.max, other.Z.max);
			//had a nested ternary to determine state however it collapsed down into this
			var state = State == "on" ? "off" : "on";
			return new Cuboid(state, (min_x, max_x), (min_y, max_y), (min_z, max_z));
		}
	}
}
