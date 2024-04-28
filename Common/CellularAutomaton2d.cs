using System;
using System.Collections.Generic;
using System.Linq;
using static Common.Grid2d;

namespace Common;

/// <summary>
/// Generic Cellular Automaton Solver, operates on underlying Grid2D - constructed from Input - according to the rules given. 
/// </summary>
/// <param name="input">The AoC input, assuming the conventional grid input.</param>
/// <param name="rules">The Rules, applied per iteration step.</param>
/// <param name="getNeighbors">Optional, when not supplied will use default Grid2D implementation with diagonals enabled. </param>
public class CellularAutomaton2d(
	IReadOnlyList<string> input, 
	Func<Cell, IReadOnlyCollection<Cell>, Cell> rules,
	Func<Cell, Grid2d, List<Cell>> getNeighbors = default)
{
	private Grid2d grid = new(input);


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

		return new(start, cycleLength);
	}

	private Grid2d DoIteration(Grid2d g) =>
		g.Aggregate(new Grid2d( ), (newGrid, cell) =>
		{
			var neighbors = getNeighbors == null 
				? g.GetNeighbors(cell) 
				: getNeighbors(cell, g);

			newGrid.Add(rules(cell, neighbors ));
			return newGrid;
		});

	public int CountCells(char state) => grid.GetCells(c => c.Character == state).Count;


	public record CycleResult(int Start, int Length);
}