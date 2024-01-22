namespace AoC2021;

public class Day18 : Solution
{
	public Day18(string file) : base(file, split: "\n") { }

	public override async Task<string> SolvePart1()
	{
		var final = Reduce(Input);

		return CalculateMagnitude(final).ToString();
	}

	public override async Task<string> SolvePart2()
	{
		var sums = new List<int>();
		for (var i = 0; i < Input.Count; i++)
		{
			for (var j = i + 1; j < Input.Count - 1; j++)
			{
				sums.Add(CalculateMagnitude(ActuallyReduce(Add(Input[i], Input[j]))));
				sums.Add(CalculateMagnitude(ActuallyReduce(Add(Input[j], Input[i]))));
			}
		}
		return sums.Max().ToString();
	}

	
	public static string Add(string l, string r) => $"[{l},{r}]";


	public static bool TrySplit(string n, out string sn)
	{
		sn = n;
		var m = Regex.Match(n, @"\d{2,}");

		if (m.Success)
		{
			var pair = Split(n[m.Index..(m.Index + m.Value.Length)]);
			sn = n.ReplaceAt(m.Index, pair, m.Value.Length);
			return true;
		}
		return false;
	}


	private static bool HasValueLargerThan10(string n, out int idx)
	{
		var m = Regex.Match(n, @"\d{2,}");
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
			var pair = Regex.Match(n[idx..], @"\[\d+,\d+\]").Value;
			
			var hasLeft = Regex.Matches(n[..idx], @"\d+");
			var hasRight = Regex.Matches(n[(idx + pair.Length)..], @"\d+"); 
			var offsetRight = 0;
			
			if (hasLeft.Any( ))
			{
				var left = hasLeft.Last( );
				var nl = (left.Value.ToInt( ) + GetValue(pair).x).ToString( );
				n = n.ReplaceAt(left.Index, nl, left.Value.Length);
				offsetRight = nl.Length - left.Value.Length;
			}

			if (hasRight.Any())
			{
				var right = hasRight.First();
				var nl = (right.Value.ToInt() + GetValue(pair).y).ToString();
				n = n.ReplaceAt(right.Index + idx + pair.Length + offsetRight, nl, right.Value.Length);
			}

			sn = n.ReplaceAt(idx + offsetRight, "0", pair.Length);

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

			current = ActuallyReduce(current);

		}
		return current;
	}


	public static string ActuallyReduce(string current)
	{
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
		return current;
	}
}
