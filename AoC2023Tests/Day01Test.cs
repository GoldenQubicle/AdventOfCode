namespace AoC2023Tests;

public class Day01Test
{
	[Test]
	public void Part1( )
	{
		var day01 = new Day01("day01test1");
		var actual = day01.SolvePart1( ).Result;
		Assert.That(actual, Is.EqualTo("142"));
	}

	[Test]
	public void Part2( )
	{
		var day01 = new Day01("day01test2");
		var actual = day01.SolvePart2( ).Result;
		Assert.That(actual, Is.EqualTo("281"));
	}
}