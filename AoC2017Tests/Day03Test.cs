namespace AoC2017Tests;

public class Day03Test
{


	[TestCase("1", "0")]
	[TestCase("12", "3")]
	[TestCase("23", "2")]
	[TestCase("1024", "31")]
	public async Task Part1(string input, string expected)
	{
		var day03 = new Day03(new List<string> { input });
		var actual = await day03.SolvePart1( );
		Assert.That(actual, Is.EqualTo(expected));
	}

	
}
