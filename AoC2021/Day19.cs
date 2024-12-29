using System.Diagnostics.Contracts;
using System.Drawing.Text;
using Microsoft.VisualBasic;

namespace AoC2021;

public partial class Day19 : Solution
{
	private readonly Dictionary<int, List<Vector3>> scanners = new( );

	public Day19(string file) : base(file, split: "\n")
	{
		foreach (var line in Input)
		{
			if (line.StartsWith("---"))
			{
				scanners.Add(scanners.Count, new List<Vector3>( ));
				continue;
			}

			scanners.Last( ).Value.Add(Regex( ).Match(line).AsVector3( ));
		}
	}

	public override async Task<string> SolvePart1()
	{
		var transforms = new List<ScannerTransform>( );

		foreach (var (s1, s2) in scanners.GetIndexPairs( ))
		{
			//foreach scanner pair, for each rotation subtract the beacon positions.
			//If we find 12 or more of the same beacon delta for a given pair, it must mean the scanners see the same beacons
			//and consequently, we have the rotation and offset for the given pair of scanners 
			for (var r = 0 ;r < 24 ;r++)
			{
				var deltas1s2 = scanners[s1]
					.SelectMany(v1 => scanners[s2].Select(v2 => Vector3.Subtract(v1, Rotate(r, v2))))
					.GroupBy(v => v)
					.OrderByDescending(g => g.Count( ));

				if (deltas1s2.First( ).Count( ) >= 12)
				{
					var offset = deltas1s2.First( ).First( );
					transforms.Add(new(s1, s2, r, offset));
				}

				var deltas2s1 = scanners[s2]
					.SelectMany(v1 => scanners[s1].Select(v2 => Vector3.Subtract(v1, Rotate(r, v2))))
					.GroupBy(v => v)
					.OrderByDescending(g => g.Count( ));

				if (deltas2s1.First( ).Count( ) >= 12)
				{
					var offset = deltas2s1.First( ).First( );
					transforms.Add(new(s2, s1, r, offset));
				}
			}


		}

		//var origin = Vector3.Zero;
		//var rotation = 0;
		//var origin0 = Vector3.Add(Vector3.Zero, Rotate(rotation, Vector3.Zero));
		//var origin1 = Vector3.Add(origin0, Rotate(0, transforms[0].Offset));
		//var origin3 = Vector3.Add(origin1, Rotate(transforms[0].Rotation, transforms[2].Offset));
		//var origin4 = Vector3.Add(origin1, Rotate(transforms[0].Rotation, transforms[4].Offset));

		////how in the world do we get the proper position for scanner 2 relative to origin?!
		////to get from 0-1-4-2 we apply the rotations for each step
		//var origin2 = Vector3.Add(origin4, Rotate(transforms[0].Rotation, Rotate(transforms[4].Rotation, transforms[7].Offset)));

		/* what needs to happen next; for all scanners with overlap, translate the position and all beacon positions to origin
		 * then per scanner check which beacons are within range
		 *
		 */

		var origins = new Dictionary<int, Vector3> { { 0, Vector3.Zero } };
		var queue = new Queue<(Vector3 origin, List<int> rotations, ScannerTransform)>( );
		transforms.Where(t => t.From == 0).ForEach(t => queue.Enqueue((Vector3.Zero, [0], t)));
		var beacons = new HashSet<Vector3>();
		beacons.AddRange(scanners[0]);

		while (queue.TryDequeue(out var state))
		{
			var (origin, rotations, transform) = state;

			var nextOrigin = Vector3.Add(origin, DoRotations(rotations, transform.Offset));
			origins.TryAdd(transform.To, nextOrigin);

			scanners[transform.To].ForEach(b0 =>
			{
				var br = DoRotations(rotations.Expand(transform.Rotation), b0);
				var b1 = Vector3.Add(nextOrigin, br );
				if (!beacons.Add(b1))
				{
					var t = string.Empty;
				}
			});

			var to = transforms.Where(t => t.From == transform.To && t.To != transform.From && !origins.ContainsKey(t.To)).ToList();
			to.ForEach(t =>
			{
				queue.Enqueue((nextOrigin, rotations.Expand(transform.Rotation), t));
			});
		}

		//var origin = transforms.First(t => t.From == 0);
		//var to = transforms.Where(t => t.From == origin.To && t.To != origin.From).ToList();
		return beacons.Count().ToString();
		return string.Empty;

		Vector3 DoRotations(List<int> rotations, Vector3 vec)
		{
			rotations.Reverse();
			foreach (var r in rotations)
			{
				vec = Rotate(r, vec);
			}

			rotations.Reverse();
			return vec;
		}
	}

	private record ScannerTransform(int From, int To, int Rotation, Vector3 Offset);

	private Vector3 Rotate(int n, Vector3 v) => n switch
	{
		0 => new(v.X, v.Y, v.Z),
		1 => new(v.Z, v.Y, -v.X),
		2 => new(-v.X, v.Y, -v.Z),
		3 => new(-v.Z, v.Y, v.X),
		4 => new(-v.X, -v.Y, v.Z),
		5 => new(-v.Z, -v.Y, -v.X),
		6 => new(v.X, -v.Y, -v.Z),
		7 => new(v.Z, -v.Y, v.X),
		8 => new(v.X, -v.Z, v.Y),
		9 => new(v.Y, -v.Z, -v.X),
		10 => new(-v.X, -v.Z, -v.Y),
		11 => new(-v.Y, -v.Z, v.X),
		12 => new(v.X, v.Z, -v.Y),
		13 => new(-v.Y, v.Z, -v.X),
		14 => new(-v.X, v.Z, v.Y),
		15 => new(v.Y, v.Z, v.X),
		16 => new(v.Z, v.X, v.Y),
		17 => new(v.Y, v.X, -v.Z),
		18 => new(-v.Z, v.X, -v.Y),
		19 => new(-v.Y, v.X, v.Z),
		20 => new(-v.Z, -v.X, v.Y),
		21 => new(v.Y, -v.X, v.Z),
		22 => new(v.Z, -v.X, -v.Y),
		23 => new(-v.Y, -v.X, -v.Z)
	};



	public override async Task<string> SolvePart2()
	{
		return string.Empty;
	}

	[GeneratedRegex(@"(?<x>-?\d+),(?<y>-?\d+),(?<z>-?\d+)")]
	private static partial Regex Regex();

}
