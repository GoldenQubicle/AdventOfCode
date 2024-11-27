namespace AoC2023;

public class Day25 : Solution
{
	private Dictionary<string, List<string>> components;

	public Day25(string file) : base(file)
	{

		components = Input.Select(line => line.Split(": "))
			.ToDictionary(p => p[0], p => p[1].Split(' ').ToList());

		var edges = components.SelectMany(kvp => kvp.Value.Select(c => (c, kvp.Key))).ToList();

		foreach (var tuple in edges)
			if (!components.TryAdd(tuple.c, new() { tuple.Key }))
				components[tuple.c].Add(tuple.Key);
	}



	public override async Task<string> SolvePart1()
	{
		//is it a reasonable assumption that the only cycles in the graph are those involving the 3 edges to be removed?!
		// -> no, there's 19 of them...
		var graph = new Graph();

		var edges = components.SelectMany(kvp => kvp.Value.Select(c => (c, kvp.Key)));
		foreach (var edge in edges)
		{
			graph.AddEdge(edge);
		}

		var cycles = graph.GetAllCycles();


		return string.Empty;
	}

	public override async Task<string> SolvePart2()
	{
		return string.Empty;
	}

	class Graph
	{
		private Dictionary<string, List<string>> _adjacencyList;

		public Graph()
		{
			_adjacencyList = new Dictionary<string, List<string>>( );
		}

		public void AddEdge((string u, string v) e)
		{
			if (!_adjacencyList.ContainsKey(e.u))
				_adjacencyList[e.u] = new List<string>( );
			if (!_adjacencyList.ContainsKey(e.v))
				_adjacencyList[e.v] = new List<string>( );

			_adjacencyList[e.u].Add(e.v);
			_adjacencyList[e.v].Add(e.u); // Undirected graph
		}

		public List<List<string>> GetAllCycles()
		{
			var allCycles = new List<List<string>>( );
			var visited = new HashSet<string>( );
			var stack = new List<string>( );

			foreach (var node in _adjacencyList.Keys)
			{
				if (!visited.Contains(node))
				{
					DFS(node, visited, stack, allCycles, parent: null);
				}
			}

			return allCycles;
		}

		private void DFS(
			string current,
			HashSet<string> visited,
			List<string> stack,
			List<List<string>> allCycles,
			string parent)
		{
			visited.Add(current);
			stack.Add(current);

			foreach (var neighbor in _adjacencyList[current])
			{
				if (!visited.Contains(neighbor))
				{
					// Recursive DFS call
					DFS(neighbor, visited, stack, allCycles, current);
				}
				else if (neighbor != parent && stack.Contains(neighbor)) // Cycle detected
				{
					// Extract the cycle path
					var cycle = ExtractCycle(stack, neighbor);
					if (!IsDuplicateCycle(allCycles, cycle))
						allCycles.Add(cycle);
				}
			}

			stack.RemoveAt(stack.Count - 1); // Backtrack
		}

		private List<string> ExtractCycle(List<string> stack, string neighbor)
		{
			var cycle = new List<string>( );
			int index = stack.LastIndexOf(neighbor);

			for (int i = index ;i < stack.Count ;i++)
				cycle.Add(stack[i]);

			cycle.Add(neighbor); // Complete the cycle
			return cycle;
		}

		private bool IsDuplicateCycle(List<List<string>> allCycles, List<string> cycle)
		{
			var cycleSet = new HashSet<string>(cycle);
			foreach (var existingCycle in allCycles)
			{
				if (cycleSet.SetEquals(existingCycle))
					return true;
			}
			return false;
		}

	}



}




