using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common;
using Common.Extensions;

namespace AoC2023;

public class Day16 : Solution
{
	private readonly Grid2d grid;

	public Day16(string file) : base(file) => grid = new Grid2d(Input);


	public override string SolvePart1()
	{
		var queue = new Queue<Beam>( );
		var tiles = new HashSet<(int x, int y)>( );
		var maxTilesCount = 0;
		var beamCount = 0;

		queue.Enqueue(new Beam((0, 0), (1, 0), SplitBeam));

		while (queue.TryDequeue(out var beam))
		{
			beamCount++;
			while (grid.IsInBounds(beam.Position))
			{
				tiles.Add(beam.Position);
				beam.Update(grid[beam.Position].Character);
			}

			if (tiles.Count > maxTilesCount)
			{
				maxTilesCount = tiles.Count;
				beamCount = 0;
			}
			Console.WriteLine($"tile count {tiles.Count} | beam count {beamCount}");

			if (beamCount == 100000)
				break;
		}

		return tiles.Count.ToString( );


		(int, int) SplitBeam(char c, (int, int) p)
		{
			switch (c)
			{
				case '-':
				{
					queue.Enqueue(new Beam(p, (1, 0), SplitBeam));
					return (-1, 0);
				}
				case '|':
				{
					queue.Enqueue(new Beam(p, (0, 1), SplitBeam));
					return (0, -1);
				}
				default:
					return (0, 0);
			}
		}
	}

	public override string SolvePart2()
	{
		return string.Empty;
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
				('/', (1, 0)) => (0, -1),
				('/', (-1, 0)) => (0, 1),
				('/', (0, 1)) => (-1, 0),
				('/', (0, -1)) => (1, 0),

				('\\', (1, 0)) => (0, 1),
				('\\', (-1, 0)) => (0, -1),
				('\\', (0, 1)) => (1, 0),
				('\\', (0, -1)) => (-1, 0),

			};

			Position = Position.Add(Direction);
		}
	};
}
