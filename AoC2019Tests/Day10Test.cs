namespace AoC2019Tests;

public class Day10Test
{

	[TestCaseSource(nameof(Day10Part1TestCases))]
	public async Task Part1((string file, string expected) test)
	{
		var day10 = new Day10(test.file);
		var actual = await day10.SolvePart1( );
		Assert.That(actual, Is.EqualTo(test.expected));
	}

	[Test]
	public async Task Part2()
	{
		var day10 = new Day10("day10test5");
		var actual = await day10.SolvePart2( );
		Assert.That(actual, Is.EqualTo("802"));
	}

	public static IEnumerable<(string file, string expected)> Day10Part1TestCases()
	{
		yield return ("day10test1", "8");
		yield return ("day10test2", "33");
		yield return ("day10test3", "35");
		yield return ("day10test4", "41");
		yield return ("day10test5", "210");
		yield return ("day10test6", "4");
	}
}
