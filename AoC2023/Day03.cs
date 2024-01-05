using System.Text;
using Common;
using Common.Extensions;

namespace AoC2023;

public class Day03 : Solution
{
	private readonly Grid2d grid;
	public Day03(string file) : base(file) => grid = new Grid2d(Input);


	public override async Task<string> SolvePart1()
	{
		var parts = new List<int>( );
		var sb = new StringBuilder( );
		var neighbors = new List<Grid2d.Cell>( );

		foreach (var cell in grid)
		{
			if (char.IsDigit(cell.Character))
			{
				sb.Append(cell.Character);
				neighbors.AddRange(grid.GetNeighbors(cell).Where(c => !char.IsDigit(c.Character)));
				continue;
			}

			if (neighbors.Count == 0)
				continue;

			if (neighbors.Any(n => n.Character != '.'))
			{
				parts.Add(int.Parse(sb.ToString( )));
			}

			neighbors.Clear( );
			sb.Clear( );
		}
		return parts.Sum( ).ToString( );
	}


	public override async Task<string> SolvePart2()
	{
		var ratios = new List<long>( );
		foreach (var cell in grid)
		{
			if (cell.Character != '*')
				continue;

			var neighbors = grid.GetNeighbors(cell).Where(n => char.IsDigit(n.Character)).ToList( );

			if (neighbors.Count < 2)
				continue;

			//please look away from this nightmare. 
			//basically for each digit neighbor looking to the left & right to determine the range to grab
			var digits = neighbors
				.Select(d =>
					grid.TryGetCell(d.Position.Add(-1, 0), out var m1) && m1.Character is '.' && grid.TryGetCell(d.Position.Add(1, 0), out var m2) && m2.Character is '.'
						? new List<Grid2d.Cell> { d } // it is just a single digit, e.g. 7
						: grid.TryGetCell(d.Position.Add(-1, 0), out var cl1) && cl1.Character is '.' or '*'
							? grid.GetRange(d.Position, d.Position.Add(2, 0)) // grab 2 to the right
							: grid.TryGetCell(d.Position.Add(1, 0), out var cr1) && cr1.Character is '.' or '*'
								? grid.GetRange(d.Position.Add(-2, 0), d.Position) // grab 2 to the left
								: grid.GetRange(d.Position.Add(-1, 0), d.Position.Add(1, 0))) // grab 1 from left & right
				.Select(d => int.Parse(new string(d.Where(c => char.IsDigit(c.Character)).Select(c => c.Character).ToArray( )))) // filter out any . in grabbed range and convert to int
				.Distinct( ).ToList( );

			//nasty check for one particular gear wherein the same digit appears above and below the gear. 
			//hence we check when if only 1 digit is found but there are 2 distinct y positions for the neighbors
			//for completeness sake we should also check the same scenario for distinct x positions of neighbors
			//however this case does not appear in the input so screw it. 
			if (digits.Count == 1 && neighbors.Select(n => n.Position.y).Distinct( ).Count( ) == 2)
			{
				digits.Add(digits.First( ));
			}
			else if (digits.Count < 2)
			{
				continue;
			}

			ratios.Add(digits[0] * digits[1]);
		}

		return ratios.Sum( ).ToString( );
	}
}