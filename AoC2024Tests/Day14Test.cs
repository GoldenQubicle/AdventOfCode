namespace AoC2024Tests;
            
public class Day14Test
{
    Day14 day14;
            
    [SetUp]
    public void Setup( )
    {
        day14 = new Day14("day14test1");
        Day14.Width = 11;
        Day14.Height = 7;
	}
    
    [Test]
    public async Task Part1( )
    {
	    
        var actual = await day14.SolvePart1( );
        Assert.That(actual, Is.EqualTo("12"));
    }
            
    [Test]
    public async Task Part2( )
    {
        var actual = await day14.SolvePart2( );
        Assert.That(actual, Is.EqualTo(""));
    }
}
