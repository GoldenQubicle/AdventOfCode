namespace AoC2018Tests;
            
public class Day18Test
{
    Day18 day18;
            
    [SetUp]
    public void Setup( )
    {
        day18 = new Day18("day18test1");
    }
    
    [Test]
    public async Task Part1( )
    {
        var actual = await day18.SolvePart1( );
        Assert.That(actual, Is.EqualTo("1147"));
    }

    [Ignore("No test case for part 2")]
    [Test]
    public async Task Part2( )
    {
        var expected = string.Empty;
        var actual = await day18.SolvePart2( );
        Assert.That(actual, Is.EqualTo(expected));
    }
}
