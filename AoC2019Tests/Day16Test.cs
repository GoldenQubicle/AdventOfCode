namespace AoC2019Tests;

using static AoC2019.Day16;

public class Day16Test
{
	Day16 day16;

	[SetUp]
	public void Setup()
	{
		//day16 = new Day16("day16test1");
	}

	[TestCase("12345678", "01029498", 4)]
	[TestCase("80871224585914546619083218645595", "24176176", 100)]
	[TestCase("19617804207202209144916044189917", "73745418", 100)]
	[TestCase("69317163492948606335995924319873", "52432133", 100)]
	//[TestCase("11111111111111111111111111111111", "52432133", 100)]
	public async Task Part1(string input, string expected, int phases)
	{
		var day16 = new Day16(new List<string> { input }) { Phases = phases };

		var actual = await day16.SolvePart1( );
		Assert.That(actual, Is.EqualTo(expected));
	}

	[TestCase("03036732577212944063491565474664", "84462026", 100)]
	[TestCase("02935109699940807407585447034323", "78725270", 100)]
	[TestCase("03081770884921959731165446850517", "53553731", 100)]
	//[TestCase("11111111111111111111111111111111", "52432133", 5)]
	public async Task Part2(string input, string expected, int phases)
	{
		var day16 = new Day16(new List<string> { input }) { Phases = phases };
		var actual = await day16.SolvePart2( );
		Assert.That(actual, Is.EqualTo(expected));
	}

	[TestCaseSource(nameof(GetRangeTestCases))]
	public void GetRangeTest((int idx, double signal, List<Range> expected) test)
	{
		var actual = GetRanges(test.idx, test.signal);

		Assert.That(actual, Is.EqualTo(test.expected));
	}

	public static IEnumerable<(int idx, double signal, List<Range> expected)> GetRangeTestCases()
	{
		yield return (1, 8, new( ) { new(Sign.Plus, 0, 1), new(Sign.Minus, 2, 3), new(Sign.Plus, 4, 5), new(Sign.Minus, 6, 7) });
		yield return (2, 8, new( ) { new(Sign.Plus, 1, 3), new(Sign.Minus, 5, 7) });
		yield return (3, 8, new( ) { new(Sign.Plus, 2, 5) });
		yield return (2, 6, new( ) { new(Sign.Plus, 1, 3), new(Sign.Minus, 5, 7) });

	}
}
