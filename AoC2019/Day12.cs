namespace AoC2019;

public class Day12 : Solution
{
	public int Steps { get; set; } = 1000;

	private readonly List<Vector3> positions;

	private readonly List<Vector3> velocities = new( )
	{
		Vector3.Zero, Vector3.Zero, Vector3.Zero, Vector3.Zero
	};

	public Day12(string file) : base(file, "\n") => positions = Input
		.Select(l => Regex.Match(l, @"<x=(?<x>-?\d+), y=(?<y>-?\d+), z=(?<z>-?\d+)>"))
		.Select(m => m.AsVector3( )).ToList( );

	public override async Task<string> SolvePart1()
	{
		var idx = new List<int> { 0, 1, 2, 3 };
		var pairs = new Combinations<int>(idx, 2)
			.Select(p => (l: p[0], r: p[1])).ToList( );

		for (var s = 0; s < Steps; s++)
		{
			foreach (var pair in pairs)
			{
				var pull = GravityPull(pair);
				ApplyGravity(pair, pull);
			}

			foreach (var i in idx)
			{
				positions[i] = Vector3.Add(positions[i], velocities[i]);
			}
		}

		var potential = positions.Select(p => Math.Abs(p.X) + Math.Abs(p.Y) + Math.Abs(p.Z));
		var kinetic = velocities.Select(p => Math.Abs(p.X) + Math.Abs(p.Y) + Math.Abs(p.Z));

		return potential.Zip(kinetic, (p, k) => p * k).Sum().ToString();
	}

	public override async Task<string> SolvePart2()
	{
		return string.Empty;
	}

	private void ApplyGravity((int l, int r) pair, (Vector3 vl, Vector3 vr) pull)
	{
		velocities[pair.l] = Vector3.Add(velocities[pair.l], pull.vl);
		velocities[pair.r] = Vector3.Add(velocities[pair.r], pull.vr);
	}

	private (Vector3 vl, Vector3 vr) GravityPull((int l, int r) p) =>
		(new(Compare(Axis.X, p.l, p.r), Compare(Axis.Y, p.l, p.r), Compare(Axis.Z, p.l, p.r)),
		 new(Compare(Axis.X, p.r, p.l), Compare(Axis.Y, p.r, p.l), Compare(Axis.Z, p.r, p.l)));
	

	private float Compare(Axis axis, int l, int r) => (axis, positions[l], positions[r]) switch
	{
		(Axis.X, { X: var xl }, { X: var xr }) => xl == xr ? 0 : xl < xr ? 1 : -1,
		(Axis.Y, { Y: var yl }, { Y: var yr }) => yl == yr ? 0 : yl < yr ? 1 : -1,
		(Axis.Z, { Z: var zl }, { Z: var zr }) => zl == zr ? 0 : zl < zr ? 1 : -1,
	};

	private enum Axis { X, Y, Z }

}
