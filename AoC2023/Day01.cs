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
	public class Day01 : Solution
	{
		private readonly Dictionary<int, string> digits = new( )
		{
			{ 1, "one" },
			{ 2, "two" },
			{ 3, "three" },
			{ 4, "four" },
			{ 5, "five" },
			{ 6, "six" },
			{ 7, "seven" },
			{ 8, "eight" },
			{ 9, "nine" },
		};
		public Day01(string file) : base(file) { }

		public Day01(List<string> input) : base(input) { }

		public override string SolvePart1() => Input
			.Select(s => s.Where(char.IsDigit).AsString( ))
			.Select(s => s.Length == 1 ? $"{s[0]}{s[0]}" : $"{s[0]}{s[^1]}")
			.Select(int.Parse)
			.Sum( ).ToString( );

		public override string SolvePart2() => Input.Select(s =>
			{
				var idx = 0;
				var sb = new StringBuilder( );
				while (idx < s.Length)
				{
					var t = s[idx..];
					digits.ForEach(d =>
					{
						if (s[idx..].StartsWith(d.Key.ToString( )) || s[idx..].StartsWith(d.Value))
							sb.Append(d.Key);
					});
					idx++;
				}
				return sb.ToString( );
			})
			.Select(s => s.Where(char.IsDigit).AsString( ))
			.Select(s => s.Length == 1 ? $"{s[0]}{s[0]}" : $"{s[0]}{s[^1]}")
			.Select(int.Parse)
			.Sum( ).ToString( );

	}
}