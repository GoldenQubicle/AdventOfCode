using Common;
using Common.Extensions;
using static AoC2018.Day13;

namespace AoC2018
{
	public class Day13 : Solution
	{
		private Grid2d grid;
		private List<Cart> carts;

		public record Cart
		{
			public char Direction { get; set; }
			public (int x, int y) Position { get; set; }
			private int turns;

			public void Update() => Position = Direction switch
			{
				'<' => Position.Add(-1, 0),
				'>' => Position.Add(1, 0),
				'^' => Position.Add(0, -1),
				'v' => Position.Add(0, 1),
			};

			public void TurnAtIntersection()
			{
				Direction = (turns % 3, Direction) switch
				{
					(0, '<') or (2, '>') => 'v',
					(0, '>') or (2, '<') => '^',
					(0, '^') or (2, 'v') => '<',
					(0, 'v') or (2, '^') => '>',
					_ => Direction
				};
				turns++;
			}

			public void ChangeDirection(char corner) => Direction = (corner, Direction) switch
			{
				('/', '<') or ('\\', '>') => 'v',
				('/', '>') or ('\\', '<') => '^',
				('/', '^') or ('\\', 'v') => '>',
				('/', 'v') or ('\\', '^') => '<'
			};
		}

		public Day13(string file) : base(file, doTrim: false) { }

		private void InitializeGridAndCarts()
		{
			grid = new Grid2d(Input);

			carts = grid
				.Where(c => c.Character is '<' or '^' or '>' or 'v')
				.Select(c => new Cart { Direction = c.Character, Position = c.Position }).ToList( );

			grid.Where(c => c.Character is '<' or '^' or '>' or 'v')
				.ForEach(c => c.Character = c.Character switch
				{
					'<' or '>' => '-',
					'^' or 'v' => '|',
					_ => c.Character
				});
		}


		public override string SolvePart1()
		{
			InitializeGridAndCarts( );
			var collision = false;

			while (!collision)
				collision = DoUpdateCarts( ).Count != 0;

			return carts.GroupBy(c => c.Position).First(g => g.Count( ) == 2).Select(c => $"{c.Position.x},{c.Position.y}").First( );
		}

		public override string SolvePart2()
		{
			InitializeGridAndCarts( );

			while (carts.Count > 1)
				DoUpdateCarts( ).ForEach(c => carts.Remove(c));

			return $"{carts[0].Position.x},{carts[0].Position.y}";
		}

		private List<Cart> DoUpdateCarts()
		{
			var collided = new List<Cart>( );
			foreach (var cart in carts)
			{
				cart.Update( );
				var current = grid[cart.Position].Character;

				switch (current)
				{
					case '+':
						cart.TurnAtIntersection( );
						break;
					case '\\' or '/':
						cart.ChangeDirection(current);
						break;
				}
				
				collided.AddRange(carts.GroupBy(c => c.Position).Where(g => g.Count( ) == 2).SelectMany(g => g).ToList( ));
			}

			return collided;
		}
	}
}