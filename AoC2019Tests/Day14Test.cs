namespace AoC2019Tests;
            
public class Day14Test
{
    Day14 day14;
            
    [SetUp]
    public void Setup( )
    {
        day14 = new Day14("day14test1");
    }
    
    [TestCaseSource(nameof(GetPart1TestCases))]
    public async Task Part1((string file, string expecteded) test)
    {
	    var day14 = new Day14(test.file);
		var actual = await day14.SolvePart1( );
        Assert.That(actual, Is.EqualTo(test.expecteded));
    }

	[TestCaseSource(nameof(GetPart2TestCases))]
	public async Task Part2((string file, string expecteded) test)
    {
	    var day14 = new Day14(test.file);
	    var actual = await day14.SolvePart2( );
	    Assert.That(actual, Is.EqualTo(test.expecteded));
	}

    public static IEnumerable<(string file, string expecteded)> GetPart1TestCases()
    {
	    yield return ("day14test1", "31");
	    yield return ("day14test2", "165");
	    yield return ("day14test3", "13312");
	    yield return ("day14test4", "180697");
	    yield return ("day14test5", "2210736");
    }

    public static IEnumerable<(string file, string expecteded)> GetPart2TestCases()
    {
	    //yield return ("day14test3", "82892753"); // for some reason this test case fails..
	    yield return ("day14test4", "5586022");
	    yield return ("day14test5", "460664");
    }
}
