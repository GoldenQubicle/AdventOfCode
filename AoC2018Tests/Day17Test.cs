namespace AoC2018Tests;
            
public class Day17Test
{
    Day17 day17;
            
    [SetUp]
    public void Setup( )
    {
        day17 = new Day17("day17test1");
    }
    
    [Test]
    public async Task Part1( )
    {
        var actual = await day17.SolvePart1( );
        Assert.That(actual, Is.EqualTo("57"));
    }
            
    [Test]
    public async Task Part2( )
    {
        var expected = string.Empty;
        var actual = await day17.SolvePart2( );
        Assert.That(actual, Is.EqualTo(expected));
    }
}
