using System.Text.RegularExpressions;
using Common;
using Common.Extensions;

namespace AoC2023
{
	public class Day02 : Solution
	{
		private readonly Dictionary<int, List<(int r, int g, int b)>> games;

		public Day02(string file) : base(file) => games = Input
			.Select((l, idx) => (idx, l.Split(':')[1]))
			.ToDictionary(t => t.idx + 1, t => t.Item2.Split(';')
				.Select(s => Regex.Matches(s, @"(?<r>\d+.(?=red))|(?<g>\d+.(?=green))|(?<b>\d+.(?=blue))"))
				.Select(m => (r: m.TryGetGroup("r"), g: m.TryGetGroup("g"), b: m.TryGetGroup("b"))).ToList());


		public override string SolvePart1() => games
			.Where(g => !g.Value.Any(s => s.r > 12 || s.g > 13 || s.b > 14))
			.Sum(g => g.Key).ToString();


		public override string SolvePart2() => games
			.Select(g => (g.Value.MaxBy(s => s.r).r, g.Value.MaxBy(s => s.g).g, g.Value.MaxBy(s => s.b).b))
			.Select(g => g.r * g.g * g.b)
			.Sum().ToString();
	}
}