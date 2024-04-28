using System;
using System.Collections.Generic;
using System.Linq;
using static Common.Grid2d;

namespace Common;

public class CellularAutomaton2d(IReadOnlyList<string> input)
{
	private Grid2d grid = new(input);

	public Func<Cell, IReadOnlyCollection<Cell>, Cell> Rules { get; init; }


	public void Iterate(int steps)
	{
		for (var i = 0 ;i < steps ;i++)
		{
			grid = DoIteration(grid);
		}
	}

	private Grid2d DoIteration(Grid2d g) =>
		g.Aggregate(new Grid2d( ), (newGrid, cell) =>
		{
			newGrid.Add(Rules(cell, g.GetNeighbors(cell)));
			return newGrid;
		});

	public int CountCells(char state) => grid.GetCells(c => c.Character == state).Count;
}