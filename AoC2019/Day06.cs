namespace AoC2019;

public class Day06 : Solution
{
	private readonly Dictionary<string, List<string>> orbits;

	public Day06(string file) : base(file, "\n") => orbits = Input
		.Select(l => l.Split(')'))
		.Aggregate(new Dictionary<string, List<string>>( ), (orbits, pair) =>
		{
			if (orbits.ContainsKey(pair[0]))
				orbits[pair[0]].Add(pair[1]);
			else
				orbits.Add(pair[0], new( ) { pair[1] });

			if (orbits.ContainsKey(pair[1]))
				orbits[pair[1]].Add(pair[0]);
			else
				orbits.Add(pair[1], new( ) { pair[0] });
			return orbits;
		});

	public override async Task<string> SolvePart1() => DoOrbitPlanets("COM").ToString();

	public override async Task<string> SolvePart2() => DoOrbitPlanets("YOU", "SAN").ToString( );
	
	private int DoOrbitPlanets(string start, string target = "")
	{
		var isPart2 = !string.IsNullOrEmpty(target);
		var current = start;
		var path = new Stack<string>( );
		var visited = new HashSet<string>( );
		var count = 0;

		while (true)
		{
			visited.Add(current);

			if (isPart2 && current.Equals(target))
				break;

			if (!orbits.ContainsKey(current)) //reached leaf in graph
			{
				count += path.Count;
				current = path.Pop( );
			}

			var next = orbits[current].FirstOrDefault(p => !visited.Contains(p));

			if (next is not null)
			{
				path.Push(current);
				current = next;
				continue;
			}

			if (path.Count == 0) // returned back to start
				break;

			count += path.Count;
			current = path.Pop( );
		}


		return isPart2 ? path.Count - 2: count;
	}
}
