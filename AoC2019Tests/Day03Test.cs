using System.Numerics;

namespace AoC2019Tests;

public class Day03Test
{
	[TestCaseSource(nameof(GetTestCasesPart1))]
	public async Task Part1((string file, string expected) test)
	{
		var day03 = new Day03(test.file);
		var actual = await day03.SolvePart1( );
		Assert.That(actual, Is.EqualTo(test.expected));
	}


	[TestCaseSource(nameof(GetTestCasesPart2))]
	public async Task Part2((string file, string expected) test)
	{
		var day03 = new Day03(test.file);
		var actual = await day03.SolvePart2( );
		Assert.That(actual, Is.EqualTo(test.expected));
	}


	[TestCaseSource(nameof(GetSegmentIntersectionTestCases))]
	public async Task SegmentIntersectionShouldBeCorrect((Day03.Segment s1, Day03.Segment s2, Vector2 expected) test)
	{
		var actual = test.s1.TryGetIntersection(test.s2);

		Assert.That(actual, Is.EqualTo(test.expected));
	}

	public static IEnumerable<(string file, string expected)> GetTestCasesPart1()
	{
		yield return ("day03test1", "6");
		yield return ("day03test2", "159");
		yield return ("day03test3", "135");
	}

	public static IEnumerable<(string file, string expected)> GetTestCasesPart2()
	{
		yield return ("day03test1", "30");
		yield return ("day03test2", "610");
		yield return ("day03test3", "410");
	}

	public static IEnumerable<(Day03.Segment s1, Day03.Segment s2, Vector2 expected)> GetSegmentIntersectionTestCases()
	{
		yield return (new(0, new(2, 6), new(6, 6)), new(1, new(4, 2), new(4, 8)), new(4, 6));
		yield return (new(0, new(4, 2), new(4, 8)), new(1, new(2, 6), new(6, 6)), new(4, 6));
		yield return (new(0, new(4, 2), new(4, 8)), new(1, new(6, 0), new(6, 6)), new(0, 0));
	}
}
