using System.Text;
using Common;

namespace AoC2018
{
	public class Day12 : Solution
	{
		private readonly string state;
		private readonly Dictionary<string, string> rules;

		public Day12(string file) : base(file)
		{
			state = Input.First( ).Split(":").Last( ).Trim( );
			rules = Input.Skip(1)
				.Select(s => s.Split(" => "))
				.ToDictionary(p => p[0], p => p[1]);
		}

		public override async Task<string> SolvePart1()
		{
			var current = state;
			var offsetIdx = 0;
			for (var i = 0 ;i < 20 ;i++)
			{
				var(adjust, offset) = current switch
				{
					var c when c[0] == '#' => ($"..{current}", -2),
					var c when c[..2] == ".#" => ($".{current}", -1),
					var c when c[^1] == '#' => ($"{current}..", 0),
					var c when c[^2..] == "#." => ($"{current}.", 0),
					_ => (current, 0)
				};

				offsetIdx += offset;
				current = adjust;

				var idx = 0;
				var newState = new StringBuilder();
			
				while (idx < current.Length)
				{
					var match = (idx, current.Length) switch
					{
						(0, _) => $"..{current[idx..(idx + 3)]}",
						(1, _) => $".{current[(idx - 1)..(idx + 3)]}",
						var (p, l) when p == l - 2 => $"{current[(idx - 2)..(idx + 2)]}.",
						var (p, l) when p == l - 1 => $"{current[(idx - 2)..(idx + 1)]}..",
						_ => $"{current[(idx - 2)..(idx + 3)]}"
					};

					newState.Append(rules.TryGetValue(match, out var result) ? result : '.');

					idx++;
				}

				current = newState.ToString();
				Console.WriteLine($"{i} {current} {GetSum(current, offsetIdx)}");
			}

			return GetSum(current, offsetIdx).ToString();
		}

		private static int GetSum(string plants, int offsetIdx) =>
			plants.Select((s, idx) => (s, idx: idx + offsetIdx)).Where(p => p.s == '#').Sum(p => p.idx);


		//after 100 generations the sum increases with 5 for each subsequent generation
		//the score at generation 100 is 724
		public override async Task<string> SolvePart2() => (724 + ((50000000000 - 101) * 5)).ToString();


	}
}