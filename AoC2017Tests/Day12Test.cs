namespace AoC2017Tests;
            
public class Day12Test
{
    Day12 day12;
            
    [SetUp]
    public void Setup( )
    {
        day12 = new Day12("day12test1");
    }
    
    [Test]
    public async Task Part1( )
    {
        var actual = await day12.SolvePart1( );
        Assert.That(actual, Is.EqualTo("6"));
    }
            
    [Test]
    public async Task Part2( )
    {
        var actual = await day12.SolvePart2( );
        Assert.That(actual, Is.EqualTo("2"));
    }
}
