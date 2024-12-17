namespace AoC2024Tests;
            
public class Day17Test
{
    Day17 day17;
            
    [SetUp]
    public void Setup( )
    {
        //day17 = new Day17("day17test1");
    }

	[TestCaseSource(nameof(GetCasesPart1))]
	public async Task Part1((string file, string expected) testCase)
    {
		var day17 = new Day17(testCase.file);
		var actual = await day17.SolvePart1( );
        Assert.That(actual, Is.EqualTo(testCase.expected));
    }
            
    [Test]
    public async Task Part2( )
    {
        var actual = await day17.SolvePart2( );
        Assert.That(actual, Is.EqualTo(""));
    }

    private static IEnumerable<(string file, string expected)> GetCasesPart1()
    {
	    yield return ("day17test1", "4,6,3,5,6,3,5,2,1,0");
	    yield return ("day17test3", "0,1,2");
	    yield return ("day17test4", "4,2,5,6,7,7,7,7,3,1,0");
    }
}
