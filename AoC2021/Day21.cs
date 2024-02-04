namespace AoC2021;

public class Day21 : Solution
{
	private Player player1;
	private Player player2;

	internal static int WinScorePart1 = 1000;
	internal static int WinScorePart2 = 21;

	public Day21(string file) : base(file, split: "\n") { }

	public override async Task<string> SolvePart1()
	{
		InitializePlayers( );

		var die = Enumerable.Range(1, 100).ToCircularList( );
		var dieRolls = 0;

		while (true)
		{
			if (player1.Move(RollDie( )))
				return (dieRolls * player2.Score).ToString( );

			if (player2.Move(RollDie( )))
				return (dieRolls * player1.Score).ToString( );
		}

		int RollDie()
		{
			dieRolls += 3;
			return die.TakeAt(3).Sum( );
		}
	}

	public override async Task<string> SolvePart2()
	{
		InitializePlayers( );

		//rolling the dice 3 times with 3 possible outcomes per roll results in 27 different universes per player, per turn
		var variations = new Variations<int>(new[ ] { 1, 2, 3 }, 3, GenerateOption.WithRepetition).ToList( );
		// possible moves are 3, 4, 5, 6, 7, 8, 9 with a frequency of 1, 3, 6, 7, 6, 3, 1
		// each of these odds can be rolled per player, per turn. If player wins it means they won in N universes on that turn
		var dieOdds = variations.GroupBy(s => s.Sum( )).ToDictionary(g => g.Key, g => g.Count( ));

		var games = new PriorityQueue<(Player p1, Player p2), long>( ); //no need for prio really, just to carry the universe count along
		var scores = (player1: 0L, player2: 0L);
		games.Enqueue((player1, player2), 1);

		while (games.TryDequeue(out var game, out var universeCount))
		{
			foreach (var roll1 in dieOdds)
			{
				var np1 = game.p1.New( );

				if (np1.Move(roll1.Key, isPart1: false))
				{
					scores.player1 += roll1.Value * universeCount;
					continue;
				}


				foreach (var roll2 in dieOdds)
				{
					var np2 = game.p2.New( );

					if (np2.Move(roll2.Key, isPart1: false))
						scores.player2 += roll1.Value * roll2.Value * universeCount;
					else
						games.Enqueue((np1, np2), roll1.Value * roll2.Value * universeCount);
				}
			}
		}

		return scores.player1 > scores.player2
			? scores.player1.ToString( )
			: scores.player2.ToString( );
	}

	private record struct Player
	{
		public int Idx { get; set; }
		public long Score { get; private set; }

		public bool Move(int moves, bool isPart1 = true)
		{
			Idx += moves % 10;
			Idx = Idx > 10 ? Idx % 10 : Idx;
			Score += Idx;
			return Score >= (isPart1 ? WinScorePart1 : WinScorePart2);
		}

		public readonly Player New() => this with { };
	}

	private void InitializePlayers()
	{
		player1 = new Player { Idx = Input[0].Split(':')[1].ToInt( ) };
		player2 = new Player { Idx = Input[1].Split(':')[1].ToInt( ) };
	}
}
