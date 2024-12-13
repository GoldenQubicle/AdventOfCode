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
		/*
		 * basic idea: per scanner calculate the distance between all beacons
		 * then compare those distances across scanners
		 * then assuming no 2 set of 2 beacons are the same distance apart -> this seems to be correct
		 * in each pairing checked no distance match occured more than 1
		 * however... there's many more than 12 matches
		 * guess per pairing, have to check the 8 configurations of x,y & z layouts..
		 
		 */

		var distances = new Dictionary<int, List<DistancePairs>>( );


		foreach (var scanner in scanners)
		{
			distances.Add(scanner.Key, new( ));

			foreach (var pair in GetIndexPairs(scanner.Value.Count))
			{
				var distance = Vector3.Distance(scanner.Value[pair.p1], scanner.Value[pair.p2]);
				distances[scanner.Key].Add(new(pair.p1, pair.p2, distance));
			}
		}

		// so basically, the first eleven matches have idx 0. the question is what does that correspond to in the other pair
		//for each scanner pairing..

		foreach (var pair in GetIndexPairs(scanners.Count))
		{
			//for each pair of scanner, see which beacons pairs have the same distance between them
			//if there are >= 66 of such same distance pairs it means we have at least 12 beacons overlapping
			var same = distances[pair.p1]
				.Select(d1 => (d1, d2: distances[pair.p2].FirstOrDefault(d2 => d1.Distance == d2.Distance)))
				.Where(d => d.d2 != default).ToList( );

			var beaconCount = same.Select(s => s.d1.Idx1).Concat(same.Select(s => s.d1.Idx2)).ToHashSet( ).Count;
			
			if(beaconCount < 12) continue;

			var beacon = same[0].d1.Idx1;
			var beaconPair = same.Take(beaconCount-1)
				.Select(s => s.d2.Idx1).Concat(same.Take(beaconCount-1).Select(s => s.d2.Idx2))
				.GroupBy(idx => idx).First(g => g.Count() == beaconCount - 1);

			/*now construct beacon mapping... the problem kinda is I want to have a sparse matrix
			 * because not every beacon is contained in the overlapping region
			 * s1	s2	s4
			 *  1	2	
			 *		3	4
			 * 3	8	7
			 */

			Console.WriteLine($"scanner pair {pair.p1}:{pair.p2} has {beaconCount} in common - probably {same.Count}");
		}




		return string.Empty;
	}

	private record DistancePairs(int Idx1, int Idx2, float Distance);

	private record Beacon
	{
		public List<(int scanner,int index)> Map { get; } = new();
	}

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
