namespace AoC2024Tests;
            
public class Day10Test
{
    Day10 day10;
            
    [SetUp]
    public void Setup( )
    {
        day10 = new Day10("day10test1");
    }
    
    [Test]
    public async Task Part1( )
    {
        var actual = await day10.SolvePart1( );
        Assert.That(actual, Is.EqualTo("36"));
    }
            
    [Test]
    public async Task Part2( )
    {
	    day10 = new Day10("day10test1");
		var actual = await day10.SolvePart2( );
        Assert.That(actual, Is.EqualTo("81"));
    }
}
