namespace AoC2024Tests;
            
public class Day18Test
{
    Day18 day18;
            
    [SetUp]
    public void Setup( )
    {
	    Day18.Width = 7;
	    Day18.Height = 7;
	    Day18.Simulate = 12;

        day18 = new Day18("day18test1");
    }
    
    [Test]
    public async Task Part1( )
    {
        var actual = await day18.SolvePart1( );
        Assert.That(actual, Is.EqualTo("22"));
    }
            
    [Test]
    public async Task Part2( )
    {
        var actual = await day18.SolvePart2( );
        Assert.That(actual, Is.EqualTo("6,1"));
    }
}
