using static System.Net.Mime.MediaTypeNames;

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


			var (p, d) = GetPathDirections(current, start, cameFrom);
		

			var options = grid.GetNeighbors(current, n => !cameFrom.ContainsKey(n));

			if (d.Count >= 3)
			{
				var ud = d.TakeLast(3).Distinct( );
				if (ud.Count() == 1)
				{
					options = options.Where(n => GetDirection(n, current) != ud.First()).ToList();
				}
			}

			foreach (var next in options)
		    {
	
				
			    var newCost = costSoFar[current.Position] + next.Value;

			    if (costSoFar.TryGetValue(next.Position, out var v) && newCost < v)
				    costSoFar[next.Position] = newCost;
			    else
				    costSoFar.Add(next.Position, newCost);

				queue.Enqueue(next, newCost );
				cameFrom.TryAdd(next, current);
		    }
	    }

		var (path, dirs) = GetPathDirections(end, start, cameFrom);


		path.Skip(1).WithIndex().ForEach(n => n.Value.Character = dirs[n.idx]);


		Console.WriteLine(grid);

		return costSoFar[end.Position].ToString();
    }

    private (List<INode>, List<char> dirs) GetPathDirections(INode end, INode start, Dictionary<INode, INode> cameFrom)
    {
	    var path = new List<INode>( );
	    var c = end;
	    while (!c.Equals(start))
	    {
		    path.Add(c);
		    c = cameFrom[c];
	    }
	    path.Add(start);
	    path.Reverse();
	    var dirs = new List<char>();
	    foreach (var (value, idx) in path.WithIndex().Skip(1))
	    {
		    dirs.Add(GetDirection(value, path[idx - 1]));
	    }

	    return (path, dirs);
    }

    public override async Task<string> SolvePart2( )
    {
    	return string.Empty;
    }

    private char GetDirection(INode current, INode prev) => current.Position.Subtract(prev.Position) switch
    {
	    (1, _) => '>', // east
	    (_, 1) => 'v', // south
	    (-1, _) => '<', // west
	    (_, -1) => '^' // north
    };
}
