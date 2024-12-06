namespace AoC2024;

public class Day06 : Solution
{

	private readonly Grid2d grid;

	private readonly List<(int, int)> offsets = [(0, -1), (1, 0), (0, 1), (-1, 0)];

	public Day06(string file) : base(file) => grid = new(Input, diagonalAllowed: false);
    

    public override async Task<string> SolvePart1( )
    {
	    var visited = new HashSet<(int, int)>();
	    var current = grid.First(c => c.Character == '^').Position;
	    var dir = 0;

	    while (true)
	    {
		    visited.Add(current);
		    var next = current.Add(offsets[dir]);
		    
		    if (!grid.IsInBounds(next))
			    break;

		    if (grid[next].Character == '#')
		    {
			    dir = (dir + 1) % 4;
			    next = current.Add(offsets[dir]);
			}

		    current = next;
	    }


    	return visited.Count.ToString();
    }

    public override async Task<string> SolvePart2( )
    {
    	return string.Empty;
    }
}
