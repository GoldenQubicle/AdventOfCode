using Common.Interfaces;

namespace AoC2022
{
	public class Day12 : Solution
	{
		private readonly Grid2d grid;

		public Day12(string file) : base(file) => grid = new(Input, diagonalAllowed: false);

		public override async Task<string> SolvePart1() => await GetPath(
				grid.GetCells(c => c.Character == 'S').First( ),
				grid.GetCells(c => c.Character == 'E').First( ))
			.ContinueWith(t => (t.Result.Count( ) - 1).ToString( ));


		public override async Task<string> SolvePart2() => await Task.WhenAll(grid
				.Where(c => getCharacter(c.Character) == 'a' && grid.GetNeighbors(c).Any(n => n.Character == 'b'))
				.Select(async o => await GetPath(o, grid.GetCells(c => c.Character == 'E').First( ))))
			.ContinueWith(t => t.Result.Min(p => p.Count( ) - 1).ToString( ));

		
		private async Task<IEnumerable<INode>> GetPath(INode start, INode target) => await PathFinding.BreadthFirstSearch(
			start, target, grid,
			(c, n) => getCharacter(n.Character) - 1 <= getCharacter(c.Character),
			(c, t) => c.Character == t.Character,
			RenderAction);


		char getCharacter(char c) => c switch
		{
			'S' => 'a',
			'E' => 'z',
			_ => c
		};
	}
}