namespace AoC2021Tests;
            
public class Day20Test
{
    Day20 day20;
            
    [SetUp]
    public void Setup( )
    {
        day20 = new Day20("day20test1");
    }
    
    [Test]
    public async Task Part1( )
    {
        var actual = await day20.SolvePart1( );
        Assert.That(actual, Is.EqualTo("35"));
    }
            
    [Test]
    public async Task Part2( )
    {
        var actual = await day20.SolvePart2( );
        Assert.That(actual, Is.EqualTo("3351"));
    }
}
