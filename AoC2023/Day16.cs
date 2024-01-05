using Common;
using Common.Extensions;

namespace AoC2023;

public class Day16 : Solution
{
	private readonly Grid2d grid;

	public Day16(string file) : base(file) => grid = new Grid2d(Input);

	public override async Task<string> SolvePart1() => SimulateBeams((0, 0), (1, 0)).ToString( );

	public override async Task<string> SolvePart2()
	{
		//this is obviously slow af
		var left = grid.GetColumn(0).Select(c => SimulateBeams(c.Position, (1, 0))).Max( );
		var right = grid.GetColumn(grid.Width - 1).Select(c => SimulateBeams(c.Position, (-1, 0))).Max( );
		var top = grid.GetRow(0).Select(c => SimulateBeams(c.Position, (0, 1))).Max( );
		var down = grid.GetRow(grid.Height - 1).Select(c => SimulateBeams(c.Position, (0, -1))).Max( );

		return new List<int> { left, right, top, down }.Max( ).ToString( );
	}

	private int SimulateBeams((int x, int y) start, (int, int) dir)
	{
		var queue = new Queue<Beam>( );
		var tiles = new HashSet<(int x, int y)>( );
		var splitters = new HashSet<(int x, int y)>( );

		queue.Enqueue(new Beam(start, dir, SplitBeam));
		Console.WriteLine($"starting {start.x} {start.y} of {grid.Width} {grid.Height}");

		while (queue.TryDequeue(out var beam))
		{
			while (grid.IsInBounds(beam.Position))
			{
				tiles.Add(beam.Position);
				beam.Update(grid[beam.Position].Character);
			}
		}

		return tiles.Count;


		(int, int) SplitBeam(char c, (int, int) p)
		{
			switch (c)
			{
				case '-':
				{
					if (splitters.Contains(p))
						return (-1, 0);

					queue.Enqueue(new Beam(p, (1, 0), SplitBeam));
					splitters.Add(p);
					return (-1, 0);
				}
				case '|':
				{
					if (splitters.Contains(p))
						return (0, -1);

					queue.Enqueue(new Beam(p, (0, 1), SplitBeam));
					splitters.Add(p);

					return (0, -1);
				}
				default:
					return (0, 0);
			}
		}
	}


	public record Beam((int x, int y) Position, (int x, int y) Direction, Func<char, (int, int), (int, int)> Split)
	{
		public (int x, int y) Position { get; set; } = Position;
		public (int x, int y) Direction { get; set; } = Direction;

		public void Update(char c)
		{
			Direction = (c, Direction) switch
			{
				('.', _) => Direction,
				('|', (0, 1) or (0, -1)) => Direction,
				('-', (1, 0) or (-1, 0)) => Direction,
				('-', (0, 1) or (0, -1)) => Split('-', Position),
				('|', (1, 0) or (-1, 0)) => Split('|', Position),

				('/', (1, 0)) or ('\\', (-1, 0)) => (0, -1),
				('/', (-1, 0)) or ('\\', (1, 0)) => (0, 1),
				('/', (0, 1)) or ('\\', (0, -1)) => (-1, 0),
				('/', (0, -1)) or ('\\', (0, 1)) => (1, 0),
			};

			Position = Position.Add(Direction);
		}
	};
}
