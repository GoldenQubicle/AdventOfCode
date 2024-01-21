using System.Drawing;

namespace AoC2021;

public class Day18 : Solution
{
	public Day18(string file) : base(file, split: "\n")
	{

	}

	public Day18(List<string> input) : base(input) { }

	public override async Task<string> SolvePart1()
	{
		var final = Reduce(Input);

		return string.Empty;
	}

	public override async Task<string> SolvePart2()
	{
		return string.Empty;
	}

	
	public static string Add(string l, string r) => $"[{l},{r}]";

	public static bool TrySplit(string n, out string sn)
	{
		sn = n;
		if (HasValueLargerThan10(n, out var idx))
		{
			var pair = Split(n[idx..(idx + 2)]);
			sn = n.ReplaceAt(idx, pair, 2);
			return true;
		}

		return false;
	}

	private static bool HasValueLargerThan10(string n, out int idx)
	{
		var m = Regex.Match(n, @"\d{2}");
		idx = m.Index;

		return m.Success;
	}

	public static string Split(string n)
	{
		var value = n.ToInt( );
		if (value < 10)
			throw new ArgumentException($"Number {n} is less than 10!");

		var x = (int)Math.Floor(value / 2f);
		var y = (int)Math.Ceiling(value / 2f);

		return $"[{x},{y}]";
	}

	public static bool TryExplode(string n, out string sn)
	{
		sn = n;

		if (HasExplodingPair(n, out var idx))
		{
			var hasLeft = Regex.Matches(n[..idx], @"\d+");
			var hasRight = Regex.Matches(n[(idx + 5)..], @"\d+");
			var pair = GetValue(n[idx..(idx + 5)]);
			var offsetRight = 0;
			
			if (hasLeft.Any( ))
			{
				var left = hasLeft.Last( );
				var nl = (left.Value.ToInt( ) + pair.x).ToString( );
				n = n.ReplaceAt(left.Index, nl, left.Value.Length);
				offsetRight = nl.Length - 1;
			}

			if (hasRight.Any())
			{
				var right = hasRight.First();
				var nl = (right.Value.ToInt() + pair.y).ToString();
				n = n.ReplaceAt(right.Index + idx + 5 + offsetRight, nl, right.Value.Length);
			}

			sn = n.ReplaceAt(idx + offsetRight, "0", 5);

			return true;
		}

		return false;
	}


	private static (int x, int y) GetValue(string pair)
	{
		var isValidPair = pair.StartsWith('[') &&
						  pair.EndsWith(']') &&
						  pair.Count(c => c == '[') == 1 &&
						  pair.Count(c => c == ']') == 1;

		if (!isValidPair)
			throw new ArgumentException($"{pair} is not a valid pair!");

		var parts = pair.Trim('[', ']').Split(',');
		return (parts[0].ToInt( ), parts[1].ToInt( ));
	}



	private static bool HasExplodingPair(string n, out int idx)
	{
		var nesting = 0;
		idx = 0;
		while (idx < n.Length)
		{
			if (n[idx] == '[')
				nesting++;

			if (n[idx] == ']')
				nesting--;

			if (nesting > 4)
				return true;
			idx++;
		}

		return false;
	}

	public static int CalculateMagnitude(string n)
	{
		while (true)
		{
			var match = Regex.Match(n, @"\[\d+,\d+\]");
			if (!match.Success)
				break;

			var pair = GetValue(match.Value);
			var mag = 3 * pair.x + 2 * pair.y;
			n = n.ReplaceAt(match.Index, mag.ToString( ), match.Value.Length);
		}

		return n.ToInt( );
	}

	public static string Reduce(List<string> input)
	{
		var idx = 0;
		var current = input[idx];

		while (idx < input.Count - 1)
		{
			idx++;
			current = Add(current, input[idx]);

			while (HasExplodingPair(current, out _) || HasValueLargerThan10(current, out _))
			{
				if (TryExplode(current, out var sn))
				{
					current = sn;
					continue;
				}

				if (TrySplit(current, out var snn))
				{
					current = snn;
				}
			}

		}


		return current;
	}
}
