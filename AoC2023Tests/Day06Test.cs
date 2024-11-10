namespace AoC2023Tests;

public class Day06Test
{
	Day06 day06;

	[SetUp]
	public void Setup( )
	{
		day06 = new Day06("day06test1");
	}
        
	[Test]
	public void Part1( )
	{
		var actual = day06.SolvePart1( ).Result;
		Assert.That(actual, Is.EqualTo("288"));
	}

	[Test]
	public void Part2( )
	{
		var actual = day06.SolvePart2( ).Result;
		Assert.That(actual, Is.EqualTo("71503"));
	}
}