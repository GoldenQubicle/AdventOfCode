namespace AoC2021Tests;

public class Day03Test
{
	Day03 day03;

	[SetUp]
	public void Setup( )
	{
		day03 = new Day03("day03test1");
	}
        
	[Test]
	public void Part1( )
	{
		var actual = day03.SolvePart1( ).Result;
		Assert.AreEqual("198", actual);
	}

	[Test]
	public void Part2( )
	{
		var actual = day03.SolvePart2( ).Result;
		Assert.AreEqual("230", actual);
	}
}