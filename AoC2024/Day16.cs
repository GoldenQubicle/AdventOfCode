namespace AoC2024;

public class Day16 : Solution
{
	private readonly Grid2d grid;

	public Day16(string file) : base(file) => grid = new(Input, diagonalAllowed: false);

	public override async Task<string> SolvePart1() => DoReindeerMaze(isPart1: true);

	public override async Task<string> SolvePart2() => DoReindeerMaze(isPart1: false);


	private string DoReindeerMaze(bool isPart1)
	{
		var start = grid.First(c => c.Character is 'S');
		var end = grid.First(c => c.Character is 'E');

		var queue = new PriorityQueue<State, long>( );
		var minCost = long.MaxValue;
		var atExit = false;
		var maxDistance = Maths.GetManhattanDistance(start, end);
		var seats = new HashSet<(int, int)>( );
		var minPathLength = int.MaxValue;

		queue.Enqueue(new(start, 0, new( )), maxDistance);

		while (queue.TryDequeue(out var state, out _))
		{
			var (current, dir, seen) = state;

			seen.Add(current);

			var options = grid.GetNeighbors(current, n => !seen.Contains(n) && n.Character is '.' or 'E').ToList( );

			if (!options.Any( ))
				continue; // dead end

			//as long as there's only 1 option in the same direction just keep moving
			//could optimize this more by also taking direction switches into account
			//however for some reason it doesn't work as expected and cannot bother to find out why
			while (options.Count == 1 && GetDirection(current, options[0]) == dir)
			{
				options[0].Cost = current.Cost + 1;
				current = options[0];
				seen.Add(current);

				if (current.Position == end.Position) // we made it out
				{
					atExit = true;
					break;
				}

				options = grid.GetNeighbors(current, n => !seen.Contains(n) && n.Character is '.' or 'E').ToList( );
			}

			if (atExit)
			{
				//for part 2 we're only interested in the BEST paths
				//annoyingly there are paths with the same cost but MORE cells visited than the best paths
				//consequently we check on cost & path length, and clear the seats if a better minimum is found for either
				if (current.Cost < minCost)
				{
					minCost = current.Cost;
					seats.Clear( );
				}

				if (current.Cost == minCost && seen.Count < minPathLength)
				{
					minPathLength = seen.Count;
					seats.Clear( );
				}

				if (current.Cost == minCost && seen.Count == minPathLength)
					seats.AddRange(seen.Select(c => c.Position));

				atExit = false;
				continue;
			}

			options.ForEach(o =>
			{
				var newDir = GetDirection(current, o);
				var newCost = newDir == dir ? current.Cost + 1 : current.Cost + 1001;

				if (newCost > o.Cost && o.Cost != 0)
					return;

				o.Cost = newCost;
				queue.Enqueue(new(o, newDir, [.. seen]), newCost - maxDistance - Maths.GetManhattanDistance(o, end));
			});
		}

		return isPart1 ? minCost.ToString( ) : seats.Count.ToString( );
	}


	private record State(INode Current, int Direction, HashSet<INode> Seen);


	private int GetDirection(INode current, INode next) => current.Position.Subtract(next.Position) switch
	{
		(1, _) => 0, // east
		(_, -1) => 1, // south
		(-1, _) => 2, // west
		(_, 1) => 3 // north
	};
}
