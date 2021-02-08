using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Extensions;

namespace AoC2015
{
    public class Day18 : Solution
    {
        private Dictionary<(int x, int y), char> grid;
        private readonly List<(int x, int y)> offsets;

        CellularAutomaton automaton;

        public int Steps { get; set; } = 100;

        public Day18(string file) : base(file, "\n")
        {
            grid = Input.SelectMany((line, y) => line.Select((c, x) => new KeyValuePair<(int, int), char>((x, y), c)))
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            offsets = Combinator.Generate(new List<int> { -1, 0, 1 }, new CombinatorOptions { Length = 2 })
                .Where(r => !r.All(v => v == 0)).Select(r => (r[0], r[1])).ToList();

            automaton = new CellularAutomaton(new CellularAutomatonOptions
            {
                Dimensions = 2,
                DoesWrap = false,
                IsInfinite = false
            });


            automaton.ApplyGameOfLifeRules = (activeCount, currentState) => activeCount switch
            {
                < 2 or > 3 when currentState == '#' => '.',
                3 when currentState == '.' => '#',
                _ => currentState
            };
        }

        public override string SolvePart1( )
        {
            automaton.GenerateGrid(Input);
            automaton.Iterate(Steps);
            return automaton.CountCells('#').ToString();
        }

        public override string SolvePart2( )
        {
            var dim = Input.Count - 1;

            Input[0] = Input[0].ReplaceAt(0, '#');
            Input[0] = Input[0].ReplaceAt(dim, '#');
            Input[dim] = Input[dim].ReplaceAt(0, '#');
            Input[dim] = Input[dim].ReplaceAt(dim, '#');

            var ul = new Position(new int[ ] { 0, 0 });
            var ur = new Position(new int[ ] { dim, 0 });
            var bl = new Position(new int[ ] { 0, dim });
            var br = new Position(new int[ ] { dim, dim });

            automaton.ApplyPositionalRules = (pos, state) => pos == ul || pos == ur || pos == bl || pos == br ? '#' : state;
            automaton.GenerateGrid(Input);
            automaton.Iterate(Steps);
           
            return automaton.CountCells('#').ToString();
        }
    }
}