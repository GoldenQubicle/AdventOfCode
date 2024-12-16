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

	public Day19(List<string> input) : base(input) { }

	public override async Task<string> SolvePart1()
	{
		var transforms = new List<ScannerTransform>();

		foreach (var (s1, s2) in GetIndexPairs(scanners.Count))
		{
			for (var r = 0; r < 24; r++)
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

		var origin0 = Vector3.Zero;
		var origin1 = Vector3.Add(origin0, Rotate(0, transforms[0].Offset));
		var origin3 = Vector3.Add(origin1, Rotate(transforms[0].Rotation, transforms[2].Offset));
		var origin4 = Vector3.Add(origin1, Rotate(transforms[0].Rotation, transforms[4].Offset));

		//how in the world do we get the proper position for scanner 2 relative to origin?!
		//to get from 0-1-4-2 we apply the rotations for each step
		var origin2 = Vector3.Add(origin4, Rotate(transforms[0].Rotation, Rotate(transforms[4].Rotation, transforms[7].Offset)));

		//build a mapping by going over all scanner transforms
		//s1.from -> s1.to, get all transforms where s1.to == sN.from && sN.to != s1.to
		//temporary hold on to all rotations along the way
		//in the end we should have all scanner positions relative to origin
		//...aaand than what.... ?

		return string.Empty;
	}

	private record ScannerTransform(int From, int To, int Rotation,  Vector3 Offset);

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

	//2024 day 8
	private static IEnumerable<(int p1, int p2)> GetIndexPairs(int count) =>
		Enumerable.Range(0, count - 1)
			.SelectMany(a1 => Enumerable.Range(a1 + 1, count - 1 - a1).Select(a2 => (a1, a2)));
}
