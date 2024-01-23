namespace AoC2021;

public class Day15 : Solution
{
	private Grid2d grid;
	public Day15(string file) : base(file) =>
		grid = new Grid2d(Input, diagonalAllowed: false);

	public override async Task<string> SolvePart1()
	{
		var start = grid[0, 0];
		var target = grid[grid.Max(c => c.X), grid.Max(c => c.Y)];

		var result = await PathFinding.UniformCostSearch(start, target, grid,
			(node, node1) => true,
			(node, node1) => node.Equals(target),
			(node, node1) => 0L);

		result.path.ForEach(c => c.Character = '#');
		return result.cost.ToString();
	}

	public override async Task<string> SolvePart2()
	{
		var expansion = new List<Grid2d.Cell>( );

		foreach (var cell in grid)
		{
			var value = cell.Value;
			for (var c = 1 ;c < 5 ;c++)
			{
				value = value + 1 == 10 ? 1 : value + 1;
				var nc = new Grid2d.Cell(cell.Position.Add(c * grid.Width, 0), char.Parse(value.ToString( )));
				expansion.Add(nc);
			}
		}
		expansion.ForEach(grid.Add);
		expansion.Clear( );

		foreach (var cell in grid)
		{
			var value = cell.Value;
			for (var r = 1 ;r < 5 ;r++)
			{
				value = value + 1 == 10 ? 1 : value + 1;
				var nc = new Grid2d.Cell(cell.Position.Add(0, r * grid.Height), char.Parse(value.ToString( )));
				expansion.Add(nc);
			}
		}
		expansion.ForEach(grid.Add);

		var start = grid[0, 0];
		var target = grid[grid.Max(c => c.X), grid.Max(c => c.Y)];

		var result = await PathFinding.UniformCostSearch(start, target, grid,
			(node, node1) => true,
			(node, node1) => node.Equals(target),
			(node, node1) => Maths.GetManhattanDistance((node as Grid2d.Cell).Position, (node1 as Grid2d.Cell).Position));


		result.path.ForEach(c => c.Character = '#');
		
		return result.cost.ToString();
	}
}