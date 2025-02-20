namespace AoC2023;

public class Day14 : Solution
{
	private readonly Grid2d grid;

	public Day14(string file) : base(file) => grid = new Grid2d(Input);


	public override async Task<string> SolvePart1()
	{
		TiltPlatform((0, -1), 1);

		return grid.Where(c => c.Character == 'O').Sum(c => (grid.Height - c.Y)).ToString( );
	}

	public override async Task<string> SolvePart2()
	{
		var loads = new List<int>( );

		for (var i = 0 ;i < 150 ;i++)
		{
			TiltPlatform((0, -1), 1); //north
			TiltPlatform((-1, 0), 1); //west
			TiltPlatform((0, 1), grid.Height - 1); //south
			TiltPlatform((1, 0), grid.Width - 1); // east
			loads.Add(grid.Where(c => c.Character == 'O').Sum(c => grid.Height - c.Y));
		}

		var result = Maths.DetectPattern(loads);
		var idx = (1000000000 - result.Idx - 1) % result.Pattern.Count;

		return result.Pattern[idx].ToString( );
	}


	private void TiltPlatform((int x, int y) offset, int idx)
	{
		var startsAtOne = idx == 1;
		var isColumn = offset.x != 0;

		Func<int, bool> hasRemaining = (startsAtOne, isColumn) switch
		{
			(false, true) or (false, false) => i => i >= 0,
			(true, true) => i => i < grid.Width,
			(true, false) => i => i < grid.Height
		};

		Func<(int x, int y), bool> canRoll = (startsAtOne, isColumn) switch
		{
			(true, true) => p => p.x >= 0,
			(false, true) => p => p.x < grid.Width,
			(true, false) => p => p.y >= 0,
			(false, false) => p => p.y < grid.Height
		};

		while (hasRemaining(idx))
		{
			var rocks = isColumn
				? grid.GetColumn(idx).Where(c => c.Character == 'O').ToList( )
				: grid.GetRow(idx).Where(c => c.Character == 'O').ToList( );

			foreach (var rock in rocks)
			{
				var p = rock.Position.Add(offset);

				while (canRoll(p))
				{
					if (grid[p].Character != '.')
						break;

					p = p.Add(offset);
				}

				grid[p.Subtract(offset)].Character = 'O';

				var replaceRock = isColumn
					? rock.Position.x != p.Subtract(offset).x
					: rock.Position.y != p.Subtract(offset).y;

				if (replaceRock)
					grid[rock.Position].Character = '.';
			}

			if (startsAtOne)
				idx++;
			else
				idx--;
		}
	}
}
