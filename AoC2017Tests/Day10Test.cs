namespace AoC2017Tests;
            
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
	    Day10.List = new () { 0, 1, 2, 3, 4 };

        var actual = await day10.SolvePart1( );
        Assert.That(actual, Is.EqualTo("12"));
    }
            
    [Test]
    public async Task Part2( )
    {
        var expected = string.Empty;
        var actual = await day10.SolvePart2( );
        Assert.That(actual, Is.EqualTo(expected));
    }
}
