using System.Data.Common;
using System.Net.Sockets;

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

		for (var s = 0 ;s < Steps ;s++)
		{
			pairs.ForEach(ApplyGravity);
			idx.ForEach(ApplyVelocity);
		}

		var potential = positions.Select(p => Math.Abs(p.X) + Math.Abs(p.Y) + Math.Abs(p.Z));
		var kinetic = velocities.Select(p => Math.Abs(p.X) + Math.Abs(p.Y) + Math.Abs(p.Z));

		return potential.Zip(kinetic, (p, k) => p * k).Sum( ).ToString( );
	}

	public override async Task<string> SolvePart2()
	{
		var idx = new List<int> { 0, 1, 2, 3 };
		var pairs = new Combinations<int>(idx, 2)
			.Select(p => (l: p[0], r: p[1])).ToList( );

		var keys = idx
			.SelectMany(i => Enum.GetValues<Axis>( )
				.SelectMany(a => new List<char> { 'p', 'v' }
					.Select(d => new Key(i, a, d)))).ToList( );
		var states = new Dictionary<State, int>( );
		var cycles = keys.ToDictionary(k => k, _ => new List<int>());
		var step = 0;

		while (cycles.Values.Any(c => c.Count < 1))
		{
			keys.ForEach(k =>
			{
				var state = Create(k);
				if (!states.TryAdd(state, step))
				{
					cycles[k].Add(step - states[state]);
				}
			});

			pairs.ForEach(ApplyGravity);
			idx.ForEach(ApplyVelocity);
		
			step++;
		}

		var t = cycles.Values.Select(s => s.First()).Distinct();
		var x = cycles
			.GroupBy(k => new { k.Key.Axis, k.Key.Id })
			.ToDictionary(g => g.Key, g => g.Select(k => k.Value.First()).ToList())
			.ToDictionary(k => k.Key, k => LeastCommonMultiple(k.Value));

		var lcm = LeastCommonMultiple(t);
		return string.Empty;
	}


	private State Create(Key key) =>
		new(key, (key.Axis, key.Dimension) switch
		{
			(Axis.X, 'p') => positions[key.Id].X,
			(Axis.Y, 'p') => positions[key.Id].Y,
			(Axis.Z, 'p') => positions[key.Id].Z,
			(Axis.X, 'v') => velocities[key.Id].X,
			(Axis.Y, 'v') => velocities[key.Id].Y,
			(Axis.Z, 'v') => velocities[key.Id].Z,
		});

	private record struct Key(int Id, Axis Axis, char Dimension);
	private record struct State(Key Key, float Value);


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
