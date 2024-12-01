namespace AoC2017;

public class Day12 : Solution
{
	private readonly Dictionary<string, List<string>> programs;

	public Day12(string file) : base(file) => programs = Input
		.Select(line => line.Split("<->", StringSplitOptions.TrimEntries))
		.ToDictionary(l => l[0], l => l[1].Split(",", StringSplitOptions.TrimEntries).ToList( ));

	public override async Task<string> SolvePart1() => GetGroup("0").Count.ToString( );

	public override async Task<string> SolvePart2()
	{
		var count = 0;
		var start = "0";

		while (programs.Any( ))
		{
			count++;
			var group = GetGroup(start);
			
			foreach (var program in group)
				programs.Remove(program);

			start = programs.Keys.FirstOrDefault();
		}

		return count.ToString();
	}

	private HashSet<string> GetGroup(string start)
	{
		var stack = new Stack<(string current, HashSet<string> visisted)> { (start, new( )) };
		var seen = new HashSet<string>( );

		while (stack.TryPop(out var state))
		{
			var (current, visited) = state;

			while (TryGetNext(current, visited, out var options))
			{
				current = options.First( );

				if (options.Skip(1).Any( ))
				{
					options.Skip(1).ForEach(o => stack.Push((o, new(visited))));
				}
			}
			seen.AddRange(visited);
		}

		return seen;
	}

	private bool TryGetNext(string current, HashSet<string> visited, out List<string> options)
	{
		visited.Add(current);
		options = new( );
		var o = programs[current].Where(p => p != current && !visited.Contains(p));

		if (!o.Any( ))
			return false;

		options = o.ToList( );
		return true;

	}
}
