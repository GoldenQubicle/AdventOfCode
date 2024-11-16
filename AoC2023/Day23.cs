using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AoC2023;

public class Day23 : Solution
{
	private Grid2d grid;
	public Day23(string file) : base(file) => grid = new(Input, diagonalAllowed: false);


	public override async Task<string> SolvePart1() => DoHike( );


	public override async Task<string> SolvePart2()
	{
		//so the basic idea for part 2, rethink the problem in terms of intersections and the distance between them.
		grid.Where(c => c.Character is '>' or 'v').ForEach(c => c.Character = '.');

		var intersections = grid.Where(c => c.Character == '.' && grid.GetNeighbors(c).Count(n => n.Character == '.') > 2).ToList( );
		intersections.ForEach(c => c.Character = '!');

		Console.WriteLine(grid);

		return string.Empty;
		
	}

	private string DoHike()
	{
		var start = grid.GetRow(0).First(c => c.Character == '.');
		var end = grid.GetRow(grid.Height - 1).First(c => c.Character == '.');
		var routes = new List<int>( );
		var states = new Stack<State>( );
		states.Push(new(start, new( )));

		var total = grid.Count(c => c.Character != '#');
		var branches = new Dictionary<INode, int>( );

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

				//things to consider...
				//why do we bother with the current path if its potential length is less than one of its neighbors
				//also why do we bother creating states for the exact same position but with less potential length?
				foreach (var node in options.Skip(1))
				{

					states.Push(new(node, [.. seen]));
				}
			}
		}

		//5206 too low
		//6330 too low
		//6390 too low
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
