namespace AoC2021Tests;

public class Day07Test
{
	Day07 day07;

	[SetUp]
	public void Setup( )
	{
		day07 = new Day07("day07test1");
	}
        
	[Test]
	public void Part1( )
	{
		var actual = day07.SolvePart1( ).Result;
		Assert.AreEqual("37", actual);
	}

	[Test]
	public void Part2( )
	{
		var actual = day07.SolvePart2( ).Result;
		Assert.AreEqual("168", actual);
	}
}