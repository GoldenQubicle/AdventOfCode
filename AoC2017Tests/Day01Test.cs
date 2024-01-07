namespace AoC2017Tests;

public class Day01Test
{
	[TestCase("1122", "3")]
	[TestCase("1111", "4")]
	[TestCase("1234", "0")]
	[TestCase("91212129", "9")]
	public async Task Part1(string input, string expected)
	{
		var day01 = new Day01(new List<string> { input });
		var actual = await day01.SolvePart1( );
		Assert.That(actual, Is.EqualTo(expected));
	}

	[TestCase("1212", "6")]
	[TestCase("1221", "0")]
	[TestCase("123425", "4")]
	[TestCase("123123", "12")]
	[TestCase("12131415", "4")]
	public async Task Part2(string input, string expected)
	{
		var day01 = new Day01(new List<string> { input });
		var actual = await day01.SolvePart2( );
		Assert.That(actual, Is.EqualTo(expected));
	}
}
