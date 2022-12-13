namespace AoC2022
{
    public class Day12 : Solution
    {
        private Grid2d grid;

        public Day12(string file) : base(file) => grid = new(Input, diagonalAllowed: false);

        public override string SolvePart1() => grid.GetShortestPath(
                grid.GetCells(c => c.Character == 'S').First(),
                grid.GetCells(c => c.Character == 'E').First(),
                (c,n) => getCharacter(n.Character) - 1 <= getCharacter(c.Character),
                (c,t) => c.Character == t.Character).Count.ToString();


        public override string SolvePart2() => 
             grid.Where(c => getCharacter(c.Character) == 'a' && grid.GetNeighbors(c).Any(n => n.Character == 'b'))
                .Select(s =>
                {
                    var g = new Grid2d(Input, diagonalAllowed: false);
                    return g.GetShortestPath(s,
                        g.GetCells(c => c.Character == 'E').First(),
                        (c, n) => getCharacter(n.Character) - 1 <= getCharacter(c.Character),
                        (c, t) => c.Character == t.Character);
                }).Min(p => p.Count).ToString();

        char getCharacter(char c) => c switch
        {
            'S' => 'a',
            'E' => 'z',
            _ => c
        };
    }
}