namespace AoC2019Tests;

internal class IntCodeComputerTests
{
	[TestCaseSource(nameof(GetDay1TestCases))]
	public void IntCodeProgramDay2Test((List<int> input, List<int> result) test)
	{
		var sut = new IntCodeComputer(test.input);
		sut.Execute();

		Assert.That(sut.Memory, Is.EqualTo(test.result));

	}

	public static IEnumerable<(List<int> input, List<int> result)> GetDay1TestCases()
	{
		yield return (new List<int> { 1, 0, 0, 0, 99 }, new List<int> { 2, 0, 0, 0, 99 });
		yield return (new List<int> { 2, 3, 0, 3, 99 }, new List<int> { 2, 3, 0, 6, 99 });
		yield return (new List<int> { 2, 4, 4, 5, 99, 0 }, new List<int> { 2, 4, 4, 5, 99, 9801 });
		yield return (new List<int> { 1, 1, 1, 4, 99, 5, 6, 0, 99 }, new List<int> { 30, 1, 1, 4, 2, 5, 6, 0, 99 });
	}
}