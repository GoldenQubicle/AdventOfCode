using System;
using System.Collections.Generic;
using static Common.Grid2d;

namespace Common
{
    public class CellularAutomaton2d
    {
        public Func<int, Cell, Cell> GameOfLifeRules { get; init; }
        public Func<Cell, Cell> AdditionalRules { get; init; }

        private Grid2d grid;
        private const char Active = '#';
        private const char InActive = '.';
            
        public CellularAutomaton2d(List<string> input) => grid = new Grid2d(input);
        
        public void Iterate(int steps)
        {
            for (var i = 0; i < steps; i++)
            {
                var newGrid = new Grid2d();
                foreach (var cell in grid)
                {
                    var activeCount = grid.GetNeighbors(cell, c => c.Character == Active).Count; //assuming AoC input treats # as active state
                    var newCell = GameOfLifeRules(activeCount, cell);

                    if (AdditionalRules is not null)
                        newCell = AdditionalRules(newCell);

                    newGrid.Add(newCell);
                }
                grid = newGrid;
            }
        }

        public int CountCells(char state) => grid.QueryCells(c => c.Character == state).Count;
    }
}
