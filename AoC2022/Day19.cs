namespace AoC2022;

public class Day19 : Solution
{
	private readonly List<Dictionary<Resource, List<(Resource r, int n)>>> blueprints;

	public Day19(string file) : base(file, split: "\n")
	{
		blueprints = Input
			.Select(l => Regex.Matches(l, @"(\d+)"))
			.Select(m =>

				new Dictionary<Resource, List<(Resource r, int n)>>
				{
					{ Resource.Ore , new() { (Resource.Ore, m.AsInt(1)) } },
					{ Resource.Clay , new() { (Resource.Ore, m.AsInt(2)) } },
					{ Resource.Obsidian , new () { (Resource.Ore, m.AsInt(3)), (Resource.Clay, m.AsInt(4)) } },
					{ Resource.Geode , new (){ (Resource.Ore, m.AsInt(5)), (Resource.Obsidian, m.AsInt(6)) } },
				}
			).ToList( );

	}


	public override async Task<string> SolvePart1()
	{
		var qualityLevels = new List<int>( );

		foreach (var bluePrint in blueprints.WithIndex( ))
		{
			var state = new ProductionState(bluePrint.Value);

			while (state.Minute <= 24)
			{
				state.Tick( );
			}

			qualityLevels.Add((bluePrint.idx + 1) * state.Resources[Resource.Geode]);
		}



		return qualityLevels.Sum( ).ToString( );
	}

	public override async Task<string> SolvePart2()
	{
		return string.Empty;
	}

	internal class ProductionState(Dictionary<Resource, List<(Resource resource, int amount)>> blueprint)
	{
		public int Minute { get; set; } = 1;
		internal Dictionary<Resource, int> Resources { get; init; } = new( )
		{
			{ Resource.Ore, 0 },{ Resource.Clay, 0 },{ Resource.Obsidian, 0 },{ Resource.Geode, 0 },
		};
		internal Dictionary<Resource, int> Robots { get; init; } = new( ) { { Resource.Ore, 1 } };
		internal Dictionary<Resource, List<(Resource resource, int amount)>> BluePrints { get; init; } = blueprint;
		internal List<Resource> NewRobots { get; set; } = new();


		public void Tick()
		{
			var sb = new StringBuilder( );
			sb.AppendLine($"==Minute {Minute}==");

			TryProducedRobots(sb);

			foreach (var robot in Robots)
			{
				Resources[robot.Key]++;
			}

			if (NewRobots.Count > 0)
			{
				NewRobots.ForEach(r => Robots[r] = Robots.ContainsKey(r) ? Robots[r]+1 : 1 );
				NewRobots.Clear( );
			}


			Robots.ForEach(r => sb.AppendLine($"{r.Value} {r.Key}-collecting robot"));
			Resources.ForEach(r => sb.Append($" {r.Value} {r.Key} "));
			sb.AppendLine();
			Console.WriteLine(sb.ToString());
			Minute++;
		}

		private void TryProducedRobots(StringBuilder sb)
		{
			foreach (var bluePrint in BluePrints.Reverse())
			{
				//this is obviously where the optimization needs to occur
				if (bluePrint.Value.All(b => Resources[b.resource] >= b.amount))
				{
					bluePrint.Value.ForEach(r =>
					{
						Resources[r.resource] -= r.amount;
						sb.Append($"Spend {r.amount} {r.resource}");
					});

					sb.Append($" to build {bluePrint.Key} robot");
					sb.AppendLine();
					NewRobots.Add(bluePrint.Key);
				}
			}
		}
	}



	internal enum Resource
	{
		Ore,
		Clay,
		Obsidian,
		Geode
	}
}
