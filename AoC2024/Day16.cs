namespace AoC2024;

public class Day16 : Solution
{
	private readonly Grid2d grid;

	public Day16(string file) : base(file) => grid = new(Input, diagonalAllowed: false);

	public override async Task<string> SolvePart1()
	{
		var start = grid.First(c => c.Character is 'S');
		var end = grid.First(c => c.Character is 'E');
		
		var queue = new PriorityQueue<State, long>( );
		var minCost = long.MaxValue;
		var atExit = false;
		var maxDistance = Maths.GetManhattanDistance(start, end);

		queue.Enqueue(new(start, 0, new( )), maxDistance);

		while (queue.TryDequeue(out var state, out _))
		{
			var (current, dir, seen) = state;

			seen.Add(current);

			if (current.Position == end.Position) // we made it out
			{
				minCost = current.Cost < minCost ? current.Cost : minCost;
				continue;
			}

			var options = grid.GetNeighbors(current, n => !seen.Contains(n) && n.Character is '.' or 'E').ToList( );

			if (!options.Any( ))
				continue; // dead end

			//as long as there's only 1 option in the same direction just keep moving
			//could optimize this more to also take direction switches into account
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
				minCost = current.Cost < minCost ? current.Cost : minCost;
				atExit = false;
				continue;
			}
			
			options.ForEach(o =>
			{
				var newDir = GetDirection(current, o);
				var newCost = newDir == dir ? current.Cost + 1 : current.Cost + 1001;

				if (newCost > o.Cost && o.Cost != 0) return;

				o.Cost = newCost;
				queue.Enqueue(new(o, newDir, new(seen)), newCost - maxDistance - Maths.GetManhattanDistance(o, end));
			});
		}

		return minCost.ToString( );
	}

	public override async Task<string> SolvePart2()
	{
		return string.Empty;
	}

	private record State(INode Current, int Direction, HashSet<INode> Seen);

	private (int x, int y) GetDirection(int d) => d switch
	{
		0 => (1, 0), // east
		1 => (0, 1), // south
		2 => (-1, 0), // west
		3 => (0, -1), // north
	};

	private int GetDirection(INode current, INode next) => (current.Position.Subtract(next.Position)) switch
	{
		(-1, _) => 0, // east
		(_, -1) => 1, // south
		(1, _) => 2, // west
		(_, 1) => 3 // north
	};
}