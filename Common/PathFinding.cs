using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Extensions;

namespace Common;

public static class PathFinding
{
	// Technically not finding any path but closely related and first implementation for visualizing AoC stuff. 
	public static async Task FloodFill((int x, int y) start, Grid2d grid, Func<Grid2d.Cell, bool> constraint, Func<IEnumerable<Grid2d.Cell>, Task> renderAction = null)
	{
		var queue = new Queue<Grid2d.Cell>();
		var visited = new HashSet<Grid2d.Cell>();
		queue.Enqueue(grid[start]);

		while (queue.TryDequeue(out var current))
		{
			visited.Add(current);
			
			if(renderAction is not null)
				await renderAction(visited);

			queue.EnqueueAll(grid.GetNeighbors(current, n => !queue.Contains(n) && !visited.Contains(n) && constraint(n)));
		}
	}

	//note we do not always know the target position to begin with!
	public static async Task BreadthFirstSearch((int x, int y) start, (int x, int y) target, 
		Grid2d grid, Func<Grid2d.Cell, bool> constraint, Func<IEnumerable<Grid2d.Cell>, Task> renderAction = null)
	{
		var queue = new Queue<Grid2d.Cell>( );
		var visited = new Dictionary<Grid2d.Cell, Grid2d.Cell>( );
		var current = grid[start];
		queue.Enqueue(current);
		visited.Add(grid[start], new Grid2d.Cell((0,0)) );

		while (queue.TryDequeue(out var next))
		{
			current = next;

			if (current.Position == target)
				break;

			var neighbors = grid.GetNeighbors(current, n => !queue.Contains(n) && !visited.ContainsKey(n) && constraint(n));
			neighbors.ForEach(n => visited.TryAdd(n, current));
			queue.EnqueueAll(neighbors);
		}

		var path = new List<Grid2d.Cell>();
		while (current.Position != start)
		{
			path.Add(current);
			current = visited[current];
		}
		path.Add(grid[start]);

		if (renderAction is not null)
		{
			for (var i = 1; i <= path.Count; i++)
			{
				await renderAction(path.TakeLast(i));
			}
		}
	}
}
