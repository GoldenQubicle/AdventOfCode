namespace AoC2024;

public class Day04 : Solution
{

	private readonly Grid2d puzzle;

	public Day04(string file) : base(file) => puzzle = new(Input);


	public override async Task<string> SolvePart1() => puzzle
		.Where(c => c.Character == 'X')
		.Select(c => puzzle.Offsets
			.Select(o => Enumerable.Range(1, 3)
				.Select(n => c.Position.Add(n * o.x, n * o.y))
				.Where(puzzle.IsInBounds))
			.Select(n => n.Select(p => puzzle[p].Character).AsString( ))
			.Count(w => w.Equals("MAS")))
		.Sum( ).ToString( );


	public override async Task<string> SolvePart2() => puzzle
		.Where(c => c.Character == 'A')
		.Select(c => puzzle.Offsets
			.Where(o => o.x != 0 && o.y != 0)
			.Select(o => c.Position.Add(o.x, o.y))
			.Where(p => puzzle.IsInBounds(p) && puzzle[p].Character is 'M' or 'S')
			.Select(p => puzzle[p]))
		.Where(n => n.Count( ) == 4)
		.Count(n => n
			.GroupBy(c => c.Character)
			.All(g => g.Count() == 2 && NotOpposite(g.ToList())))
		.ToString( );

	//if both x & y differ within a group of M or S they're on opposite corners
	//thus spelling out MAM or SAS
	private bool NotOpposite(List<Cell> group) => 
		group[0].X == group[1].X || group[0].Y == group[1].Y;

}
