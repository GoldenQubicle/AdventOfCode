using Common;

namespace AoC2018
{
	public class Day09 : Solution
	{
		public static int Players { get; set; } = 412;
		public static int LastMarble { get; set; } = 71646;

		public Day09() {}

		public Day09(string file) : base(file){}

		public override string SolvePart1() => DoPlayMarbles();

		public override string SolvePart2() => DoPlayMarbles(isPartTwo: true);

		private static string DoPlayMarbles(bool isPartTwo = false)
		{
			var cl = new CircularList<int>();
			var playerScores = new Dictionary<int, int>();
			var marble = 0;
			var lastMarble = isPartTwo ? 100 * LastMarble : LastMarble;
			cl.Add(marble);

			while (marble <= lastMarble)
			{
				//Console.WriteLine(cl);

				marble++;

				if (marble % 23 == 0)
				{
					cl.MoveLeft(7);

					var score = marble + cl.Current;
					var player = marble % Players;

					if (!playerScores.ContainsKey(player))
						playerScores.Add(player, score);
					else
						playerScores[player] += score;

					cl.Remove(cl.Current);

					continue;
				}

				cl.MoveRight(2);
				cl.Insert(marble, false);
				
			}

			return playerScores.Values.Max().ToString();
		}
		
	}
}