using System.Reflection.Emit;
using Combinatorics.Collections;
using Common.Renders;

namespace AoC2024;

public class Day21 : Solution
{
	private readonly List<string> codes;

	private readonly Dictionary<char, (int x, int y)> DirPad = new( )
	{
		{ '-', (0, 0) }, { '^', (1, 0) }, { 'A', (2, 0) },
		{ '<', (0, 1) }, { 'v', (1, 1) }, { '>', (2, 1) },
	};

	private readonly Dictionary<char, (int x, int y)> NumPad = new( )
	{
		{ '7', (0,0) }, { '8', (1,0) }, { '9', (2,0) },
		{ '4', (0,1) }, { '5', (1,1) }, { '6', (2,1) },
		{ '1', (0,2) }, { '2', (1,2) }, { '3', (2,2) },
		{ '-', (0,3) }, { '0', (1,3) }, { 'A', (2,3) }, };

	public Day21(string file) : base(file) => codes = Input;

	public override async Task<string> SolvePart1()
	{
		
		var result = codes.ToDictionary(c => c, c => Recurse([c], 0).Min(s => s.Length));

		
		return result.Sum(kvp => kvp.Key.ToInt() * kvp.Value ).ToString();
	}

	private Dictionary<(char from, char to), long> cache = new( );

	public override async Task<string> SolvePart2()
	{
		foreach (var from in NumPad.Keys)
		{
			if(from == '-') continue;
			foreach (var to in NumPad.Keys)
			{
				if (to == '-' || from == to) continue;
				var moveSet = GetMoveSet(from, to, 0);
				foreach (var set in moveSet)
				{
					var f = 'A';
					var moves = new List<string> { string.Empty };
					foreach (var t in set)
					{
						var result = GetMoveSet(f, t, 1);
						moves = moves.SelectMany(m => result.Select(r => m + r)).ToList( );
						

						f = t;
					}
					moves.ForEach(m => Console.WriteLine($"from {from} to {to} {m.Length} {m}"));
				}
				
			}
		}

		return string.Empty;
	}


	

	private List<string> Recurse(List<string> input, int level)
	{
		while (true)
		{
			if (level == 3) return input;
			var superset = new List<string>();
			foreach (var s in input)
			{
				var from = 'A';
				var moves = new List<string> { string.Empty };

				foreach (var to in s)
				{
					var result = GetMoveSet(from, to, level);
					moves = moves.SelectMany(m => result.Select(r => m + r)).ToList();
					from = to;
				}

				superset.AddRange(moves);
			}

			input = superset;
			level = level + 1;
		}
	}



	private long GetMoveSetCount(char from, char to, int level)
	{
		var key = (from, to);
		if (cache.TryGetValue(key, out var count))
			return count;

		var moveSet = GetMoveSet(from, to, level);
		cache.Add(key, moveSet.Min(s => s.Length));
		return cache[key];
	}

	private List<string> GetMoveSet(char from, char to, int level)
	{
		var map = level == 0 ? NumPad : DirPad;
		var gap = map['-'];
		var delta = map[to].Subtract(map[from]);

		var x = delta.x;
		var y = delta.y;

		var perm = new List<(int, int)>( );

		for (var i = 0 ;i < Math.Abs(x) ;i++)
		{
			perm.Add(x < 0 ? (-1, 0) : (1, 0));
		}

		for (var j = 0 ;j < Math.Abs(y) ;j++)
		{
			perm.Add(y < 0 ? (0, -1) : (0, 1));
		}

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




	//internal class DirectionPad
	//{
	//	private readonly Dictionary<char, (int x, int y)> buttons = new( )
	//	{
	//		{ '^', (1, 0) },
	//		{ 'A', (2, 0) },
	//		{ '<', (0, 1) },
	//		{ 'v', (1, 1) },
	//		{ '>', (2, 1) },
	//	};

	//	private (int x, int y) gap = (0, 0);

	//	private char current = 'A';

	//	public List<string> Press(char button)
	//	{
	//		var pos = buttons[current];
	//		var result = GetMoveSet(buttons[button].Subtract(pos), pos, gap);
	//		current = button;
	//		return result;
	//	}
	//}

	//internal class KeyPad
	//{
	//	private readonly Dictionary<char, (int x, int y)> buttons = new( )
	//	{
	//		{ '7', (0,0) },
	//		{ '8', (1,0) },
	//		{ '9', (2,0) },
	//		{ '4', (0,1) },
	//		{ '5', (1,1) },
	//		{ '6', (2,1) },
	//		{ '1', (0,2) },
	//		{ '2', (1,2) },
	//		{ '3', (2,2) },
	//		{ '0', (1,3) },
	//		{ 'A', (2,3) },
	//	};

	//	private (int x, int y) gap = (0, 3);

	//	private char current = 'A';

	//	public List<string> Press(char button)
	//	{
	//		var pos = buttons[current];
	//		var result = GetMoveSet(buttons[button].Subtract(pos), pos, gap);
	//		current = button;
	//		return result;
	//		//var x = buttons[button].x - buttons[current].x;
	//		//var y = buttons[button].y - buttons[current].y;
	//		//var perm = new List<(int, int)>( );

	//		//for (var i = 0 ;i < Math.Abs(x) ;i++)
	//		//{
	//		//	perm.Add(x < 0 ? (-1, 0) : (1, 0));
	//		//}

	//		//for (var j = 0 ;j < Math.Abs(y) ;j++)
	//		//{
	//		//	perm.Add(y < 0 ? (0, -1) : (0, 1));
	//		//}

	//		//var moveSets = new Permutations<(int, int)>(perm).ToList( );


	//		//var results = new List<string>( );
	//		//foreach (var set in moveSets)
	//		//{
	//		//	var pos = buttons[current];
	//		//	var result = string.Empty;
	//		//	foreach (var move in set)
	//		//	{
	//		//		pos = pos.Add(move);
	//		//		if (pos == gap)
	//		//			break;
	//		//		result += GetDirection(move);
	//		//	}
	//		//	if (pos == gap)
	//		//		continue;
	//		//	result += 'A';
	//		//	results.Add(result);
	//		//}

	//		//current = button;
	//		//return results;
	//	}
	//}



	internal static char GetDirection((int, int) dir) => dir switch
	{
		(1, 0) => '>',
		(-1, 0) => '<',
		(0, 1) => 'v',
		(0, -1) => '^',
	};
}
