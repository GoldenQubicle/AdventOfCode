using Common;
using Common.Extensions;

namespace AoC2023;

public class Day17 : Solution
{
	private readonly Grid2d grid;

	public Day17(string file) : base(file) => grid = new Grid2d(Input, diagonalAllowed: false);
    

    public override async Task<string> SolvePart1( ) 
    {
        grid.ForEach(c => c.Cost = c.Character.ToLong());
        var prev = new List<Grid2d.Cell>();

        var start = grid[0, 0];
        var end = grid[grid.Width - 1, grid.Height - 1];
     //   var p = PathFinding.UniformCostSearch(start, end, grid, )
     //   var path = grid.GetShortestPath_V1(start, end,
	    //    (current, n) =>
	    //    {
     //           if (!prev.Contains(current))
     //               prev.Add(current);
     //           if (prev.Count <= 3) return true;

     //           if (prev.TakeLast(3).All(c => c.X == current.X))
	    //            return n.X != current.X;

     //           if (prev.TakeLast(3).All(c => c.Y == current.Y))
	    //            return n.Y != current.Y;

     //           return true;
	    //    }, 
	    //    (c, t) => c.Position == t.Position);

    	//return path.Sum(c => c.Character.ToLong( )).ToString();
        return string.Empty;
    }

    public override async Task<string> SolvePart2( )
    {
    	return string.Empty;
    }
}
