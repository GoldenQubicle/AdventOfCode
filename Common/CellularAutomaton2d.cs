using System;
using System.Collections.Generic;
using System.Linq;
using static Common.Grid2d;

namespace Common;

public class CellularAutomaton2d(IReadOnlyList<string> input, Func<Cell, IReadOnlyCollection<Cell>, Cell> rules)
{
	private Grid2d grid = new(input);

	public Func<Cell, IReadOnlyCollection<Cell>, Cell> Rules = rules;


	public void Iterate(int steps)
	{
		for (var i = 0 ;i < steps ;i++)
		{
			grid = DoIteration(grid);
		}
	}

	public CycleResult DetectCycle()
	{
		var iteration = 0;
		var state = new Dictionary<string, int>( );

		while (true)
		{
			iteration++;
			grid = DoIteration(grid);

			//break when the same state has been added, assuming start of cycle
			if (!state.TryAdd(grid.ToString( ), iteration))
				break;
		}

		var start = state[grid.ToString( )];
		var cycleLength = iteration - start;

		return new(start, cycleLength, grid);
	}

	private Grid2d DoIteration(Grid2d g) =>
		g.Aggregate(new Grid2d( ), (newGrid, cell) =>
		{
			newGrid.Add(Rules(cell, g.GetNeighbors(cell)));
			return newGrid;
		});

	public int CountCells(char state) => grid.GetCells(c => c.Character == state).Count;


	public record CycleResult(int Start, int Length, Grid2d State);
}