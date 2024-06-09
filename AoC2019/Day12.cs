namespace AoC2019;

public class Day12 : Solution
{
	public int Steps { get; set; } = 1000;

	private readonly List<Vector3> positions;

	private readonly List<Vector3> velocities = new( )
	{
		Vector3.Zero, Vector3.Zero, Vector3.Zero, Vector3.Zero
	};

	private static readonly List<int> ids = new( ) { 0, 1, 2, 3 };
	private readonly List<(int l, int r)> pairs = new Combinations<int>(ids, 2)
		.Select(p => (l: p[0], r: p[1])).ToList( );

	public Day12(string file) : base(file, "\n") => positions = Input
		.Select(l => Regex.Match(l, @"<x=(?<x>-?\d+), y=(?<y>-?\d+), z=(?<z>-?\d+)>"))
		.Select(m => m.AsVector3( )).ToList( );

	public override async Task<string> SolvePart1()
	{
		var step = 0;

		while (step++ < Steps)
		{
			pairs.ForEach(ApplyGravity);
			ids.ForEach(ApplyVelocity);
		}

		var potential = positions.Select(p => Math.Abs(p.X) + Math.Abs(p.Y) + Math.Abs(p.Z));
		var kinetic = velocities.Select(p => Math.Abs(p.X) + Math.Abs(p.Y) + Math.Abs(p.Z));

		return potential.Zip(kinetic, (p, k) => p * k).Sum( ).ToString( );
	}

	public override async Task<string> SolvePart2()
	{
		var states = new Dictionary<State, long>( );
		var cycles = new Dictionary<Axis, long>( );
		var step = 0L;

		while (cycles.Count < 3)
		{
			Enum.GetValues<Axis>( ).ForEach(axis =>
			{
				var state = CreateState(axis);

				if (states.TryAdd(state, step))
					return;

				cycles.TryAdd(axis, step - states[state]);
			});

			pairs.ForEach(ApplyGravity);
			ids.ForEach(ApplyVelocity);

			step++;
		}

		return LeastCommonMultiple(cycles.Values).ToString( );
	}

	private State CreateState(Axis axis)
	{
		var v = ids.Select(i => axis switch
		{
			Axis.X => new Vector2(positions[i].X, velocities[i].X),
			Axis.Y => new Vector2(positions[i].Y, velocities[i].Y),
			Axis.Z => new Vector2(positions[i].Z, velocities[i].Z),
		}).ToList( );
		return new(axis, v[0], v[1], v[2], v[3]);
	}

	private record struct State(Axis Axis, Vector2 P1, Vector2 P2, Vector2 P3, Vector2 P4);

	private void ApplyVelocity(int idx) =>
		positions[idx] = Vector3.Add(positions[idx], velocities[idx]);

	private void ApplyGravity((int l, int r) pair)
	{
		var (vl, vr) = GravityPull(pair);
		velocities[pair.l] = Vector3.Add(velocities[pair.l], vl);
		velocities[pair.r] = Vector3.Add(velocities[pair.r], vr);
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
