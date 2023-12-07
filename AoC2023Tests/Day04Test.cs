using AoC2023;

namespace AoC2023Tests;

public class Day04Test
{
	Day04 day04;

	[SetUp]
	public void Setup( )
	{
		day04 = new Day04("day04test1");
	}
        
	[Test]
	public void Part1( )
	{
		var actual = day04.SolvePart1( );
		Assert.That(actual, Is.EqualTo("13"));
	}

	[Test]
	public void Part2( )
	{
		var actual = day04.SolvePart2( );
		Assert.That(actual, Is.EqualTo("30"));
	}
}