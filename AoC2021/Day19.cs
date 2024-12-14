using System.Collections.Generic;

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


			so... the distance pairing itself is NOT sufficient because I treat all beacons with the same distance into the same bin
			that is, I do not take the orientation into consideration so...
			what if instead, for each scanner pair, I compute the delta vectors by applying every combination of +/-xyz for every component
			when there's 12 or more of the same delta's it must mean it is the same beacons...?

			x, y, z
			

		 */


		foreach (var (s1, s2) in GetIndexPairs(scanners.Count))
		{
			//var deltas = scanners[s1]
			//	.SelectMany(v1 => scanners[s2].Select(v2 => Vector3.Subtract(v1, Flip(0, Swizzle(0, v2)) )))
			//	.GroupBy(v => v)
			//	.OrderByDescending(g => g.Count( ));

			for (var s = 0 ;s < 6 ;s++)
			{
				for (var f = 0 ;f < 8 ;f++)
				{
					var deltas = scanners[s1]
						.SelectMany(v1 => scanners[s2].Select(v2 => Vector3.Subtract(v1, Flip(f, Swizzle(s, v2)))))
						.GroupBy(v => v)
						.OrderByDescending(g => g.Count( ));

					if (deltas.First( ).Count( ) >= 12)
					{
						var t = "";
					}
				}
			}
		}

		return string.Empty;
	}

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


	//	var distances = new  Dictionary<int, List<DistancePairs>>( );


	//	foreach (var scanner in scanners)
	//	{
	//		distances.Add(scanner.Key, new());
	//		foreach (var pair in GetIndexPairs(scanner.Value.Count))
	//		{
	//			var distance = Vector3.Distance(scanner.Value[pair.p1], scanner.Value[pair.p2]);
	//			distances[scanner.Key].Add(new(scanner.Key, pair.p1, pair.p2, distance));
	//		}
	//	}


	//	// so basically, the first eleven matches have idx 0. the question is what does that correspond to in the other pair
	//	//for each scanner pairing..
	//	var beaconMapping = scanners
	//		.ToDictionary(s => s.Key, s => s.Value.WithIndex().ToDictionary(t => t.idx, _ => default(Beacon)));
	//	var bcount = 0;

	//	foreach (var pair in GetIndexPairs(scanners.Count))
	//	{
	//		//for each pair of scanner, see which beacons pairs have the same distance between them
	//		//if there are >= 66 of such same distance pairs it means we have at least 12 beacons overlapping
	//		var equal = distances[pair.p1]
	//			.Select(d1 => (s1: d1, s2: distances[pair.p2].FirstOrDefault(d2 => d1.Distance == d2.Distance)))
	//			.Where(d => d.s2 != default).ToList( );

	//		//get the distinct beacon indices for scanner 1
	//		var beaconCount1 = equal.Select(s => s.s1.Idx1).Concat(equal.Select(s => s.s1.Idx2)).ToHashSet( ).Count;
	//		var beaconCount2 = equal.Select(s => s.s2.Idx1).Concat(equal.Select(s => s.s2.Idx2)).ToHashSet( ).Count;

	//		//if (beaconCount1 < 12 && beaconCount2 < 12 ) continue;


	//		//take the beacon from the first distance pair from the first scanner in the pair
	//		//due to the pairing it will occur as the 1st index of the distance pairs beaconCount times
	//		//var beaconIdxS1 = equal[0].s1.Idx1; 
	//		//var beaconIdxS2 = equal.Take(11) //now figure out from the distance pairs of scanner 2 which index also occurs beaconCount times 
	//		//	.Select(s => s.s2.Idx1).Concat(equal.Take(11).Select(s => s.s2.Idx2))
	//		//	.GroupBy(idx => idx).First(g => g.Count() == 11 ).Key;

	//		////so now I finally figured out the first beacon mapping...
	//		////next... go over equals and work out which beacon index maps to which other index
	//		//var beaconPairs = equal
	//		//	.Take(11)
	//		//	.Select(d => (idxs1: d.s1.Idx2,idxs2: d.s2.Idx1 == beaconIdxS2 ? d.s2.Idx2 : d.s2.Idx1)).ToList();
	//		//beaconPairs.Add((beaconIdxS1, beaconIdxS2));

	//		//ok so...this pairing up on distance doesn't quite work because... the input is sneaky of course
	//		//there are groups in scanner 1 which cannot establish the idx for scanner 2, even with 12 unique ids for scanner 1 AND 66 matching pairs
	//		//the questions is, how to handle this... 

	//		var t1 = equal
	//			.GroupBy(d => d.s1.Idx1)
	//			.Where(g => g.Count() > 1)
	//			.Select(g => (idx1: g.Key,
	//				idx2: g.Select(d => d.s2.Idx1).Concat(g.Select(d => d.s2.Idx2))
	//					.GroupBy(idx => idx)
	//					.OrderBy(g2 => g2.Count()).Last().Key)).ToList();

	//		var t2 = equal
	//			.GroupBy(d => d.s1.Idx2)
	//			.Where(g => g.Count( ) > 1)
	//			.Select(g => (idx1: g.Key,
	//				idx2: g.Select(d => d.s2.Idx1).Concat(g.Select(d => d.s2.Idx2))
	//					.GroupBy(idx => idx)
	//					.OrderBy(g2 => g2.Count( )).Last( ).Key)).ToList( );

	//		var t3 = equal
	//			.GroupBy(d => d.s2.Idx1)
	//			.Where(g => g.Count( ) > 1)
	//			.Select(g => (idx1: g.Select(d => d.s1.Idx1).Concat(g.Select(d => d.s1.Idx2))
	//					.GroupBy(idx => idx)
	//					.OrderBy(g2 => g2.Count( )).Last( ).Key,
	//				idx2: g.Key)).ToList( );

	//		var t4 = equal
	//			.GroupBy(d => d.s2.Idx2)
	//			.Where(g => g.Count( ) > 1)
	//			.Select(g => (idx1: g.Select(d => d.s1.Idx1).Concat(g.Select(d => d.s1.Idx2))
	//					.GroupBy(idx => idx)
	//					.OrderBy(g2 => g2.Count( )).Last( ).Key,
	//				idx2: g.Key)).ToList( );


	//		var beaconPairs = new HashSet<(int, int)>(t1.Concat(t2).Concat(t3).Concat(t4));

	//		if(beaconPairs.Count < 12) continue;

	//		Console.WriteLine($"scanner pair {pair.p1}:{pair.p2} has {beaconCount1} in common with {equal.Count} pairings");

	//		foreach (var (b1, b2) in beaconPairs)
	//		{

	//			var vb = new Vector3(-scanners[pair.p2][b2].X, scanners[pair.p2][b2].Y, -scanners[pair.p2][b2].Z);

	//			var delta = Vector3.Subtract(scanners[pair.p1][b1], vb);
	//			Console.WriteLine(delta);

	//			if (beaconMapping[pair.p1][b1] == default && beaconMapping[pair.p2][b2] == default)
	//			{
	//				bcount++;
	//				//Console.WriteLine($"Added new beacon for total of {bcount}");
	//				var beacon = new Beacon( );
	//				beacon.Map.Add((pair.p1, b1));
	//				beacon.Map.Add((pair.p2, b2));
	//				beaconMapping[pair.p1][b1] = beacon;
	//				beaconMapping[pair.p2][b2] = beacon;
	//				continue;
	//			}

	//			if (beaconMapping[pair.p1][b1] != default && beaconMapping[pair.p2][b2] == default)
	//			{
	//				var beacon = beaconMapping[pair.p1][b1];
	//				beacon.Map.Add((pair.p2, b2));
	//				beaconMapping[pair.p2][b2] = beacon;
	//				continue;
	//			}

	//			if (beaconMapping[pair.p1][b1] == default && beaconMapping[pair.p2][b2] != default)
	//			{
	//				var beacon = beaconMapping[pair.p2][b2];
	//				beacon.Map.Add((pair.p1, b1));
	//				beaconMapping[pair.p1][b1] = beacon;
	//				continue;
	//			}

	//			if (beaconMapping[pair.p1][b1] != default && beaconMapping[pair.p2][b2] != default)
	//			{
	//				//pretty sure this can never happen...? Just in case a breakpoint here
	//				//yeah no this can most certainly happen and it means these 2 distinct beacons need to merge into one!?
	//				var beacon = new Beacon();
	//				beacon.Map.AddRange(beaconMapping[pair.p1][b1].Map);
	//				beacon.Map.AddRange(beaconMapping[pair.p2][b2].Map);
	//				beaconMapping[pair.p1][b1] = beacon;
	//				beaconMapping[pair.p2][b2] = beacon;
	//			}
	//		}

	//		/*now construct beacon mapping... the problem kinda is I want to have a sparse matrix
	//		 * because not every beacon is contained in the overlapping region
	//		 * s1	s2	s4
	//		 *  1	2	
	//		 *		3	4
	//		 * 3	8	7
	//		 */


	//	}

	//	//hmmm.. missing one beacon in test...
	//	var beacons = beaconMapping.SelectMany(s => s.Value.Select(d => d.Value)).Where(b => b != default).Distinct().ToList();
	//	//606, 607, 597 -- too high
	//	//255 not correct
	//	return (beacons.Count).ToString();
	//}


	private record DistancePairs(int Scanner, int Idx1, int Idx2, float Distance);

	private record Beacon
	{
		public List<(int scanner, int index)> Map { get; } = new( );
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
