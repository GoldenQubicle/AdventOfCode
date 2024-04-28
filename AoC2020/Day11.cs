using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Common.Extensions;

namespace AoC2020;

public class Day11(string file) : Solution(file)
{
	private const char Seated = '#';
	private const char Empty = 'L';
	private const char Floor = '.';


	public override async Task<string> SolvePart1()
	{
		var ca = new CellularAutomaton2d(Input, GetRules( ));

		ca.DetectCycle( );

		return ca.CountCells(Seated).ToString( );
	}


	public override async Task<string> SolvePart2()
	{
		var ca = new CellularAutomaton2d(Input, GetRules(isPart1: false), GetNeighborsInSight);

		ca.DetectCycle( );

		return ca.CountCells(Seated).ToString( );
	}


	private static Func<Grid2d.Cell, IReadOnlyCollection<Grid2d.Cell>, Grid2d.Cell> GetRules(bool isPart1 = true) =>
		(cell, neighbors) => cell.Character switch
		{
			Empty when neighbors.All(c => c.Character is Empty or Floor) => cell with { Character = Seated },
			Seated when neighbors.Count(c => c.Character is Seated) >= (isPart1 ? 4 : 5) => cell with { Character = Empty },
			_ => cell
		};

	public static List<Grid2d.Cell> GetNeighborsInSight(Grid2d.Cell cell, Grid2d grid) =>
		grid.Offsets.Select(o =>
		{
			var nPos = cell.Position.Add(o);
			while (grid.IsInBounds(nPos))
			{
				if (grid[nPos].Character is Empty or Seated)
				{
					return grid[nPos];
				}

				nPos = nPos.Add(o);
			}

			var pPos = (nPos.x - o.x, nPos.y - o.y);
			return grid[pPos];
		}).Where(c => c.Position != cell.Position).Distinct( ).ToList( );


}