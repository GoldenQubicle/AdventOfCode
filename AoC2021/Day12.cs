namespace AoC2021;

public class Day12 : Solution
{
	private readonly Dictionary<string, List<string>> nodes = new();
	public Day12(string file) : base(file)
	{
		Input.ForEach(l =>
		{
			var parts = l.Split("-");
			if (nodes.ContainsKey(parts[0])) nodes[parts[0]].Add(parts[1]);
			else nodes.Add(parts[0], new List<string> { parts[1] });

			if (nodes.ContainsKey(parts[1])) nodes[parts[1]].Add(parts[0]);
			else nodes.Add(parts[1], new List<string> { parts[0] });
		});
	}

	public override async Task<string> SolvePart1()
	{
		var pathsFound = 0;
		var queue = new Queue<(List<string> path, List<string> visited)>();
		queue.Enqueue((new List<string> { "start" }, new List<string> { "start" }));

		while (queue.Any())
		{
			var current = queue.Dequeue();
			nodes[current.path.Last()].Where(n => !current.visited.Contains(n)).ForEach(n =>
			{
				if (n.Equals("end")) pathsFound++;
				else queue.Enqueue(char.IsLower(n[0])
					? (current.path.Expand(n), current.visited.Expand(n))
					: (current.path.Expand(n), current.visited));
			});
		}

		return pathsFound.ToString();
	}

	public override async Task<string> SolvePart2()
	{
		var paths = new HashSet<string>();
		var queue = new Queue<(List<string> path, List<string> visited, bool seenTwice)>();
		queue.Enqueue((new List<string> { "start" }, new List<string> { "start" }, false));

		while (queue.Any())
		{
			var current = queue.Dequeue();
			nodes[current.path.Last()].Where(n => !current.visited.Contains(n)).ForEach(n =>
			{
				if (n.Equals("end"))
				{
					paths.Add(current.path.Aggregate(new StringBuilder(), (builder, s) => builder.Append(s + " ")).ToString());
					return;
				}

				var isSmallCave = char.IsLower(n[0]);

				switch (isSmallCave)
				{
					case true when !current.path.Contains(n):
						queue.Enqueue((current.path.Expand(n), current.visited, true));
						queue.Enqueue((current.path.Expand(n), current.visited.Expand(n), false));
						break;
					case true:
						queue.Enqueue((current.path.Expand(n), current.visited.Expand(n), true));
						break;
					default:
						queue.Enqueue((current.path.Expand(n), current.visited, current.seenTwice));
						break;
				}

			});
		}
		return paths.Count().ToString();
	}
}