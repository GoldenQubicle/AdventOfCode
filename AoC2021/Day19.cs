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

	public override async Task<string> SolvePart1() => ReconstructScannerRegion(isPart1: true);

	public override async Task<string> SolvePart2() =>  ReconstructScannerRegion(isPart1: false);

	private string ReconstructScannerRegion(bool isPart1)
	{
		var transforms = new List<ScannerTransform>( );

		//foreach scanner pair check for overlap per rotation and if so store the rotation and offset
		foreach (var (s1, s2) in scanners.GetIndexPairs( ))
		{
			for (var r = 0 ;r < 24 ;r++)
			{
				if (HasOverlap(s1, s2, r, out var offset1))
					transforms.Add(new(s1, s2, r, offset1));

				if (HasOverlap(s2, s1, r, out var offset2))
					transforms.Add(new(s2, s1, r, offset2));
			}
		}

		//reconstructing the region from scanner 0 with origin 0,0,0
		var beacons = new HashSet<Vector3> { scanners[0] };
		var origins = new Dictionary<int, Vector3> { { 0, Vector3.Zero } };
		
		var queue = new Queue<(Vector3 origin, List<int> rotations, ScannerTransform)>( );
		transforms.Where(t => t.From == 0).ForEach(t => queue.Enqueue((Vector3.Zero, [0], t)));
		
		while (queue.TryDequeue(out var state))
		{
			var (origin, rotations, transform) = state;

			//calculate the next origin in absolute space
			var nextOrigin = Vector3.Add(origin, DoRotations(rotations, transform.Offset));
			origins.TryAdd(transform.To, nextOrigin);
			
			//transform all beacons for the given scanner into absolute space
			scanners[transform.To].ForEach(b0 =>
			{
				var b1 = Vector3.Add(nextOrigin, DoRotations(rotations.Expand(transform.Rotation), b0));
				beacons.Add(b1);
			});

			//get the next scanners for which we haven't determined the origin in absolute space yet
			var next = transforms.Where(t => t.From == transform.To && t.To != transform.From && !origins.ContainsKey(t.To)).ToList( );
			next.ForEach(t =>
			{
				queue.Enqueue((nextOrigin, rotations.Expand(transform.Rotation), t));
			});
		}

		var distances = new List<long>( );
		foreach (var pair in origins.GetIndexPairs( ))
		{
			distances.Add(Maths.GetManhattanDistance(origins[pair.idx1], origins[pair.idx2]));
		}

		return isPart1 ? beacons.Count.ToString( ) : distances.Max( ).ToString();

		Vector3 DoRotations(List<int> rotations, Vector3 vec)
		{
			rotations.Reverse( );
			foreach (var r in rotations)
			{
				vec = Rotate(r, vec);
			}

			rotations.Reverse( );
			return vec;
		}
	}

	private bool HasOverlap(int s1, int s2, int r, out Vector3 offset)
	{
		//If we find 12 or more of the same beacon delta for a given scanner pair, it must mean the scanners see the same beacons.
		//Consequently we have the rotation and offset for the given pair of scanners.

		offset = Vector3.Zero;

		var delta = scanners[s1]
			.SelectMany(v1 => scanners[s2].Select(v2 => Vector3.Subtract(v1, Rotate(r, v2))))
			.GroupBy(v => v)
			.OrderByDescending(g => g.Count( )).ToList( );

		if (delta.First( ).Count( ) < 12)
			return false;

		offset = delta.First( ).First( );

		return true;
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


	[GeneratedRegex(@"(?<x>-?\d+),(?<y>-?\d+),(?<z>-?\d+)")]
	private static partial Regex Regex();

}
