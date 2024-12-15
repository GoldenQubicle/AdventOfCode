namespace AoC2024Tests;
            
public class Day15Test
{
	[TestCaseSource(nameof(GetCasesPart1))]
	public async Task Part1((string file, string expected) testCase)
    {
	    var day15 = new Day15(testCase.file);
		var actual = await day15.SolvePart1( );
        Assert.That(actual, Is.EqualTo(testCase.expected));
    }
            
    [Test]
    public async Task Part2( )
    {
		var day15 = new Day15("day15test2");
		var actual = await day15.SolvePart2( );
        Assert.That(actual, Is.EqualTo("9021"));
    }

    private static IEnumerable<(string file, string expected)> GetCasesPart1()
    {
	    yield return ("day15test1", "2028");
	    yield return ("day15test2", "10092");
    }
}
