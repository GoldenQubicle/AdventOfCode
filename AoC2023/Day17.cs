namespace AoC2023;

public class Day17 : Solution
{
	private readonly Grid2d grid;

	public Day17(string file) : base(file) => grid = new Grid2d(Input, diagonalAllowed: false);
    

    public override async Task<string> SolvePart1( )
    {
	    var start = grid[0, 0];
	    var end = grid[grid.Width - 1, grid.Height - 1];

	    var cameFrom = new Dictionary<INode, INode>();
	    var costSoFar = new Dictionary<(int x, int y), long>();
	    var queue = new PriorityQueue<INode, long>();

		queue.Add(start, 0);
		costSoFar.Add(start.Position, 0);
		cameFrom.Add(start, default);

	    while (queue.TryDequeue(out var current, out var priority))
	    {
		    if (current.Position == end.Position)
			    break;

		    var options = grid.GetNeighbors(current, n => !cameFrom.ContainsKey(n));

		    foreach (var next in options)
		    {
			    var newCost = costSoFar[current.Position] + next.Value;

			    if (costSoFar.TryGetValue(next.Position, out var v) && newCost < v)
				    costSoFar[next.Position] = newCost;
			    else
				    costSoFar.Add(next.Position, newCost);

				queue.Enqueue(next, newCost);
				cameFrom.TryAdd(next, current);
		    }
	    }

		var path = new List<INode>( );
		var c = end;
		while (!c.Equals(start))
		{
			path.Add(c);
			c = cameFrom[c];
		}
		path.Add(start);

		path.ForEach(n => n.Character = '#');
		Console.WriteLine(grid);

		return costSoFar[end.Position].ToString();
    }

    public override async Task<string> SolvePart2( )
    {
    	return string.Empty;
    }
}
