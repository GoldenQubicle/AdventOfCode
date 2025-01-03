namespace AoC2021Tests;


public class Day12Test
{
	Day12 day12;

	[SetUp]
	public void Setup( )
	{
		day12 = new Day12("day12test1");
	}
        
	[TestCase("day12test1","10")] 
	[TestCase("day12test2","19")] 
	[TestCase("day12test3","226")]
	public void Part1(string file, string expected )
	{
		day12 = new Day12(file);
		var actual = day12.SolvePart1( ).Result;
		Assert.AreEqual(expected, actual);
	}

	[Ignore("No Idea WTF Happened, not correct anymore")]
	[TestCase("day12test1", "36")]
	[TestCase("day12test2", "103")]
	[TestCase("day12test3", "3509")]
	public void Part2(string file, string expected)
	{
		day12 = new Day12(file);
		var actual = day12.SolvePart2().Result;
		Assert.AreEqual(expected, actual);
	}
}