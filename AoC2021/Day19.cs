namespace AoC2021;

public partial class Day19 : Solution
{
	private Dictionary<int, List<Vector3>> scanners = new();

	public Day19(string file) : base(file, split: "\n")
	{
		foreach (var line in Input)
		{
			if (line.StartsWith("---"))
			{
				scanners.Add(scanners.Count, new List<Vector3>( ));
				continue;
			}

			scanners.Last().Value.Add(Regex( ).Match(line).AsVector3( ));
		}
	}

	public Day19(List<string> input) : base(input) { }

	public override async Task<string> SolvePart1()
	{
		/*
		 * basic idea: per scanner calculate the distance between all beacons
		 * then compare those distances across scanners
		 * then assuming no 2 set of 2 beacons are the same distance apart
		 * if 2 scanners overlap the same 12 beacons, they should have the same set of 12 distances
		 * how to put this in code, no idea yet. 
		 */

		return string.Empty;
	}

	public override async Task<string> SolvePart2()
	{
		return string.Empty;
	}

	[GeneratedRegex(@"(?<x>-?\d+),(?<y>-?\d+),(?<z>-?\d+)")]
	private static partial Regex Regex();
}
