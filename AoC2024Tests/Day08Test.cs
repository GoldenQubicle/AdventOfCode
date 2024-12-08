namespace AoC2024Tests;

public class Day08Test
{
	Day08 day08;

	[SetUp]
	public void Setup()
	{
		day08 = new Day08("day08test1");
	}

	[Test]
	public async Task Part1()
	{
		var actual = await day08.SolvePart1( );
		Assert.That(actual, Is.EqualTo("14"));
	}

	[Test]
	public async Task Part2()
	{
		var actual = await day08.SolvePart2( );
		Assert.That(actual, Is.EqualTo("34"));
	}

	[Test]
	public async Task Part2Test2()
	{
		var day08 = new Day08("day08test2");

		var actual = await day08.SolvePart2( );
		Assert.That(actual, Is.EqualTo("9"));
	}
}
