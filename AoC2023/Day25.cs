namespace AoC2023;

public class Day25 : Solution
{
	private Dictionary<string, List<string>> components;

	public Day25(string file) : base(file)
	{

		components = Input.Select(line => line.Split(": "))
			.ToDictionary(p => p[0], p => p[1].Split(' ').ToList( ));

		var edges = components.SelectMany(kvp => kvp.Value.Select(c => (c, kvp.Key))).ToList( );

		foreach (var tuple in edges)
			if (!components.TryAdd(tuple.c, [tuple.Key]))
				components[tuple.c].Add(tuple.Key);
	}



	public override async Task<string> SolvePart1()
	{
		var nodes = components.SelectMany(c => c.Value.Select(n => n)).Distinct( ).ToList( );
		//var edges = components.SelectMany(kvp => kvp.Value.Select(c => new Edge(c, kvp.Key))).Distinct().ToList( );
		var g = GetGroup(nodes.First());

	
		return string.Empty;
	}

	//copy/pasta from 2017 day 12
	private HashSet<string> GetGroup(string start)
	{
		var stack = new Stack<(string current, HashSet<string> visisted)> { (start, [ ]) };
		var seen = new HashSet<string>( );

		while (stack.TryPop(out var state))
		{
			var (current, visited) = state;

		

			while (TryGetNext(current, visited, out var options))
			{
				var next = options.First( );
				
				options.Skip(1).ForEach(o => stack.Push((o, [.. visited])));

				current = next;
			}

			seen.AddRange(visited);
		}
		

		return seen;
	}

	private bool TryGetNext(string current, HashSet<string> visited, out List<string> options)
	{
		options = [ ];
		visited.Add(current);

		var o = components[current]
			.Where(p => p != current && !visited.Contains(p)).ToList( );

		if (!o.Any( ))
			return false;

		options = o.ToList( );
		return true;

	}

	private record Edge(string A, string B)
	{
		public virtual bool Equals(Edge? other)
		{
			if (other is null) return false;

			return (other.A == A && other.B == B) || (other.A == B && other.B == A);
		}

		public override int GetHashCode()
		{
			return Math.Abs(A.GetHashCode()) + Math.Abs(B.GetHashCode());
		}
	}

	public override async Task<string> SolvePart2()
	{
		return string.Empty;
	}
}




