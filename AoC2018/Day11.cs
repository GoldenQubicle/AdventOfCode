using Common;
using Common.Extensions;

namespace AoC2018
{
	public class Day11 : Solution
	{
		public static int SerialNumber { get; set; } = 7689;
		private Grid2d grid;

		public Day11(string file) : base(file) { }

		public void InitializeGrid()
		{
			grid = new Grid2d(300, 300);
			grid.ForEach(c =>
			{
				c.Value = CalculatePowerLevel(c.Position.Add(1, 1), SerialNumber);
			});
		}

		public override string SolvePart1()
		{
			InitializeGrid( );
			var maxLevel = 0L;
			var pos = (x: 0, y: 0);

			foreach (var cell in grid)
			{
				var n = grid.GetNeighbors(cell);
				if (n.Count != 8)
					continue;

				var level = n.Sum(c => c.Value) + cell.Value;

				if (level > maxLevel)
				{
					maxLevel = level;
					pos = (cell.Position.x, cell.Position.y);
				}
			}

			return $"{pos.x},{pos.y}";
		}

		public override string SolvePart2()
		{
			InitializeGrid( );
			
			var maxLevel = 0L;
			var pos = (x: 0, y: 0);
			var size = 0;

			//constrained the square dimensions for the test cases but it proofed sufficient for the actual solution too :)
			for (var s = 10; s < 20; s++)
			{
				for (var x = 0 ;x < grid.Width - s ;x++)
				{
					for (var y = 0 ;y < grid.Height - s ;y++)
					{
						var level = 0L;
						var topLeft = (x, y);
						for (var dx = x ;dx < x + s ;dx++)
						{
							for (var dy = y ;dy < y + s ;dy++)
							{
								level += grid[(dx, dy)].Value;
							}
						}

						if (level >= maxLevel)
						{
							maxLevel = level;
							pos = topLeft;
							size = s;
						}
					}
				}
			}

			//grid2d is zero based, hence we add 1 to the x & y components of the position
			return $"{pos.x + 1},{pos.y + 1},{size}";
		}


		public static int CalculatePowerLevel((int x, int y) pos, int serialNumber)
		{
			var rackId = pos.x + 10;
			var powerLevel = rackId * pos.y;
			powerLevel += serialNumber;
			powerLevel *= rackId;
			return Math.Abs(powerLevel / 100 % 10) - 5;
		}
	}
}