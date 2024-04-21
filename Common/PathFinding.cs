using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Extensions;
using Common.Interfaces;
using Common.Renders;

namespace Common;

public class PathFinding
{
	//4 21 2024 should never have never hardcoded the rendering stuff in here. 
	//not happy with this api and usage anyway

	// Technically not finding any path but closely related and first implementation for visualizing AoC stuff. 
	public static async Task FloodFill((int x, int y) start, IGraph graph, Func<INode, bool> constraint)
	{
		var queue = new Queue<INode>( );
		var visited = new HashSet<INode>( );
		queue.Enqueue(graph[start.x, start.y]);

		while (queue.TryDequeue(out var current))
		{
			visited.Add(current);

			//if (IRenderState.IsActive)
			//	await IRenderState.Update(new PathFindingRender{ Set = visited});

			queue.EnqueueAll(graph.GetNeighbors(current, n => !queue.Contains(n) && !visited.Contains(n) && constraint(n)));
		}
	}

	public static async Task<IEnumerable<INode>> BreadthFirstSearch(INode start, INode target, IGraph graph,
		Func<INode,INode, bool> constraint,
		Func<INode, INode, bool> targetCondition )
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

			//if (IRenderState.IsActive)
			//	await IRenderState.Update(new PathFindingRender { Set = visited.Keys });
		}

		var path = new List<INode>( );
		while (!current.Equals(start))
		{
			path.Add(current);
			current = visited[current];
		}
		path.Add(start);

		//if (IRenderState.IsActive)
		//{
		//	Console.WriteLine($"BFS found path with length: {path.Count} and visited {visited.Count} cells");
		//	for (var i = 1 ;i <= path.Count ;i++)
		//		await IRenderState.Update(new PathFindingRender{ Set = path.TakeLast(i)});
		//}

		return path;
	}

	public static async Task<(IEnumerable<INode> path, long cost)> UniformCostSearch(INode start, INode target, IGraph grid,
		Func<INode, INode, bool> constraint,
		Func<INode, INode, bool> targetCondition,
		Func<INode, INode, long> heuristic)
	{
		var queue = new PriorityQueue<INode, long>( );
		var visited = new Dictionary<INode, INode>( );
		var costs = new Dictionary<INode, long>( );
		var current = start;
		queue.Enqueue(current, 0);
		visited.Add(current, default);
		costs.Add(current, 0);


		//1-18-2024: note we're not using the heuristic, nor the actual priority value from the queue.
		//Had some trouble properly separating the concepts of distance vs priority value while debugging. 
		while (queue.TryDequeue(out var next, out var cost))
		{
			current = next;

			if (targetCondition(current, target))
				break;

			foreach (var n in grid.GetNeighbors(current, n => !visited.ContainsKey(n) &&  constraint(n, current)))
			{
				var newCost = costs[current] + n.Value;

				if (costs.TryGetValue(n, out var value) && newCost < value)
					costs[n] = newCost;
				else
					costs.Add(n, newCost);

				if (queue.UnorderedItems.Contains((n, newCost)))
					continue;

				queue.Enqueue(n, newCost + heuristic(target, n));

				visited.TryAdd(n, current);

				//if (IRenderState.IsActive)
				//	await IRenderState.Update(new PathFindingRender { Set = visited.Keys });
			}
		}

		var path = new List<INode>( );
		while (!current.Equals(start))
		{
			path.Add(current);
			current = visited[current];
		}
		path.Add(start);

		//if (IRenderState.IsActive)
		//{
		//	Console.WriteLine($"UCS found path with length {path.Count}, total cost of {costs[target]} and visited {visited.Count} cells");

		//	for (var i = 1 ;i <= path.Count ;i++)
		//		await IRenderState.Update(new PathFindingRender{ Set = path.TakeLast(i)});
		//}

		return (path, costs[target]);
	}
}
