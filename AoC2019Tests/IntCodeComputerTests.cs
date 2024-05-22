namespace AoC2019Tests;

internal class IntCodeComputerTests
{
	[TestCaseSource(nameof(GetDay2TestCases))]
	public void Day2Tests((List<int> input, List<int> result) test)
	{
		var sut = new IntCodeComputer(test.input);
		sut.Execute();

		Assert.That(sut.Memory, Is.EqualTo(test.result));

	}

	[TestCaseSource(nameof(GetDay5TestCases))]
	public void Day5Tests((List<int> input, List<int> result) test)
	{
		var sut = new IntCodeComputer(test.input);
		sut.Execute( );

		Assert.That(sut.Memory, Is.EqualTo(test.result));
	}

	[Test]
	public void Day5Diagnostics()
	{
		var day5 = new Day05("day05");
		var sut = new IntCodeComputer(IntCodeComputer.DayInputToIntInput(day5.Input))
		{
			Input = 1
		};
	}

	public static IEnumerable<(List<int> input, List<int> result)> GetDay5TestCases()
	{
		yield return (new List<int> { 1002, 4, 3, 4, 33 }, new List<int> { 1002, 4, 3, 4, 99 });
		//yield return (new List<int> { 2, 3, 0, 3, 99 }, new List<int> { 2, 3, 0, 6, 99 });
		//yield return (new List<int> { 2, 4, 4, 5, 99, 0 }, new List<int> { 2, 4, 4, 5, 99, 9801 });
		//yield return (new List<int> { 1, 1, 1, 4, 99, 5, 6, 0, 99 }, new List<int> { 30, 1, 1, 4, 2, 5, 6, 0, 99 });
	}

	public static IEnumerable<(List<int> input, List<int> result)> GetDay2TestCases()
	{
		yield return (new List<int> { 1, 0, 0, 0, 99 }, new List<int> { 2, 0, 0, 0, 99 });
		yield return (new List<int> { 2, 3, 0, 3, 99 }, new List<int> { 2, 3, 0, 6, 99 });
		yield return (new List<int> { 2, 4, 4, 5, 99, 0 }, new List<int> { 2, 4, 4, 5, 99, 9801 });
		yield return (new List<int> { 1, 1, 1, 4, 99, 5, 6, 0, 99 }, new List<int> { 30, 1, 1, 4, 2, 5, 6, 0, 99 });
	}

	[Test]
	public async Task Day2Solution()
	{
		var day2 = new Day02("day02");
		var part1 = await day2.SolvePart1();
		var part2 = await day2.SolvePart2();

		Assert.That(part1, Is.EqualTo("3790645"));
		Assert.That(part2, Is.EqualTo("6577"));
	}
}