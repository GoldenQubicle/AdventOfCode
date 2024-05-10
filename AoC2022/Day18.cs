namespace AoC2022;

public class Day18 : Solution
{
	private readonly HashSet<Vector3> cubes;

	private readonly List<Vector3> offsets = new( )
	{
		new (1, 0, 0),  new (-1, 0, 0),
		new (0, 1, 0),  new (0, -1, 0),
		new (0, 0, 1),  new (0, 0, -1),
	};


	public Day18(string file) : base(file, split: "\n") =>
		cubes = Input.Select(l => Regex.Match(l, @"(?<x>\d+),(?<y>\d+),(?<z>\d+)").AsVector3()).ToHashSet();


	public override async Task<string> SolvePart1() => GetSurfaceCount(cubes).ToString( );


	public override async Task<string> SolvePart2()
	{
		// The basic outline; start by inversions of the cube grid.
		// Take a cell, perform flood fill, designate filled area as cluster.
		// Rinse and repeat until all negative space has been filled, i.e. organized into clusters. 
		// Filter clusters, any which contains points on the edges cannot be contained by cubes.
		// Get surface area of remaining, contained, clusters and subtract from total sides part 1. 

		var (X, Y, Z) = GetCubesBounds( );
		var grid = GetInverseCubeSpace(X, Y, Z); 
		var queue = new Queue<Vector3>( );
		var seen = new HashSet<Vector3>( );
		var clusters = new Dictionary<int, HashSet<Vector3>>( );
		
		queue.Enqueue(grid.First( ));

		while (true)
		{
			while (queue.TryDequeue(out var current))
			{
				seen.Add(current);
				GetNeighbors(current, grid, seen, queue).ForEach(queue.Enqueue);
			}

			clusters.Add(clusters.Count, new(seen));
			seen.Clear( );

			var next = grid.Except(clusters.SelectMany(c => c.Value)).FirstOrDefault( );

			if (next == default)
				break;

			queue.Enqueue(next);
		}

		var containedSides = clusters.Values
			.Where(c => !c.Any(p => 
				p.X == X.min || p.X == X.max || 
				p.Y == Y.min || p.Y == Y.max || 
				p.Z == Z.min || p.Z == Z.max))
			.Aggregate(0, (sides, cluster) => sides + GetSurfaceCount(cluster));


		return (GetSurfaceCount(cubes) - containedSides).ToString( );
	}

	private HashSet<Vector3> GetInverseCubeSpace((int min, int max) X, (int min, int max) Y, (int min, int max) Z)
	{
		var grid = new HashSet<Vector3>( );

		for (var x = X.min ;x <= X.max ;x++)
			for (var y = Y.min ;y <= Y.max ;y++)
				for (var z = Z.min ;z <= Z.max ;z++)
					grid.Add(new(x, y, z));


		return grid.Except(cubes).ToHashSet( );
	}


	private ((int min, int max) X, (int min, int max) Y, (int min, int max) Z) GetCubesBounds() =>
		(new((int)cubes.Min(c => c.X) - 1, (int)cubes.Max(c => c.X) + 1),
		new((int)cubes.Min(c => c.Y) - 1, (int)cubes.Max(c => c.Y) + 1),
		new((int)cubes.Min(c => c.Z) - 1, (int)cubes.Max(c => c.Z) + 1));


	private List<Vector3> GetNeighbors(Vector3 current, HashSet<Vector3> grid, HashSet<Vector3> seen, Queue<Vector3> queue) =>
		offsets.Select(o => o + current)
			.Where(p => grid.Contains(p) && !seen.Contains(p) && !queue.Contains(p)).ToList( );


	private int GetSurfaceCount(IReadOnlyCollection<Vector3> points) => points
		.Aggregate(0, (totalSides, cube) => totalSides + offsets
			.Aggregate(6, (sides, offset) => points.Contains(cube + offset) ? sides - 1 : sides));

}
