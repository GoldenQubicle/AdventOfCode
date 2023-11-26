using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common;
using Common.Extensions;

namespace AoC2018
{
	public class Day02 : Solution
	{
		public Day02(string file) : base(file) { }

		public Day02(List<string> input) : base(input) { }

		public override string SolvePart1()
		{
			var counts = Input.Select(s =>
			{
				var counts = s.GroupBy(c => c);
				return (counts.Any(g => g.Count( ) == 2), counts.Any(g => g.Count( ) == 3));
			}).Aggregate((0, 0), (count, t) => count.Add((t.Item1 ? 1 : 0, t.Item2 ? 1 : 0)));
			return (counts.Item1 * counts.Item2).ToString( );
		}


		public override string SolvePart2()
		{
			foreach (var s1 in Input)
			{
				foreach (var s2 in Input)
				{
					var d1 = s1.Except(s2).ToList( );
					if (d1.Count != 1)
						continue;

					var idx = s1.IndexOf(d1.First( ));
					var r1 = s1.Remove(idx, 1);
					var r2 = s2.Remove(idx, 1);

					if (r1 == r2)
						return r1;
				}
			}

			return string.Empty;
		}
	}
}