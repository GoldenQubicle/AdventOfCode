namespace AoC2022
{
    public class Day12 : Solution
    {
        private Grid2d grid;

        public Day12(string file) : base(file) => grid = new(Input, diagonalAllowed: false);

        public async Task SolveAsync() => await PathFinding.BreadthFirstSearch(
	        grid.GetCells(c => c.Character == 'S').First( ),
	        grid.GetCells(c => c.Character == 'E').First( ),
	        grid,
	        (c, n) => getCharacter(n.Character) - 1 <= getCharacter(c.Character),
	        (c, t) => c.Character == t.Character,
	        RenderAction);

        public override string SolvePart1() => ( Task.Run( async () => await PathFinding.BreadthFirstSearch(
                grid.GetCells(c => c.Character == 'S').First(),
                grid.GetCells(c => c.Character == 'E').First(),
                grid,
                (c,n) => getCharacter(n.Character) - 1 <= getCharacter(c.Character),
                (c,t) => c.Character == t.Character,
                RenderAction)).Result.Count() -1).ToString();

        // Note: as per 3-1-2024 the actual answer is no longer correct using the newfangled bfs... 
        public override string SolvePart2() => 
             grid.Where(c => getCharacter(c.Character) == 'a' && grid.GetNeighbors(c).Any(n => n.Character == 'b'))
                .Select(s =>
                {
                    var g = new Grid2d(Input, diagonalAllowed: false);
                    return PathFinding.BreadthFirstSearch(
	                    s, g.GetCells(c => c.Character == 'E').First(),
                        grid,
                        (c, n) => getCharacter(n.Character) - 1 <= getCharacter(c.Character),
                        (c, t) => c.Character == t.Character).Result.Count() -1;
                }).Min().ToString();

        char getCharacter(char c) => c switch
        {
            'S' => 'a',
            'E' => 'z',
            _ => c
        };
    }
}