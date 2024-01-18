namespace AoC2021Tests;

public class Day14Test
{
	Day14 day14;

	[SetUp]
	public void Setup( )
	{
		day14 = new Day14("day14test1");
	}
        
	[Test]
	public void Part1( )
	{
		var actual = day14.SolvePart1( ).Result;
		Assert.AreEqual("1588", actual);
	}

	[Test]
	public void Part2( )
	{
		var actual = day14.SolvePart2( ).Result;
		Assert.AreEqual("2188189693528", actual);
	}
}