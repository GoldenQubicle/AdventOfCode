namespace AoC2024Tests;
            
public class Day16Test
{
	[TestCaseSource(nameof(GetCasesPart1))]
	public async Task Part1((string file, string expected) testCase)
	{
		var day16 = new Day16(testCase.file);
		var actual = await day16.SolvePart1( );
		Assert.That(actual, Is.EqualTo(testCase.expected));
	}

	[TestCaseSource(nameof(GetCasesPart2))]
    public async Task Part2((string file, string expected) testCase)
    {
	    var day16 = new Day16(testCase.file);
		var actual = await day16.SolvePart2( );
        Assert.That(actual, Is.EqualTo(testCase.expected));
    }

    private static IEnumerable<(string file, string expected)> GetCasesPart1()
    {
	    yield return ("day16test1", "7036");
	    yield return ("day16test2", "11048");
    }

    private static IEnumerable<(string file, string expected)> GetCasesPart2()
    {
	    yield return ("45", "7036");
	    yield return ("64", "11048");
    }
}
