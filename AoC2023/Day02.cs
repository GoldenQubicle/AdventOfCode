using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common;
using Common.Extensions;

namespace AoC2023
{
	public class Day02 : Solution
	{
		private readonly Dictionary<int, List<(int r, int g, int b)>> games;

		public static (int r, int g, int b) Cubes { get; set; } = (12, 13, 14);
		
		public Day02(string file) : base(file) => games = Input
			.Select((l, idx) => (idx, l.Split(':')[1]))
			.ToDictionary(t => t.idx + 1, t => t.Item2.Split(';')
				.Select(s => Regex.Matches(s, @"(?<r>\d+.(?=red))|(?<g>\d+.(?=green))|(?<b>\d+.(?=blue))", RegexOptions.IgnoreCase))
				.Select(m => (r: m.TryGetGroup("r"), g: m.TryGetGroup("g"), b: m.TryGetGroup("b"))).ToList( ));


		public override string SolvePart1() => games
			.Where(g => !g.Value.Any(s => s.r > Cubes.r || s.g > Cubes.g || s.b > Cubes.b))
			.Sum(g => g.Key).ToString();



	 public override string SolvePart2()
		{
			return string.Empty;
		}
	}
}