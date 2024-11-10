namespace AoC2023;

public class Day12 : Solution
{
	private readonly List<(string row, List<int> records)> springs;

	public Day12(string file) : base(file)
	{
		springs = Input.Select(l =>
		{
			var parts = l.Split( );
			return (row: parts[0], records: parts[1].Split(',', StringSplitOptions.TrimEntries).Select(int.Parse).ToList( ));
		}).ToList( );
	}

	public override async Task<string> SolvePart1()
	{
		return springs.Select(s => GetArrangements(s.row, s.records)).Sum( ).ToString( );
		return string.Empty;
	} 
	

	private static string Replace(string row, int idx, char c)
	{
		var t = row.Remove(idx, 1);
		return t.Insert(idx, c.ToString());
	}

	public static int GetArrangements(string row, List<int> record)
	{
		var result = new List<string>( );
		var queue = new Queue<string>( );
		queue.Enqueue(row);

		while (queue.TryDequeue(out var current))
		{
			var idx = current.IndexOf('?');
			if (idx == -1)
			{
				result.Add(current);
				continue;
			}

			queue.Enqueue(Replace(current, idx, '.'));
			queue.Enqueue(Replace(current, idx, '#'));
		}

		var count = result
			.Select(r => Regex.Matches(r, "(#+)"))
			.Count(m => m.Count == record.Count && record.WithIndex( ).All(r => m[r.idx].Length == r.Value));

		return count;

	}

	public override async Task<string> SolvePart2()
	{
		return string.Empty;
	}
}
