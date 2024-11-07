namespace AoC2022;

public class Day17 : Solution
{
	private static readonly CircularList<Func<Rock>> RockFactory = new( )
	{
		() => new(RockType.Horizontal) { Blocks = new() { new(0,0), new(1,0),  new(2,0),   new (3,0) }             },
		() => new(RockType.Cross) { Blocks = new() { new(0,0), new(1,0),  new(2,0),   new (1,1), new(1, -1) } },
		() => new(RockType.Jee) { Blocks = new() { new(0,0), new(1,0),  new(2,0),   new(2,1),  new(2,2) }   },
		() => new(RockType.Vertical) { Blocks = new() { new(0,0), new(0,-1), new(0,-2),  new(0,-3) }             },
		() => new(RockType.Square) { Blocks = new() { new(0,0), new(1,0),  new (0,-1), new (1, -1) }           }
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
		RockFactory.ResetHead( );
	}

	public override async Task<string> SolvePart1() => LetRocksFall( );


	public override async Task<string> SolvePart2()
	{
		var totalHeight = 0L;
		var prevHeight = 0f;
		var deltaHeights = new List<(Rock r, float h)>( );
		var state = new Dictionary<string, int>( );
		var hashLength = 35;

		LetRocksFall(KeepTrackOfState);

		return totalHeight.ToString( );

		bool KeepTrackOfState(Rock rock, List<Rock> rocksPlaced)
		{
			var height = rocksPlaced.Max(r => r.Blocks.Max(v => v.Y + 1));
			var dHeight = height - prevHeight;
			prevHeight = height;
			deltaHeights.Add((rock, dHeight));

			if (rocksPlaced.Count < hashLength * 3)
				return false;

			var hash = deltaHeights.TakeLast(hashLength)
				.Aggregate(new StringBuilder( ), (sb, r) => sb.Append(r.r.Type switch
				{
					RockType.Horizontal => $"-{r.h}",
					RockType.Cross => $"+{r.h}",
					RockType.Jee => $"J{r.h}",
					RockType.Vertical => $"|{r.h}",
					RockType.Square => $"#{r.h}",
					_ => throw new ArgumentOutOfRangeException( )
				})).ToString( );


			if (state.TryAdd(hash, rocksPlaced.Count))
				return false;

			//detected a repeating hash pattern
			//determine the cycle length from start & end to compute the cycle length & height
			var cycleStart = state[hash] - hashLength;
			var cycleEnd = rocksPlaced.Count - hashLength;
			var cycleLength = cycleEnd - cycleStart;
			var cycleHeight = deltaHeights[cycleStart..cycleEnd].Sum(r => r.h);

			//the total number of cycles, and rocks to be placed at the end
			var cycles = (1000000000000 - cycleStart) / cycleLength;
			var remaining = (1000000000000 - cycleStart) % cycleLength;

			//the height added over all cycles
			var total = cycles * (long)cycleHeight;

			//the height from the start until the cycle starts
			var first = (long)deltaHeights[..cycleStart].Sum(r => r.h);

			//the height at the end from the remaining rocks
			var last = (long)deltaHeights[cycleStart..(cycleStart + (int)remaining)].Sum(r => r.h);

			//the total height
			totalHeight = total + first + last;
			
			return true;
		}
	}


	private string LetRocksFall(Func<Rock, List<Rock>, bool> stateTracker = null)
	{
		const int totalRocks = 2022;
		var rockCount = 0;
		var rocksPlaced = new List<Rock>( );

		while (rockCount++ < totalRocks)
		{
			bool? cycleFound;

			var rocksToCheck = rocksPlaced.TakeLast(25).ToList( ); // no need to go over all rocks placed

			var rock = SpawnRock(rocksToCheck);

			while (true)
			{
				var push = GetJetPush( );
				
				if (rock.TryMove(push, rocksToCheck))
				{
					rock.Move(push);
				}

				if (rock.TryMove(Direction.Down, rocksToCheck))
				{
					rock.Move(Direction.Down);
				}
				else
				{
					rocksPlaced.Add(rock);
					cycleFound = stateTracker?.Invoke(rock, rocksPlaced);
					break;
				}
			}

			if (cycleFound.HasValue && cycleFound.Value)
				break;
		}

		return rocksPlaced.Max(r => r.Blocks.Max(v => v.Y + 1)).ToString( );
	}


	private Direction GetJetPush()
	{
		var push = jets.Current;
		jets.MoveRight( );
		return push;
	}


	private static Rock SpawnRock(List<Rock> rocksToCheck)
	{
		var rock = RockFactory.Current( );
		RockFactory.MoveRight( );
		rock.Spawn(GetSpawnPoint(rock, rocksToCheck));
		return rock;
	}

	private static Vector2 GetSpawnPoint(Rock rock, List<Rock> placed)
	{
		if (placed.Count == 0)
			return new(2, 3);

		var maxY = placed.Max(r => r.Blocks.Max(v => v.Y));
		return new(2, maxY + 4 + Math.Abs(rock.Blocks.Min(r => r.Y)));
	}


}

internal enum Direction
{
	Left, Right, Down, Up
}

internal enum RockType
{
	Horizontal, Cross, Jee, Vertical, Square
}

internal class Rock(RockType type)
{
	public RockType Type { get; } = type;
	public List<Vector2> Blocks { get; set; }

	public bool Overlaps(Rock r) => Blocks.Any(r.Blocks.Contains);

	public void Spawn(Vector2 pos) => Blocks = Blocks.Select(b => b + pos).ToList( );

	public void Move(Direction direction) => Blocks = direction switch
	{
		Direction.Left => Blocks.Select(b => b - Vector2.UnitX).ToList( ),
		Direction.Right => Blocks.Select(b => b + Vector2.UnitX).ToList( ),
		Direction.Down => Blocks.Select(b => b - Vector2.UnitY).ToList( ),
		Direction.Up => Blocks.Select(b => b + Vector2.UnitY).ToList( ),
		_ => Blocks
	};


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