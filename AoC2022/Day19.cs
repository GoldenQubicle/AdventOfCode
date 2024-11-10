namespace AoC2022;

public class Day19 : Solution
{
	public readonly List<Blueprint> Blueprints;

	public Day19(string file) : base(file, split: "\n")
	{
		Blueprints = Input.Select(ToBluePrint).ToList( );
	}


	public Blueprint ToBluePrint(string line)
	{
		var matches = Regex.Matches(line, @"(\d+)");
		return new Blueprint
		{
			{ Resource.Ore, new() { (Resource.Ore, matches.AsInt(1)) } },
			{ Resource.Clay, new() { (Resource.Ore, matches.AsInt(2)) } },
			{ Resource.Obsidian, new() { (Resource.Ore, matches.AsInt(3)), (Resource.Clay, matches.AsInt(4)) } },
			{ Resource.Geode, new() { (Resource.Ore, matches.AsInt(5)), (Resource.Obsidian, matches.AsInt(6)) } },
		};
	}


	public override async Task<string> SolvePart1()
	{
		var qualityLevels = new List<int>( );

		foreach (var bluePrint in Blueprints.WithIndex( ))
		{
			var qualityLevel = 0;
			var state = new ProductionState(bluePrint.Value);
			var queue = new PriorityQueue<ProductionState, int>( );
			queue.Enqueue(state, state.CalculateProductionTime(Resource.Geode));
			//Console.WriteLine($"Start processing Blueprint {bluePrint.idx + 1}");
			while (queue.TryDequeue(out var current, out var priority))
			{
				//Console.WriteLine($"queue count: {queue.Count} with current priority {priority}");
				//Console.WriteLine(current.Print());
				if (current.Minute == 25)
				{
					var level = current.Resources[Resource.Geode] * (bluePrint.idx + 1);
					qualityLevel = level > qualityLevel ? level : qualityLevel;
					//Console.WriteLine($"Found level {level}, qualityLevel is now {qualityLevel}");
					continue;
				};

				if (current.NoPointContinuing(qualityLevel))
				{
					continue;
				}

				var states = current.GetOptions( ).Select(r =>
				{
					var newState = current with
					{
						BluePrints = current.BluePrints,
						Minute = current.Minute,
						Resources = new(current.Resources),
						Robots = new(current.Robots),
						NewRobot = new( )
					};
					newState.ProducedRobot(r);
					return newState;
				}).ToList( ).Expand(current);

				foreach (var s in states)
				{
					s.Tick( );

					queue.Enqueue(s, s.CalculateProductionTime(Resource.Geode));
				}

			}

			qualityLevels.Add(qualityLevel);
		}


		//402 too low
		//617 too low
		return qualityLevels.Sum( ).ToString( );
	}

	public override async Task<string> SolvePart2()
	{
		return string.Empty;
	}

	public record ProductionState(Blueprint blueprint)
	{
		public int Minute { get; set; } = 1;
		public Resources Resources { get; set; } = new( )
		{
			{ Resource.Ore, 0 },{ Resource.Clay, 0 },{ Resource.Obsidian, 0 },{ Resource.Geode, 0 },
		};
		public Resources Robots { get; set; } = new( )
		{
			{ Resource.Ore, 1 },{ Resource.Clay, 0 },{ Resource.Obsidian, 0 },{ Resource.Geode, 0 },
		};

		internal Blueprint BluePrints { get; init; } = blueprint;
		public List<Resource> NewRobot { get; set; } = new( );

		public string Print()
		{
			var sb = new StringBuilder();
			sb.AppendLine($"Minute {Minute} | Time to Geode {CalculateProductionTime(Resource.Geode)} ");
			sb.AppendLine($"Robots: Ore {Robots[Resource.Ore]} | Clay {Robots[Resource.Clay]} | Obsidian {Robots[Resource.Obsidian]}  | Geode {Robots[Resource.Geode]} |");
			sb.AppendLine($"Resources: Ore {Resources[Resource.Ore]} | Clay {Resources[Resource.Clay]} | Obsidian {Resources[Resource.Obsidian]}  | Geode {Resources[Resource.Geode]} |");
			sb.AppendLine("---------------------------------------------------------");
			return sb.ToString();
		}

		public void Tick()
		{
			foreach (var robot in Robots)
			{
				Resources[robot.Key] += robot.Value;
			}

			NewRobot.ForEach(r => Robots[r] += 1);
			NewRobot.Clear( );
			Minute++;
		}

		public bool NoPointContinuing(int qualityLevel)
		{
			// so theory is, the answer to blueprint 2 cannot be found because I was pruning the branch too early
			// however by increasing the magic 25 to 35 the running time has increase to be unmanageable
			// in other words, the trick is to come up with a good pruning strategy
			//return false;
			var timeTillGeode = CalculateProductionTime(Resource.Geode);
			var potentialGeodes = 25 - (Minute + timeTillGeode);
			//return  Minute + timeTillGeode > 25 ;
			
			return (Robots[Resource.Ore] > 5 ||   Robots[Resource.Geode] == 0 && 32 - Minute <= qualityLevel);
		}

		public List<Resource> GetOptions()
		{
			var possible = Enum.GetValues<Resource>().Where(CanProduceRobot).ToList();
			if (possible.Contains(Resource.Ore) && Robots[Resource.Ore] == BluePrints[Resource.Ore].First().amount)
			{
				possible.Remove(Resource.Ore);
			}

			return possible;
		}


		public int CalculateProductionTime(Resource robot)
		{
			if (CanProduceRobot(robot))
				return 0 - Robots[robot];

			var isProduced = BluePrints[robot].All(r => Robots[r.resource] > 0);

			if (isProduced)
			{
				return BluePrints[robot].Max(r => (r.amount - Resources[r.resource]) / Robots[r.resource]);
			}

			//so this is N ticks until the missing robot(s) are ready
			//HOWEVER, production of said robot(s) consumes resources, which in turn has an effect on the production time of geode robot
			//in other words, to calculate it properly, the state would need to be forwarder, all options considered recursively, etc
			//at the moment I'll keep it like this as a heuristic to use as priority in the queue, not as actual time.
			var ticksUntilReady = BluePrints[robot]
				.Where(r => Robots[r.resource] == 0)
				.Select(r => CalculateProductionTime(r.resource) + r.amount)
				.Max( );

			return ticksUntilReady;

			return -1;
		}

		private bool CanProduceRobot(Resource robot) =>
			BluePrints[robot].All(r => Resources[r.resource] >= r.amount);


		public void ProducedRobot(Resource robot)
		{
			foreach (var (resource, amount) in BluePrints[robot])
			{
				Resources[resource] -= amount;
			}

			NewRobot.Add(robot);
		}
	}



	public enum Resource
	{
		Ore,
		Clay,
		Obsidian,
		Geode
	}
}
