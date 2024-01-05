using AoC2023;

namespace AoC2023Tests;

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
		Assert.That(actual, Is.EqualTo("4361"));
	}

	[Test]
	public void Part2( )
	{
		var actual = day03.SolvePart2( ).Result;
		Assert.That(actual, Is.EqualTo("467835"));
	}
}