using System;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public class CellularAutomaton
    {
        public Func<int, char, char> ApplyGameOfLifeRules;
        public Func<Position, char, char> ApplyPositionalRules;
        private Dictionary<Position, char> Grid { get; set; } = new();
        private List<Position> Offsets { get; }
        private CellularAutomatonOptions Options { get; }
        private const char Active = '#';
        private const char InActive = '.';

        public CellularAutomaton(CellularAutomatonOptions options)
        {
            Options = options;
            Offsets = Combinator.Generate(new List<int> { -1, 0, 1 },
                new CombinatorOptions { Length = Options.Dimensions })
                 .Where(r => !r.All(v => v == 0)).Select(r => new Position(r.ToArray())).ToList();
        }

        public void Iterate(int steps)
        {
            for(var i = 0 ; i < steps ; i++)
            {
                var newGrid = new Dictionary<Position, char>();
                foreach(var (position, state) in Grid)
                {
                    var neighors = GetNeighbors(position);
                    var active = neighors.Count(n => n == Active);//assuming AoC input treats # as active state
                    var newState = ApplyGameOfLifeRules(active, state);

                    if(ApplyPositionalRules != null)
                        newState = ApplyPositionalRules(position, newState);

                    newGrid.Add(position, newState);
                };
                Grid = newGrid;
            }
        }

        public int CountCells(char state) => Grid.Values.Count(s => s == state);

        public void GenerateGrid(List<string> input)
        {
            Grid = new();
            switch(Options.Dimensions)
            {
                case 2:
                    for(var y = 0 ; y < input.Count ; y++)
                    {
                        for(var x = 0 ; x < input[y].Length ; x++)
                        {
                            Grid.Add(new Position(x, y), input[x][y]);
                        }
                    }
                    break;

                default: throw new ArgumentOutOfRangeException();
            }
        }

        private List<char> GetNeighbors(Position pos)
        {
            return Offsets
                .Select(offset => pos + offset)
                .Select(n =>
                {
                    //TODO check for wrap around
                    if(Grid.ContainsKey(n)) return Grid[n];
                    else return InActive; //no wrap and assuming AoC input always treats '.' as inactive state for now
                }).ToList();
        }
    }
}
