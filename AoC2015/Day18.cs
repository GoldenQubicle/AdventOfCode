using Common;
using Common.Extensions;

namespace AoC2015
{
    public class Day18 : Solution
    {
        readonly CellularAutomaton automaton;
        public int Steps { get; set; } = 100;

        public Day18(string file) : base(file, "\n")
        {
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

            var ul = new Position( 0, 0 );
            var ur = new Position( dim, 0 );
            var bl = new Position( 0, dim );
            var br = new Position( dim, dim );

            automaton.ApplyPositionalRules = (pos, state) => pos == ul || pos == ur || pos == bl || pos == br ? '#' : state;
            automaton.GenerateGrid(Input);
            automaton.Iterate(Steps);
           
            return automaton.CountCells('#').ToString();
        }
    }
}