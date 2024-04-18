namespace AoC2021;

public class Day20 : Solution
{
	private readonly string algo;
	private Grid2d grid;

	public Day20(string file) : base(file, split: "\n")
	{
		algo = Input.First( );
		grid = new Grid2d(Input.Skip(1).ToList( ), diagonalAllowed: true, isInfinite: true);
	}

	public override async Task<string> SolvePart1() => EnhanceImage(2);

	public override async Task<string> SolvePart2() => EnhanceImage(50);
	

	private string EnhanceImage(int count)
	{
		for (var i = 0; i < count; i++)
		{
			var newState = new Grid2d(diagonalAllowed: true, isInfinite: true);
			var (minx, maxx, miny, maxy) = grid.GetBounds()

			for (var x = minx - 2; x < maxx + 2; x++)
			{
				for (var y = miny - 2; y < maxy + 2; y++)
				{
					var p = (x, y);
					var cell = grid.IsInBounds(p) ? grid[p] : new Grid2d.Cell(p, grid.EmptyCharacter);

					var square = grid.GetNeighbors(cell)
						.Expand(cell)
						.OrderBy(c => c.Y)
						.ThenBy(c => c.X).ToList();

					var nc = new Grid2d.Cell(cell.Position, GetOutputPixel(ToBinary(square.Select(c => c.Character))));
					newState.AddOrUpdate(nc);
				}
			}

			newState.EmptyCharacter = GetOutputPixel(ToBinary(Enumerable.Repeat(grid.EmptyCharacter, 9)));
			grid = newState;
		}

		return grid.Count(c => c.Character == '#').ToString();
	}

	private string ToBinary(IEnumerable<char> input) => input.Aggregate(new StringBuilder( ), (sb, c) => sb.Append(c == '.' ? 0 : 1)).ToString( );

	private char GetOutputPixel(string binary) => algo[Convert.ToInt32(binary, 2)];

	
}
