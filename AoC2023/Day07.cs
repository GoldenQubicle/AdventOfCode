using Common;

namespace AoC2023;

public class Day07 : Solution
{
	private readonly List<Card> cards;

	public static Dictionary<char, int> LabelsPart1 = new( )
	{
		{'A',13 }, {'K',12 }, {'Q',11 }, {'J',10 },
		{'T',9  }, {'9',8  }, {'8',7  }, {'7',6  },
		{'6',5  }, {'5',4  }, {'4',3  }, {'3',2  }, {'2',1 },

	};

	public static Dictionary<char, int> LabelsPart2 = new( )
	{
		{'A',13 }, {'K',12 }, {'Q',11 }, {'T',10 },
		{'9',9  }, {'8',8  }, {'7',7  }, {'6',6  },
		{'5',5  }, {'4',4  }, {'3',3  }, {'2',2  }, {'J',1 },
	};

	public Day07(string file) : base(file) => cards = Input
		.Select(l =>
		{
			var parts = l.Split(' ', StringSplitOptions.TrimEntries);
			return new Card(parts[0], long.Parse(parts[1]));
		}).ToList( );

	public override string SolvePart1() => SortCards(LabelsPart1, isPart1: true);

	public override string SolvePart2() => SortCards(LabelsPart2, isPart1: false);

	private string SortCards(Dictionary<char, int> labels, bool isPart1)
	{
		cards.Sort(new Comparer(labels, CompareCardType, isPart1));

		return cards
			.Select((c, idx) => c.Bid * (idx + 1))
			.Sum( ).ToString( );
	}

	public static int CompareCardType(Card c1, Card c2, bool isPart1 = true) => isPart1
		? c1.Type1 < c2.Type1 ? 1 : c1.Type1 > c2.Type1 ? -1 : 0
		: c1.Type2 < c2.Type2 ? 1 : c1.Type2 > c2.Type2 ? -1 : 0;


	public class Comparer(
		Dictionary<char, int> labels,
		Func<Card, Card, bool, int> typeCompare,
		bool isPart1) : IComparer<Card>
	{
		public int Compare(Card c1, Card c2)
		{
			var result = typeCompare(c1, c2, isPart1);
			if (result != 0)
				return result;

			var idx = -1;
			int strength;
			do
			{
				idx++;
				strength = labels[c1.Hand[idx]] > labels[c2.Hand[idx]]
					? 1
					: labels[c1.Hand[idx]] < labels[c2.Hand[idx]]
						? -1
						: 0;

			} while (labels[c1.Hand[idx]] == labels[c2.Hand[idx]] && idx < 4);

			return strength;
		}
	}


	public record Card(string Hand, long Bid)
	{
		public int Type1 => Hand
				.GroupBy(c => c)
				.OrderByDescending(g => g.Count( )).ToList( ) switch
			{
				{ Count: 1 } => 1,
				{ Count: 2 } g when g.First( ).Count( ) == 4 => 2,
				{ Count: 2 } g when g.First( ).Count( ) == 3 => 3,
				{ Count: 3 } g when g.First( ).Count( ) == 3 => 4,
				{ Count: 3 } g when g.First( ).Count( ) == 2 => 5,
				{ Count: 4 } => 6,
				{ Count: 5 } => 7,
			};

		public int Type2
		{
			get
			{
				if (!Hand.Contains('J'))
					return Type1;

				return Hand.Count(c => c == 'J') switch
				{
					5 => 1,
					_ when Hand.GroupBy(c => c).Count( ) == 2 => 1,
					2 or 3 when Hand.GroupBy(c => c).Count( ) == 3 => 2,
					2 when Hand.GroupBy(c => c).Count( ) == 4 => 4,
					1 when Hand.GroupBy(c => c).Count( ) == 5 => 6,
					1 when Hand.GroupBy(c => c).Count( ) == 4 => 4,
					1 when Hand.GroupBy(c => c).Count( ) == 3 =>
						Hand.GroupBy(c => c).OrderByDescending(g => g.Count( )).First( ).Count( ) == 3 ? 2 : 3,
				};
			}
		}
	}
}