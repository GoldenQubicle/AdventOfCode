namespace AoC2024;

public class Day05 : Solution
{
	private Dictionary<int, (List<int> before, List<int> after)> rules = new( );
	private List<List<int>> updates = new( );
	public Day05(string file) : base(file)
	{
		foreach (var line in Input)
		{
			if (string.IsNullOrEmpty(line))
				continue;

			if (line.Contains('|'))
			{
				var pairs = line.Split('|', StringSplitOptions.TrimEntries).Select(int.Parse).ToList( );

				if (!rules.TryAdd(pairs[0], (new( ), new( ) { pairs[1] })))
				{
					rules[pairs[0]].after.Add(pairs[1]);
				}
				continue;
			}

			updates.Add(line.Split(',', StringSplitOptions.TrimEntries).Select(int.Parse).ToList( ));

		}

		var toBeAdded = new List<(int, int)>();
		foreach (var rule in rules)
		{
			rule.Value.after.ForEach(r =>
			{
				if (rules.ContainsKey(r))
				{
					rules[r].before.Add(rule.Key);
				}
				else
				{
					toBeAdded.Add((r, rule.Key));
				}
			});
		}
		toBeAdded.ForEach(r =>
		{
			if (!rules.TryAdd(r.Item1, (new() { r.Item2 }, new())))
			{
				rules[ r.Item1].before.Add(r.Item2);
			}
		});
	}



	public override async Task<string> SolvePart1()
	{
		var result = 0;
		foreach (var pages in updates)
		{
			if (IsValid(pages))
			{
				var mid =  (pages.Count / 2);
				result += pages[mid];
			}
		}

		return result.ToString();
	}

	private bool IsValid(List<int> pages)
	{
		var all = new List<bool>();
		foreach (var (page, idx) in pages.WithIndex())
		{
			//if (idx == 0)
			//{
			//	var valid1 = rules[page].before.All(a => !pages[1..].Contains(a));
			//}

			//if (idx == pages.Count - 1)
			//{
			//	var valid1 = rules[page].after.All(a => !pages[..^1].Contains(a));
			//}

			var before = rules[page].after.All(a => !pages[..idx].Contains(a));
			var after = rules[page].before.All(a => !pages[(idx + 1)..].Contains(a));
			all.Add(before && after);
		}

		return all.All(b => b);
	}

	public override async Task<string> SolvePart2()
	{
		return string.Empty;
	}
}
