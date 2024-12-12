namespace AoC2024;

public class Day12 : Solution
{
	private readonly Grid2d grid;
	public Day12(string file) : base(file) => grid = new(Input, diagonalAllowed: false);

	public override async Task<string> SolvePart1() => GetGardenPerimeters(isPart2: false);

	public override async Task<string> SolvePart2() => GetGardenPerimeters(isPart2: true);


	private string GetGardenPerimeters(bool isPart2 = false)
	{
		var offsets = new List<(int, int)> { (0, 0), (1, 0), (1, 1), (0, 1) };
		var plants = grid.GroupBy(c => c.Character);
		var gardens = new List<(int area, int perimenter)>( );

		foreach (var group in plants)
		{
			var remaining = group.ToHashSet( );

			while (remaining.Any( ))
			{
				var seen = new HashSet<Cell>( );
				var queue = new Queue<Cell> { remaining.First( ) };
				var vertices = new Dictionary<(int, int), int>( );

				//start flood fill 
				while (queue.TryDequeue(out var current))
				{
					seen.Add(current);
					var neighbors = grid.GetNeighbors(current);

					queue.EnqueueAll(neighbors.Where(n => !queue.Contains(n) && !seen.Contains(n) && n.Character == group.Key));

					//add 4 vertices per cell, i.e. the perimeter at cell level
					//keep track of count per vertex to determine overlap later
					offsets.ForEach(o =>
					{
						if (!vertices.TryAdd(current.Position.Add(o), 1))
							vertices[current.Position.Add(o)] += 1;
					});

				}

				var diagonals = vertices.Count(v => IsDiagonal(seen, v));

				var edges = isPart2
					? vertices.Count(kvp => kvp.Value is 1 or 3) + diagonals * 2 //1 is an outer corner, 3 is an inner corner, count diagonals twice.
					: vertices.Count(kvp => kvp.Value < 4) - diagonals + diagonals * 2; //vertex with count 4 is on the inside. Weird diagonal bookkeeping but just dividing by 2 is not correct.

				gardens.Add((seen.Count, edges));

				remaining = remaining.Except(seen).ToHashSet( );
			}
		}

		return gardens.Sum(g => g.area * g.perimenter).ToString( );
	}

	//vertices have an offset relative to cell position
	//for vertices with count 2  we check if it either has cells left up && right down OR left down && right up
	private static bool IsDiagonal(HashSet<Cell> visited, KeyValuePair<(int, int), int> v) => v.Value == 2 &&
		(visited.Any(c => c.Position == v.Key) && visited.Any(c => c.Position.Add(1, 1) == v.Key) ||
		visited.Any(c => c.Position.Add(1, 0) == v.Key) && visited.Any(c => c.Position.Add(0, 1) == v.Key));

}
