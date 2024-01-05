using System.Runtime.InteropServices.JavaScript;
using Common;

namespace AoC2018
{
	public class Day09 : Solution
	{
		public static int Players { get; set; } = 412;
		public static int LastMarble { get; set; } = 71646;

		public Day09() { }

		public Day09(string file) : base(file) { }

		public override async Task<string> SolvePart1() => DoPlayMarbles(isPartTwo: false);

		public override async Task<string> SolvePart2() => DoPlayMarbles(isPartTwo: true);

		private static string DoPlayMarbles(bool isPartTwo = false)
		{
			var cl = new CircularList<long>( );
			var players = new long[Players];
			var marble = 0;
			var lastMarble = isPartTwo ? 100 * LastMarble : LastMarble;
			cl.Add(marble);

			while (marble < lastMarble)
			{
				marble++;

				if (marble % 23 == 0)
				{
					cl.MoveLeft(7);

					players[marble % Players] += marble + cl.Current;

					cl.RemoveCurrent( );

					continue;
				}

				cl.MoveRight( );
				cl.Insert(marble);

			}

			return players.Max( ).ToString( );
		}
	}
}