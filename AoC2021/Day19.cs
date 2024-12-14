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

		var distances = new  Dictionary<int, List<DistancePairs>>( );


		foreach (var scanner in scanners)
		{
			distances.Add(scanner.Key, new());
			foreach (var pair in GetIndexPairs(scanner.Value.Count))
			{
				var distance = Vector3.Distance(scanner.Value[pair.p1], scanner.Value[pair.p2]);
				distances[scanner.Key].Add(new(scanner.Key, pair.p1, pair.p2, distance));
			}
		}


		// so basically, the first eleven matches have idx 0. the question is what does that correspond to in the other pair
		//for each scanner pairing..
		var beaconMapping = scanners
			.ToDictionary(s => s.Key, s => s.Value.WithIndex().ToDictionary(t => t.idx, _ => default(Beacon)));

		foreach (var pair in GetIndexPairs(scanners.Count))
		{
			//for each pair of scanner, see which beacons pairs have the same distance between them
			//if there are >= 66 of such same distance pairs it means we have at least 12 beacons overlapping
			var equal = distances[pair.p1]
				.Select(d1 => (s1: d1, s2: distances[pair.p2].FirstOrDefault(d2 => d1.Distance == d2.Distance)))
				.Where(d => d.s2 != default).ToList( );

			//get the distinct beacon indices for scanner 1
			var beaconCount = equal.Select(s => s.s1.Idx1).Concat(equal.Select(s => s.s1.Idx2)).ToHashSet( ).Count;
			
			if(beaconCount < 12) continue;

			//take the beacon from the first distance pair from the first scanner in the pair
			//due to the pairing it will occur as the 1st index of the distance pairs beaconCount times
			var beaconIdxS1 = equal[0].s1.Idx1; 
			var beaconIdxS2 = equal.Take(beaconCount-1) //now figure out from the distance pairs of scanner 2 which index also occurs beaconCount times 
				.Select(s => s.s2.Idx1).Concat(equal.Take(beaconCount-1).Select(s => s.s2.Idx2))
				.GroupBy(idx => idx).First(g => g.Count() == beaconCount - 1).Key;

			//so now I finally figured out the first beacon mapping...
			//next... go over equals and work out which beacon index maps to which other index
			//also yeah.. should probably add the scanner id to the distance pairs...

			var beacon = new Beacon(0);
			beacon.Map.Add((pair.p1, beaconIdxS1));
			beacon.Map.Add((pair.p2, beaconIdxS2));

			if (beaconMapping[pair.p1][beaconIdxS1] == default)
			{
				beaconMapping[pair.p1][beaconIdxS1] = beacon;
			}

			
			beaconMapping[pair.p2][beaconIdxS2] = beacon;

			var  t = beaconMapping.SelectMany(s => s.Value.Select(d => d.Value)).Where(b => b != default).Distinct().Count();

			/*now construct beacon mapping... the problem kinda is I want to have a sparse matrix
			 * because not every beacon is contained in the overlapping region
			 * s1	s2	s4
			 *  1	2	
			 *		3	4
			 * 3	8	7
			 */

			Console.WriteLine($"scanner pair {pair.p1}:{pair.p2} has {beaconCount} in common - probably {equal.Count}");
		}




		return string.Empty;
	}

	//what if.. we just add the scanner index also in here... reckon it makes things A LOT easier to work with
	
	private record DistancePairs(int Scanner, int Idx1, int Idx2, float Distance);

	private record Beacon(int Id)
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
