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
			for (var s = 0 ;s < 6 ;s++)
			{
				for (var f = 0 ;f < 8 ;f++)
				{
					var deltas1s2 = scanners[s1]
						.SelectMany(v1 => scanners[s2].Select(v2 => Vector3.Subtract(v1, Flip(f, Swizzle(s, v2)))))
						.GroupBy(v => v)
						.OrderByDescending(g => g.Count( ));

					if (deltas1s2.First( ).Count( ) >= 12)
					{
						var offset = deltas1s2.First().First();
						transforms.Add(new(s1, s2, s, f, offset));
					}

					var deltas2s1= scanners[s2]
						.SelectMany(v1 => scanners[s1].Select(v2 => Vector3.Subtract(v1, Flip(f, Swizzle(s, v2)))))
						.GroupBy(v => v)
						.OrderByDescending(g => g.Count( ));

					if (deltas2s1.First( ).Count( ) >= 12)
					{
						var offset = deltas2s1.First( ).First( );
						transforms.Add(new(s2, s1, s, f, offset));
					}
				}
			}
		}


		var origin3 = Vector3.Add(transforms[0].Offset, Flip(transforms[0].Flip, Swizzle(transforms[0].Swizzle, transforms[2].Offset)));
		var origin4 = Vector3.Add(transforms[0].Offset, Flip(transforms[0].Flip, Swizzle(transforms[0].Swizzle, transforms[4].Offset)));
		var origin2 = Vector3.Add(origin4, Flip(transforms[7].Flip, Swizzle(transforms[7].Swizzle, transforms[7].Offset)));
		return string.Empty;
	}

	private record ScannerTransform(int From, int To, int Swizzle, int Flip, Vector3 Offset);

	private Vector3 Swizzle(int n, Vector3 v) => n switch
	{
		0 => new(v.X, v.Y, v.Z),
		1 => new(v.X, v.Z, v.Y),
		2 => new(v.Y, v.Z, v.X),
		3 => new(v.Y, v.X, v.Z),
		4 => new(v.Z, v.X, v.Y),
		5 => new(v.Z, v.Y, v.X),
	};

	private Vector3 Flip(int n, Vector3 v) => n switch
	{
		0 => Vector3.Multiply(v, new Vector3(1, 1, 1)),
		1 => Vector3.Multiply(v, new Vector3(1, -1, 1)),
		2 => Vector3.Multiply(v, new Vector3(1, 1, -1)),
		3 => Vector3.Multiply(v, new Vector3(1, -1, -1)),
		4 => Vector3.Multiply(v, new Vector3(-1, 1, 1)),
		5 => Vector3.Multiply(v, new Vector3(-1, -1, 1)),
		6 => Vector3.Multiply(v, new Vector3(-1, 1, -1)),
		7 => Vector3.Multiply(v, new Vector3(-1, -1, -1)),
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
