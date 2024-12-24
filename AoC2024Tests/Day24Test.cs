namespace AoC2024Tests;
            
public class Day24Test
{
    Day24 day24;
            
    [SetUp]
    public void Setup( )
    {
        //day24 = new Day24("day24test1");
    }

	[TestCaseSource(nameof(GetCasesPart1))]
	public async Task Part1((string file, string expected) testCase)
    {
	    var day24 = new Day24(testCase.file);
		var actual = await day24.SolvePart1( );
        Assert.That(actual, Is.EqualTo(testCase.expected));
    }
            
    [Test]
    public async Task Part2( )
    {
        var actual = await day24.SolvePart2( );
        Assert.That(actual, Is.EqualTo(""));
    }

    private static IEnumerable<(string file, string expected)> GetCasesPart1()
    {
	    yield return ("day24test1", "4");
	    yield return ("day24test2", "2024");
    }
}
