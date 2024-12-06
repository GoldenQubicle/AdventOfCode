namespace AoC2024;

public class Day05 : Solution
{
	private readonly Dictionary<int, (List<int> before, List<int> after)> rules = new( );
	private readonly List<List<int>> updates = new( );
	public Day05(string file) : base(file)
	{
		//booo... this is all very sucky
		foreach (var line in Input)
		{
			if (string.IsNullOrEmpty(line))
				continue;

			if (line.Contains('|'))
			{
				var pairs = line.Split('|', StringSplitOptions.TrimEntries).Select(int.Parse).ToList( );

				if (!rules.TryAdd(pairs[0], (new( ), new( ) { pairs[1] })))
					rules[pairs[0]].after.Add(pairs[1]);

				continue;
			}

			updates.Add(line.Split(',', StringSplitOptions.TrimEntries).Select(int.Parse).ToList( ));

		}
		
		var toBeAdded = new List<(int, int)>( );
		rules.ForEach(rule =>
		{
			rule.Value.after.ForEach(r =>
			{
				if (rules.TryGetValue(r, out var rule1))
					rule1.before.Add(rule.Key);
				else
					toBeAdded.Add((r, rule.Key));
			});
		});

		
		toBeAdded.ForEach(r =>
		{
			if (!rules.TryAdd(r.Item1, (new( ) { r.Item2 }, new( ))))
			{
				rules[r.Item1].before.Add(r.Item2);
			}
		});
	}

	
	public override async Task<string> SolvePart1() => updates
		.Aggregate(0, (sum, pages) => IsValidUpdate(pages) ? sum + pages[pages.Count / 2] : sum).ToString( );


	public override async Task<string> SolvePart2() => updates
		.Aggregate(0, (sum, pages) => !IsValidUpdate(pages) ? sum + Order(pages) : sum).ToString( );

	
	private int Order(List<int> pages)
	{
		pages = Reorder(pages);

		while (!IsValidUpdate(pages))
			pages = Reorder(pages);

		return pages[pages.Count / 2];
	}


	private List<int> Reorder(List<int> pages)
	{
		var invalid = pages.WithIndex( ).First(p => !IsValid(pages, p.Value, p.idx));

		var swap = !IsBeforeValid(pages, invalid.Value, invalid.idx)
			? pages[..invalid.idx].First(p => rules[invalid.Value].after.Contains(p))
			: pages[invalid.idx..].First(p => rules[invalid.Value].before.Contains(p));

		var idx = pages.IndexOf(swap);
		pages[idx] = invalid.Value;
		pages[invalid.idx] = swap;

		return pages;
	}
	
	private bool IsValidUpdate(List<int> pages) =>
		pages.WithIndex( ).All(p => IsValid(pages, p.Value, p.idx));


	private bool IsValid(List<int> pages, int page, int idx) =>
		IsBeforeValid(pages, page, idx) && IsAfterValid(pages, page, idx);


	private bool IsBeforeValid(List<int> pages, int page, int idx) =>
		rules[page].after.All(a => !pages[..idx].Contains(a)); //what should come after does not appear before idx


	private bool IsAfterValid(List<int> pages, int page, int idx) =>
		rules[page].before.All(a => !pages[(idx + 1)..].Contains(a));//what should come before does not appear after idx

}
