using static AoC2022.Day19;

namespace AoC2022Tests;

using Blueprint = Dictionary<Resource, List<(Resource resource, int amount)>>;
using Resources = Dictionary<Resource, int>;

[Ignore("Not Finished")]
public class Day19Test
{
	Day19 day19;

	[SetUp]
	public void Setup()
	{
		day19 = new Day19("day19test1");
	}

	[Test]
	public async Task Part1()
	{
		var actual = await day19.SolvePart1( );
		Assert.That(actual, Is.EqualTo("33"));
	}

	[Test]
	public async Task Part2()
	{
		var expected = string.Empty;
		var actual = await day19.SolvePart2( );
		Assert.That(actual, Is.EqualTo(expected));
	}

	[TestCaseSource(nameof(ProductionTimeTestCases))]
	public async Task ProductionStateShouldCalculateTimeUntilGeodeRobot((Resources resources, Resources robots, int expected) test)
	{

		var state = new ProductionState(ProductionTimeTestBluePrint)
		{
			Resources = test.resources,
			Robots = test.robots
		};

		var actual = state.CalculateProductionTime(Resource.Geode);

		Assert.That(actual, Is.EqualTo(test.expected));
	}

	private Blueprint ProductionTimeTestBluePrint =>
		new( )
		{
			{ Resource.Ore,      new() { (Resource.Ore, 2) } },
			{ Resource.Clay,     new() { (Resource.Ore, 3) } },
			{ Resource.Obsidian, new() { (Resource.Ore, 4), (Resource.Clay, 5)} },
			{ Resource.Geode,    new() { (Resource.Ore, 6), (Resource.Obsidian, 7) } },
		};

	private static IEnumerable<(Resources resources, Resources robots, int expected)> ProductionTimeTestCases()
	{
		yield return (new( ) { { Resource.Ore, 6 }, { Resource.Obsidian, 7 } },
					  new( ) { { Resource.Geode, 1 } }, -1);

		yield return (new( ) { { Resource.Ore, 0 }, { Resource.Obsidian, 0 } },
					  new( ) { { Resource.Ore, 1 }, { Resource.Obsidian, 1 } }, 7);

		yield return (new( ) { { Resource.Ore, 0 }, { Resource.Obsidian, 0 } },
					  new( ) { { Resource.Ore, 2 }, { Resource.Obsidian, 3 } }, 3);
		
		yield return (new( ) { { Resource.Ore, 4 }, { Resource.Obsidian, 8 } },
				      new( ) { { Resource.Ore, 2 }, { Resource.Obsidian, 1 } }, 1);

		yield return (new( ) { { Resource.Ore, 0 }, { Resource.Clay, 0 }, { Resource.Obsidian, 0 } },
					  new( ) { { Resource.Ore, 1 }, { Resource.Clay, 1 }, { Resource.Obsidian, 0 } }, 12);

		yield return (new( ) { { Resource.Ore, 0 }, { Resource.Clay, 0 }, { Resource.Obsidian, 0 } },
					  new( ) { { Resource.Ore, 1 }, { Resource.Clay, 0 }, { Resource.Obsidian, 0 } }, 15);
	}

}
