namespace AoC2023Tests;

public class Day02Test
{
	Day02 day02;

	[SetUp]
	public void Setup( )
	{
		day02 = new Day02("day02test1");
	}
        
	[Test]
	public void Part1( )
	{
		var actual = day02.SolvePart1( ).Result;
		Assert.That(actual, Is.EqualTo("8"));
	}

	[Test]
	public void Part2( )
	{
		var actual = day02.SolvePart2( ).Result;
		Assert.That(actual, Is.EqualTo("2286"));
	}
}