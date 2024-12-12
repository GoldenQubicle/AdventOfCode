namespace AoC2024Tests;

public class Day12Test
{
	[TestCaseSource(nameof(GetCasesPart1))]
	public async Task Part1((string file, string expected) testCase)
	{
		var day12 = new Day12(testCase.file);
		var actual = await day12.SolvePart1( );
		Assert.That(actual, Is.EqualTo(testCase.expected));
	}

	[TestCaseSource(nameof(GetCasesPart2))]
	public async Task Part2((string file, string expected) testCase)
	{
		var day12 = new Day12(testCase.file);
		var actual = await day12.SolvePart2( );
		Assert.That(actual, Is.EqualTo(testCase.expected));
	}

	private static IEnumerable<(string file, string expected)> GetCasesPart2()
	{
		yield return ("day12test1", "1206");
		yield return ("day12test2", "80");
		yield return ("day12test3", "236");
		yield return ("day12test4", "368");
	}

	private static IEnumerable<(string file, string expected)> GetCasesPart1()
	{
		yield return ("day12test1", "1930");
		yield return ("day12test5", "140");
		yield return ("day12test4", "1184");

	}
}
