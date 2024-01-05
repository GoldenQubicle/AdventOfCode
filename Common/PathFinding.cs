using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Extensions;
using Common.Interfaces;

namespace Common;

public class PathFinding
{
	// Technically not finding any path but closely related and first implementation for visualizing AoC stuff. 
	public static async Task FloodFill((int x, int y) start, IGraph graph, Func<INode, bool> constraint, Func<IEnumerable<INode>, Task> renderAction = null)
	{
		var queue = new Queue<INode>( );
		var visited = new HashSet<INode>( );
		queue.Enqueue(graph[start.x, start.y]);

		while (queue.TryDequeue(out var current))
		{
			visited.Add(current);

			if (renderAction is not null)
				await renderAction(visited);

			queue.EnqueueAll(graph.GetNeighbors(current, n => !queue.Contains(n) && !visited.Contains(n) && constraint(n)));
		}
	}

	delegate bool NeighborConstraint(INode neighbor, INode current);
	delegate bool TargetCondition(INode target, INode current);
	delegate bool Heuristic(INode node1, INode node2);

	public static async Task<IEnumerable<INode>> BreadthFirstSearch(INode start, INode target, IGraph graph,
		Func<INode,INode, bool> constraint,
		Func<INode, INode, bool> targetCondition,
		Func<IEnumerable<INode>, Task> renderAction = null)
	{
		var queue = new Queue<INode>( );
		var visited = new Dictionary<INode, INode>( );
		var current = start;
		queue.Enqueue(current);
		visited.Add(current, default);
		
		while (queue.TryDequeue(out var next))
		{
			current = next;

			if (targetCondition(current, target))
				break;

			graph.GetNeighbors(current, n => !queue.Contains(n) && !visited.ContainsKey(n) && constraint(current, n))
				.ForEach(n =>
				{
					visited.TryAdd(n, current);
					queue.Enqueue(n);
				});
			
			if(renderAction is not null)
				await renderAction(visited.Keys);
		}

		var path = new List<INode>( );
		while (!current.Equals(start))
		{
			path.Add(current);
			current = visited[current];
		}
		path.Add(start);

		if (renderAction is not null)
		{
			Console.WriteLine($"BFS found path with length: {path.Count} and visited {visited.Count} cells");
			for (var i = 1 ;i <= path.Count ;i++)
				await renderAction(path.TakeLast(i));
		}

		return path;
	}

	public static async Task<IEnumerable<INode>> UniformCostSearch(INode start, INode target, IGraph grid,
		Func<INode, INode, bool> constraint,
		Func<INode, INode, bool> targetCondition,
		Func<INode, INode, long> heuristic,
		Func<IEnumerable<INode>, Task> renderAction = null)
	{
		var queue = new PriorityQueue<INode, long>( );
		var visited = new Dictionary<INode, INode>( );
		var costs = new Dictionary<INode, long>( );
		var current = start;
		queue.Enqueue(current, 0);
		visited.Add(current, default);
		costs.Add(current, 0);

		while (queue.TryDequeue(out var next, out var cost))
		{
			current = next;

			if (targetCondition(current, target))
				break;

			foreach (var n in grid.GetNeighbors(current, n => !visited.ContainsKey(n) && constraint(n, current)))
			{
				if (costs.TryGetValue(n, out var value) && cost + n.Value < value)
					costs[n] = cost + n.Value;
				else
					costs.Add(n, n.Value + cost);

				if (queue.UnorderedItems.Contains((n, costs[n])))
					continue;

				queue.Enqueue(n, costs[n] + heuristic(target, n));
				visited.TryAdd(n, current);

				if(renderAction is not null)
					await renderAction(visited.Keys);
			}
		}

		var path = new List<INode>( );
		while (!current.Equals(start))
		{
			path.Add(current);
			current = visited[current];
		}
		path.Add(start);

		if (renderAction is not null)
		{
			Console.WriteLine($"UCS found path with length: {path.Count} and visited {visited.Count} cells");

			for (var i = 1 ;i <= path.Count ;i++)
				await renderAction(path.TakeLast(i));

		}

		return path;
	}
}
