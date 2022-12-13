using System.Diagnostics;

namespace AoC2022
{
    public class Day12 : Solution
    {
        private Grid2d grid;

        public Day12(string file) : base(file) => grid = new(Input, diagonalAllowed: false);

        public override string SolvePart1()
        {
            var paths = new List<List<Position>>();
            var routes = new PriorityQueue<(List<Position> visited, Grid2d.Cell step), (int, int)>();
            var start = grid.GetCells(c => c.Character == 'S').First();
            //start = start.ChangeCharacter('a');

            routes.Enqueue((new(), start), (0, 'a'));

            while (routes.Count > 0)
            {
                var current = routes.Dequeue();

                if (current.step.Character == 'E')
                {
                    paths.Add(current.visited);
                }

                grid.GetNeighbors(current.step, n => getCharacter(n.Character) - 1 <= getCharacter(current.step.Character) && !current.visited.Contains(n.Position))
                    .ForEach(n => routes.Enqueue((current.visited.Expand(current.step.Position), n), (current.visited.Count, getCharacter(n.Character))));
            }

            return paths.Min(p => p.Count).ToString();


            char getCharacter(char c) => c switch
            {
                'S' => 'a',
                'E' => 'z',
                _ => c
            };
        }

        public override string SolvePart2() => null;
    }
}