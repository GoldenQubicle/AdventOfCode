using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Extensions;

namespace Common;

public static class FloodFill
{
	public static async Task Run((int x, int y) start, Grid2d grid, Func<Grid2d.Cell, bool> constraint, Func<IEnumerable<Grid2d.Cell>, Task> renderAction = null)
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
}
