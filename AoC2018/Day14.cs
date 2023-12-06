using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common;
using Common.Extensions;
using Microsoft.VisualBasic;

namespace AoC2018
{
	public class Day14 : Solution
	{
		public static int Make { get; set; }
		public static List<int> LookFor { get; set; } = new( ) { 8, 6, 4, 8, 0, 1 };

		private List<int> board = new( ) { 3, 7 };

		public Day14(string file) : base(file) => Make = int.Parse(Input[0]);



		public override string SolvePart1()
		{
			var elf1 = 0;
			var elf2 = 1;

			while (board.Count < Make + 10)
			{
				var e1 = board[elf1];
				var e2 = board[elf2];
				var sum = e1 + e2;

				if (sum < 10)
				{
					board.Add(sum);
				}
				else
				{
					board.Add(1);
					board.Add(sum - 10);
				}

				elf1 = (elf1 + e1 + 1) % board.Count;
				elf2 = (elf2 + e2 + 1) % board.Count;

			}

			return board.Skip(Make).Take(10).Aggregate(new StringBuilder( ), (builder, i) => builder.Append(i)).ToString( );
		}

		public override string SolvePart2()
		{
			var elf1 = 0;
			var elf2 = 1;
			var i = 0;

			while (true)
			{
				var e1 = board[elf1];
				var e2 = board[elf2];
				var sum = e1 + e2;

				if (sum < 10)
				{
					board.Add(sum);
				}
				else
				{
					board.Add(1);
					board.Add(sum - 10);
				}

				elf1 = (elf1 + e1 + 1) % board.Count;
				elf2 = (elf2 + e2 + 1) % board.Count;

				var last = board
					.Skip(i)
					.Take(LookFor.Count)
					.Select((n, idx) => (n, idx)).ToList();

				if (last.All(t => LookFor[t.idx] == t.n))
				{
					return i.ToString( );
				}

				i++;
			}
			return string.Empty;
		}
	}
}