using Combinatorics.Collections;

namespace AoC2024;

public class Day21 : Solution
{
	private readonly List<string> codes;

	private readonly Dictionary<char, (int x, int y)> dirPad = new( )
	{
		{ '-', (0, 0) }, { '^', (1, 0) }, { 'A', (2, 0) },
		{ '<', (0, 1) }, { 'v', (1, 1) }, { '>', (2, 1) },
	};

	private readonly Dictionary<char, (int x, int y)> numPad = new( )
	{
		{ '7', (0,0) }, { '8', (1,0) }, { '9', (2,0) },
		{ '4', (0,1) }, { '5', (1,1) }, { '6', (2,1) },
		{ '1', (0,2) }, { '2', (1,2) }, { '3', (2,2) },
		{ '-', (0,3) }, { '0', (1,3) }, { 'A', (2,3) }, };

	private readonly Dictionary<(char from, char to, int level), long> cache = new( );

	public Day21(string file) : base(file) => codes = Input;

	public override async Task<string> SolvePart1() =>
		codes.Sum(c => c.ToInt( ) * DoKeyPads("A" + c, 2)).ToString( );

	public override async Task<string> SolvePart2() =>
		codes.Sum(c => c.ToInt( ) * DoKeyPads("A" + c, 25)).ToString( );


	private long DoKeyPads(string input, int maxLevel, int level = 0)
	{
		var count = 0L;
		while (true)
		{
			var from = input[0];
			var to = input[1];
			var key = (from, to, level);

			if (cache.TryGetValue(key, out var minCount))
				count += minCount;
			else
			{
				var moveSet = GenerateMoveSet(from, to, level); 
				var counts = level == maxLevel - 1 
					? moveSet.Select(m => GetMoveSetCount("A" + m, level + 1)).ToList( )
					: moveSet.Select(m => DoKeyPads("A" + m, maxLevel, level + 1)).ToList( );
				cache.Add(key, counts.Min( ));
				count += cache[key];
			}

			if (input.Length == 2)
				return count;

			input = input[1..];
		}
	}

	
	private long GetMoveSetCount(string input, int level)
	{
		var count = 0L;
		while (true)
		{
			var from = input[0];
			var to = input[1];
			var key = (from, to, level);

			if (cache.TryGetValue(key, out var total))
				count += total;
			else
			{
				var moveSet = GenerateMoveSet(from, to, level);
				cache.Add(key, moveSet.Min(s => s.Length));
				count += cache[key];
			}

			if (input.Length == 2)
				return count;

			input = input[1..];
		}
	}


	//Generate the full move sets for either numeric or direction pad
	//e.g. A->2 results in <^A and ^<A
	private List<string> GenerateMoveSet(char from, char to, int level)
	{
		var map = level == 0 ? numPad : dirPad;
		var gap = map['-'];
		var (x, y) = map[to].Subtract(map[from]);

		var perm = new List<(int, int)>( );

		for (var i = 0 ;i < Math.Abs(x) ;i++)
			perm.Add(x < 0 ? (-1, 0) : (1, 0));
		
		for (var j = 0 ;j < Math.Abs(y) ;j++)
			perm.Add(y < 0 ? (0, -1) : (0, 1));

		var moveSets = new Permutations<(int, int)>(perm).ToList( );
		
		var results = new List<string>( );
		foreach (var set in moveSets)
		{
			var pos = map[from];
			var result = string.Empty;
			foreach (var move in set)
			{
				pos = pos.Add(move);
				if (pos == gap)
					break;
				result += GetDirection(move);
			}
			if (pos == gap)
				continue;
			result += 'A';
			results.Add(result);
		}

		return results;
	}

	internal static char GetDirection((int, int) dir) => dir switch
	{
		(1, 0) => '>',
		(-1, 0) => '<',
		(0, 1) => 'v',
		(0, -1) => '^',
	};
}
