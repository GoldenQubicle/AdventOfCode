using Common;
using Common.Extensions;


namespace AoC2023;

public class Day04 : Solution
{
	private readonly Dictionary<int, (List<int> win, List<int> numbers)> cards;

	public Day04(string file) : base(file) => cards = Input
		.Select((l, idx) => (l.Split(':')[1], idx))
		.ToDictionary(t => t.idx + 1, t =>
		{
			var parts = t.Item1.Split('|');
			return (
				parts[0].Split(' ').Select(s => s.Trim( )).Where(s => !string.IsNullOrEmpty(s)).Select(int.Parse).ToList( ),
				parts[1].Split(' ').Select(s => s.Trim( )).Where(s => !string.IsNullOrEmpty(s)).Select(int.Parse).ToList( ));
		});


	public override async Task<string> SolvePart1() => cards.Values
		.Select(c => c.numbers.Intersect(c.win))
		.Select(i => Math.Pow(2, i.Count( )-1))
		.Where(s => s >= 1)
		.Sum( ).ToString( );


	public override async Task<string> SolvePart2()
	{
		var wins = cards.ToDictionary(c => c.Key, c => c.Value.numbers.Intersect(c.Value.win).Count( ));
		var totalCards = 0;
		var queue = new Queue<int>( );

		cards.Keys.ForEach(queue.Enqueue);

		while (queue.Count != 0)
		{
			totalCards++;
			var current = queue.Dequeue( );
			Enumerable.Range(current + 1, wins[current]).ForEach(queue.Enqueue);
		}

		return totalCards.ToString( );
	}
}