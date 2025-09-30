namespace AoC2023Tests;


public class Day12Test
{
	Day12 day12;

	[SetUp]
	public void Setup()
	{
		day12 = new Day12("day12test1");
	}

	[TestCaseSource(nameof(GetArrangementCases))]
	public void GetArrangementsShouldReturnCount((string row, List<int> groups, int expected) test)
	{
		var actual = Day12.RecurseArrangement(test.row, test.groups, 0, 0, [ ]);
		Assert.That(actual, Is.EqualTo(test.expected));
	}

	[TestCaseSource(nameof(Part2_GetArrangementCases))]
	public void Part2_GetArrangementsShouldReturnCount((string row, List<int> groups, int expected) test)
	{
		var actual = Day12.RecurseArrangement(test.row, test.groups, 0, 0, [ ], isPart2: true);
		Assert.That(actual, Is.EqualTo(test.expected));
	}

	public static IEnumerable<(string row, List<int> groups, int expected)> Part2_GetArrangementCases()
	{
		yield return ("???.###", [1, 1, 3], 1);
		yield return (".??..??...?##.", [1, 1, 3], 16384);
		yield return ("?#?#?#?#?#?#?#?", [1, 3, 1, 6], 1);
		yield return ("????.#...#...", [4, 1, 1], 16);
		yield return ("????.######..#####.", [1, 6, 5], 2500);
		yield return ("?###????????", [3, 2, 1], 506250);
	}

	public static IEnumerable<(string row, List<int> groups, int expected)> GetArrangementCases()
	{
		yield return (".??", new List<int> { 1 }, 2);
		yield return (".#??", new List<int> { 2 }, 1);
		yield return (".??#", new List<int> { 2 }, 1);
		yield return (".??.", new List<int> { 1 }, 2);
		yield return (".???", new List<int> { 2 }, 2);
		yield return (".????", new List<int> { 2 }, 3);
		yield return (".????", new List<int> { 1 }, 4);
		yield return (".#??.", new List<int> { 3 }, 1);
		yield return (".#??.", new List<int> { 2 }, 1);
		yield return (".#???.", new List<int> { 2, 1 }, 1);
		yield return ("???.###", new List<int> { 1, 1, 3 }, 1);
		yield return (".??..??...?##.", new List<int> { 1, 1, 3 }, 4);
		yield return ("?#?#?#?#?#?#?#?", new List<int> { 1, 3, 1, 6 }, 1);
		yield return ("????.#...#...", new List<int> { 4, 1, 1 }, 1);
		yield return ("????.######..#####.", new List<int> { 1, 6, 5 }, 4);
		yield return ("?###????????", new List<int> { 3, 2, 1 }, 10);
		yield return ("?.????#####??#.?#??", new List<int> { 1, 1, 9, 1, 1 }, 2);
		yield return ("?.???#???.???.", new List<int> { 1, 4, 2 }, 15);
		yield return (".?????#?#??##.??????", new List<int> { 9, 3 }, 4);
		yield return (".?????#?#??##.??????", new List<int> { 9, 5 }, 2);
		yield return (".?????#?#??##.??????", new List<int> { 9, 6 }, 1);
		yield return (".?????#?#??##", new List<int> { 9 }, 1);
		yield return (".?????#?#??#?", new List<int> { 9 }, 2);
		yield return ("??????", new List<int> { 5 }, 2);
		yield return ("...??#???#.????", new List<int> { 6, 2 }, 3);
		yield return ("#??##.??????", new List<int> { 2, 2 }, 1);
	}

	[Test]
	public void Part1()
	{
		var actual = day12.SolvePart1( ).Result;
		Assert.That(actual, Is.EqualTo("21"));
	}

	[Test]
	public void Part2()
	{
		var actual = day12.SolvePart2( ).Result;
		Assert.That(actual, Is.EqualTo("525152"));
	}
}
