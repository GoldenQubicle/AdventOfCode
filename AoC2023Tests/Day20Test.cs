namespace AoC2023Tests;
            
public class Day20Test
{
    Day20 day20;
   
    [TestCase("day20test1", "32000000")]
    [TestCase("day20test2", "11687500")]

    public async Task Part1(string file, string expected )
    {
		var day20 = new Day20(file);
		var actual = await day20.SolvePart1( );
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Ignore("No testcase for part 2")]
    [Test]
    public async Task Part2( )
    {
        var expected = string.Empty;
        var actual = await day20.SolvePart2( );
        Assert.That(actual, Is.EqualTo(expected));
    }
}
