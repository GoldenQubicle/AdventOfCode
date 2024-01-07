namespace AoC2017Tests;
            
public class Day06Test
{
    Day06 day06;
            
    [SetUp]
    public void Setup( )
    {
        day06 = new Day06("day06test1");
    }
    
    [Test]
    public async Task Part1( )
    {
        var actual = await day06.SolvePart1( );
        Assert.That(actual, Is.EqualTo("5"));
    }
            
    [Test]
    public async Task Part2( )
    {
        var actual = await day06.SolvePart2( );
        Assert.That(actual, Is.EqualTo("4"));
    }
}
