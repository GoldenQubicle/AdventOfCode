namespace AoC2021Tests;

public class Day15Test
{
	Day15 day15;

	[SetUp]
	public void Setup( )
	{
		day15 = new Day15("day15test1");
	}
        
	[Test]
	public void Part1( )
	{
		var actual = day15.SolvePart1( ).Result;
		Assert.AreEqual("40", actual);
	}

	[Test]
	public void Part2( )
	{
		var actual = day15.SolvePart2( ).Result;
		Assert.AreEqual("315", actual);
	}
}