using System.Text.RegularExpressions;
using Common;
using Common.Extensions;

namespace AoC2023;

public class Day18 : Solution
{
	private readonly List<(string dir, long d, string hex)> digPlan;

	public Day18(string file) : base(file) => digPlan = Input
			.Select(l => Regex.Match(l, @"(?<dir>L|U|R|D).(?<d>\d+).\(#(?<hex>[0-9a-f]+)"))
			.Select(m => (dir: m.Groups["dir"].Value, d: m.AsLong("d"), hex: m.Groups["hex"].Value)).ToList( );


	public override string SolvePart1() => DigLavaLagoon(digPlan.Select(d => (d.dir, d.d)));


	public override string SolvePart2()
	{
		var dir = new Dictionary<char, string> { { '0', "R" }, { '1', "D" }, { '2', "L" }, { '3', "U" } };
		var steps = digPlan.Select(s => (dir: dir[s.hex[^1]], d: Convert.ToInt64(s.hex[..5], 16))).ToList( );

		return DigLavaLagoon(steps);
	}

	private static string DigLavaLagoon(IEnumerable<(string dir, long d)> steps)
	{
		var length = 0L;
		var coords = new List<(long x, long y)> { };
		var current = (0L, 0L);
		steps.ForEach(s =>
		{
			length += s.d;
			coords.Add(current);
			current = s.dir switch
			{
				"R" => current.Add(s.d, 0),
				"L" => current.Add(-s.d, 0),
				"U" => current.Add(0, -s.d),
				"D" => current.Add(0, s.d)
			};
		});
		coords.Add(coords[0]);
		return (Maths.CalculateAreaShoeLace(coords) + length / 2 + 1).ToString( );
	}
}
