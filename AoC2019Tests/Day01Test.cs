namespace AoC2019Tests;

public class Day01Test
{
	Day01 day01;

	[SetUp]
	public void Setup( )
	{
		day01 = new Day01("day01");
	}
        
	[TestCase(12,2)] 
	[TestCase(14,2)] 
	[TestCase(1969,654)] 
	[TestCase(100756,33583)]
	public void GetFuelPerModule(int input, int expected)
	{
		var actual = Day01.GetFuelPerModule(input);
		Assert.AreEqual(expected, actual);
	}

	[Test]
	public void Part1( )
	{
		var actual = day01.SolvePart1( ).Result;
		Assert.AreEqual("3331849", actual);
	}

	[Test]
	public void Part2( )
	{
		var actual = day01.SolvePart2( ).Result;
		Assert.AreEqual("4994898", actual);
	}
}