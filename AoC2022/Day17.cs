using System.Numerics;
using System.Runtime.InteropServices;

namespace AoC2022;

public class Day17 : Solution
{
	private static readonly CircularList<Func<Rock>> rocks = new( )
	{
		() => new() { Blocks = new() { new(0,0), new(1,0),  new(2,0),   new (3,0) }             },
		() => new() { Blocks = new() { new(0,0), new(1,0),  new(2,0),   new (1,1), new(1, -1) } },
		() => new() { Blocks = new() { new(0,0), new(1,0),  new(2,0),   new(2,1),  new(2,2) }   },
		() => new() { Blocks = new() { new(0,0), new(0,-1), new(0,-2),  new(0,-3) }             },
		() => new() { Blocks = new() { new(0,0), new(1,0),  new (0,-1), new (1, -1) }           }
	};



	private readonly CircularList<Direction> jets;

	public Day17(string file) : base(file)
	{
		jets = Input[0].Aggregate(new CircularList<Direction>( ), (list, c) =>
		{
			list.Add(c == '>' ? Direction.Right : Direction.Left);
			return list;
		});

		jets.ResetHead( );
		rocks.ResetHead( );
	}

	public override async Task<string> SolvePart1()
	{
		// so the tricky part here is that the grid keeps expanding upwards as rocks appear;
		// 'Each rock appears so that its left edge is two units away from the left wall and its bottom edge is three units above the highest rock in the room (or the floor, if there isn't one).'
		// consequently it would be massively helpful to have y = 0 at the bottom.. 
		// then again there doesn't seem to be anything inherently stopping me from considering y=0 being at the bottom, other than the toString method printing upside down
		var grid = new Grid2d(7, 3500);
		var placed = new List<Rock>( );
		var steps = 0L;

		while (steps++ < 2022)
		{

			var rock = rocks.Current( );
			rocks.MoveRight( );
			var spawn = GetSpawnPoint(rock, placed);

			rock.Spawn(spawn);
			rock.Blocks.ForEach(b => grid[b.ToTuple( )].Character = '#');

			//Console.WriteLine(grid);

			while (true)
			{

				var push = jets.Current;
				jets.MoveRight( );

				if (rock.TryMove(push, placed))
				{
					rock.Blocks.ForEach(b => grid[b.ToTuple( )].Character = '.');
					rock.Move(push);
					rock.Blocks.ForEach(b => grid[b.ToTuple( )].Character = '#');
					//Console.WriteLine(grid);
				}

				if (rock.TryMove(Direction.Down, placed))
				{
					rock.Blocks.ForEach(b => grid[b.ToTuple()].Character = '.');
					rock.Move(Direction.Down);
					rock.Blocks.ForEach(b => grid[b.ToTuple()].Character = '#');
					//Console.WriteLine(grid);
				}
				else 
				{
					placed.Add(rock);
					break;
				}

			}
		}
		Console.WriteLine(grid);

		return placed.Max(r => r.Blocks.Max(v => v.Y + 1 )).ToString();
	}

	private static Vector2 GetSpawnPoint(Rock rock, List<Rock> placed)
	{

		if (!placed.Any( ))
			return new(2, 3);

		var maxY = placed.Max(r => r.Blocks.Max(v => v.Y));
		return new(2, maxY + 4 + Math.Abs(rock.Blocks.Min(r => r.Y)));
	}

	public override async Task<string> SolvePart2() => null;
}

internal enum Direction
{
	Left, Right, Down, Up
}

internal class Rock
{
	public List<Vector2> Blocks { get; set; }

	public bool Overlaps(Rock r) => Blocks.Any(r.Blocks.Contains);

	public void Spawn(Vector2 pos) => Blocks = Blocks.Select(b => b + pos).ToList( );

	public void Move(Direction direction)
	{
		Blocks = direction switch
		{
			Direction.Left => Blocks.Select(b => b - Vector2.UnitX).ToList( ),
			Direction.Right => Blocks.Select(b => b + Vector2.UnitX).ToList( ),
			Direction.Down => Blocks.Select(b => b - Vector2.UnitY).ToList( ),
			Direction.Up => Blocks.Select(b => b + Vector2.UnitY).ToList( ),
			_ => Blocks
		};
	}


	public bool TryMove(Direction direction, List<Rock> placed)
	{
		Move(direction);

		var canMove = !Blocks.Any(b => b.Y < 0) //not at floor
					  && !Blocks.Any(b => b.X < 0) // not at left wall
					  && !Blocks.Any(b => b.X > 6) // not at right wall
					  && !placed.Any(r => r.Overlaps(this)); // not overlapping with any other rocks


		var undo = direction switch
		{
			Direction.Left => Direction.Right,
			Direction.Right => Direction.Left,
			_ => Direction.Up
		};

		Move(undo);


		return canMove;
	}
}