using Common;
using Common.Extensions;

namespace AoC2023;

public class Day14 : Solution
{
	private readonly Grid2d grid;

	public Day14(string file) : base(file) => grid = new Grid2d(Input);
    
    public override string SolvePart1( )
    {
		TiltNorth();

		return grid.Where(c => c.Character == 'O').Sum(c => (grid.Height - c.Y)).ToString();
    }

    public override string SolvePart2()
    {
		//we could do cycle detection or just run it a decreased manifold
		//like wise we could write a nice generic tilt method or just copy paste 4 times and change it around..
	    for (var i = 0; i < 1000; i++)
	    {
		    //Console.WriteLine(grid);
			TiltNorth();
			//Console.WriteLine(grid);
			TiltWest();
			//Console.WriteLine(grid);
			TiltSouth();
			//Console.WriteLine(grid);
			TiltEast();
			//Console.WriteLine(grid);
	    }

	    return grid.Where(c => c.Character == 'O').Sum(c => (grid.Height - c.Y)).ToString();
    }

	private void TiltEast()
    {
	    var col = grid.Width -1;
	    while (col >= 0)
	    {
		    var rocks = grid.GetColumn(col).Where(c => c.Character == 'O').ToList( );
		    foreach (var rock in rocks)
		    {
			    var p = rock.Position.Add(1, 0);
			    while (p.x < grid.Width)
			    {
				    if (grid[p].Character != '.')
					    break;

				    p = p.Add(1, 0);
			    }

			    grid[p.Add(-1, 0)].Character = 'O';
			    if (rock.Position.x != p.Add(-1, 0).x)
				    grid[rock.Position].Character = '.';

			    //Console.WriteLine(grid);
		    }

		    col--;
	    }
    }

	private void TiltWest()
    {
	    var col = 1;
	    while (col < grid.Width)
	    {
		    var rocks = grid.GetColumn(col).Where(c => c.Character == 'O').ToList( );
		    foreach (var rock in rocks)
		    {
			    var p = rock.Position.Add(-1, 0);
			    while (p.x >= 0)
			    {
				    if (grid[p].Character != '.')
					    break;

				    p = p.Add(-1, 0);
			    }

			    grid[p.Add(1,0)].Character = 'O';
			    if (rock.Position.x != p.Add(1, 0).x)
				    grid[rock.Position].Character = '.';

			    //Console.WriteLine(grid);
		    }

		    col++;
	    }
    }

	private void TiltNorth()
    {
	    var row = 1;
	    while (row < grid.Height)
	    {
		    var rocks = grid.GetRow(row).Where(c => c.Character == 'O').ToList();
		    foreach (var rock in rocks)
		    {
			    var p = rock.Position.Add(0, -1);
			    while (p.y >= 0)
			    {
				    if (grid[p].Character != '.')
					    break;

				    p = p.Add(0, -1);
			    }

			    grid[p.Add(0, 1)].Character = 'O';
			    if (rock.Position.y != p.Add(0, 1).y)
				    grid[rock.Position].Character = '.';

			    //Console.WriteLine(grid);
		    }

		    row++;
	    }
    }

    private void TiltSouth()
    {
	    var row = grid.Height - 1;
	    while (row >= 0)
	    {
		    var rocks = grid.GetRow(row).Where(c => c.Character == 'O').ToList();
		    foreach (var rock in rocks)
		    {
			    var p = rock.Position.Add(0, 1);
			    while (p.y < grid.Height)
			    {
				    if (grid[p].Character != '.')
					    break;

				    p = p.Add(0, 1);
			    }

			    grid[p.Add(0, -1)].Character = 'O';
			    if (rock.Position.y != p.Add(0, -1).y)
				    grid[rock.Position].Character = '.';

			    //Console.WriteLine(grid);
		    }

		    row--;
	    }
    }
}
