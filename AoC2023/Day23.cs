namespace AoC2023;

public class Day23 : Solution
{
	private readonly Grid2d grid;
	public Day23(string file) : base(file) => grid = new(Input, diagonalAllowed: false);

	public override async Task<string> SolvePart1() => DoHike( );

	public override async Task<string> SolvePart2()
	{
		//so the basic idea for part 2, rethink the problem in terms of intersections and the distance between them and turn it into a graph.
		//cause clearly, even with state pruning, just trying to solve the maze brute force is taking FOREVER

		//replace the slopes with empty tiles
		grid.Where(c => c.Character is '>' or 'v').ForEach(c => c.Character = '.');

		var graph = CreateGraph();

		var start = grid.GetRow(0).First(c => c.Character == '.');
		var end = grid.GetRow(grid.Height - 1).First(c => c.Character == '.');

		var states = new Stack<(INode, List<INode>)> { new(start, new( )) };
		var routes = new List<int>( );

		while (states.TryPop(out var state))
		{
			var (current, seen) = state;

			while (true)
			{
				seen.Add(current);

				if (current == end)
				{
					var length = 0;
					for (var i = 0 ;i < seen.Count - 1 ;i++)
						length += graph[seen[i]][seen[i + 1]];

					routes.Add(length);
					break;
				}

				var options = graph[current].Keys.Where(n => !seen.Contains(n)).ToList( );

				if (!options.Any( ))
					break;

				current = options.First( );

				foreach (var option in options.Skip(1))
					states.Push((option, [.. seen]));
				
			}
		}

		return routes.Max( ).ToString( );

	}

	private Dictionary<INode, Dictionary<INode, int>> CreateGraph()
	{
		//get all the tiles with 3 or 4 connections
		var graph = grid
			.Where(c => c.Character == '.' && grid.GetNeighbors(c).Count(n => n.Character == '.') > 2)
			.ToDictionary(n => n as INode, _ => new Dictionary<INode, int>( ));

		var toCheck = new Queue<INode>( );
		toCheck.EnqueueAll(graph.Keys);

		//for each intersection, explore all paths until the next intersection
		while (toCheck.TryDequeue(out var node))
		{
			var states = new Stack<State> { new(node, new( )) };

			while (states.TryPop(out var state))
			{
				var (current, seen) = state;

				while (true)
				{
					if (graph.ContainsKey(current) && current != node)
					{
						graph[node].Add(current, seen.Count);
						break;
					}

					seen.Add(current);

					var options = grid.GetNeighbors(current, n => n.Character != '#' && !seen.Contains(n)).ToList( );

					//we must be at either the start or end
					if (!options.Any( ))
					{
						graph[node].Add(current, seen.Count - 1);
						graph.TryAdd(current, new( ) { { node, seen.Count - 1 } });
						break;
					}

					current = options.First( );

					if (!options.Skip(1).Any( ))
						continue;

					foreach (var o in options.Skip(1))
						states.Push(new(o, [.. seen]));
				}
			}
		}

		return graph;
	}

	private string DoHike()
	{
		var start = grid.GetRow(0).First(c => c.Character == '.');
		var end = grid.GetRow(grid.Height - 1).First(c => c.Character == '.');
		var routes = new List<int>( );
		var states = new Stack<State> { new(start, new( )) };

		while (states.TryPop(out var state))
		{
			var (current, seen) = state;

			while (true)
			{
				if (current == end)
				{
					routes.Add(seen.Count);
					break;
				}

				seen.Add(current);

				var isSlope = current.Character is '>' or 'v';
				var options = grid.GetNeighbors(current, n => n.Character != '#' && !seen.Contains(n)).ToList( );

				if (isSlope)
					options = options.Where(n => current.Character is '>'
						? n.X == current.X + 1
						: n.Y == current.Y + 1).ToList( );

				if (!options.Any( ))
					break;

				current = options.First( );

				if (!options.Skip(1).Any( ))
					continue;

				foreach (var node in options.Skip(1))
					states.Push(new(node, [.. seen]));
			}
		}


		return routes.Max( ).ToString( );
	}

	private int FloodFill(INode node, HashSet<INode> seen)
	{
		var queue = new Queue<INode>( );
		var visited = new HashSet<INode>( );
		queue.Enqueue(node);

		while (queue.TryDequeue(out var current))
		{
			visited.Add(current);

			queue.EnqueueAll(grid.GetNeighbors(current, n => n.Character != '#' && !queue.Contains(n) && !visited.Contains(n) && !seen.Contains(n)));
		}

		return visited.Count;
	}

	internal record State(INode Current, HashSet<INode> Seen);
}
