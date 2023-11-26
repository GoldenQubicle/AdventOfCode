using System;
using Common;
using Common.Extensions;

namespace AoC2015
{
    public class Day18 : Solution
    {
        public int Steps { get; set; } = 100;

        private readonly Func<int, Grid2d.Cell, Grid2d.Cell> gameOfLifeRules = (activeCount, cell) => activeCount switch
        {
            < 2 or > 3 when cell.Character == '#' => cell.ChangeCharacter('.'),
            3 when cell.Character == '.' => cell.ChangeCharacter('#'),
            _ => cell
        };

        public Day18(string file) : base(file, "\n") { }

        public override string SolvePart1()
        {
            var ca = new CellularAutomaton2d(Input) { GameOfLifeRules = gameOfLifeRules };
            ca.Iterate(Steps);
            return ca.CountCells('#').ToString();
        }

        public override string SolvePart2()
        {
            var dim = Input.Count - 1;

            Input[0] = Input[0].ReplaceAt(0, '#');
            Input[0] = Input[0].ReplaceAt(dim, '#');
            Input[dim] = Input[dim].ReplaceAt(0, '#');
            Input[dim] = Input[dim].ReplaceAt(dim, '#');

            var ul = (0, 0);
            var ur = (dim, 0);
            var bl = (0, dim);
            var br = (dim, dim);

            var ca = new CellularAutomaton2d(Input)
            {
                GameOfLifeRules = gameOfLifeRules,
                AdditionalRules = cell  => cell.Position == ul || cell.Position == ur || cell.Position == bl || cell.Position == br ? cell.ChangeCharacter('#') : cell
            };

            ca.Iterate(Steps);
            return ca.CountCells('#').ToString();
        }
    }
}