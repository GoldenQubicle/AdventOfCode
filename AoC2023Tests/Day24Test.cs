namespace AoC2023Tests;
            
public class Day24Test
{
    Day24 day24;
            
    [SetUp]
    public void Setup( )
    {
        day24 = new Day24("day24test1");
    }
    
    [Test]
    public async Task Part1( )
    {
        var actual = await day24.SolvePart1( );
        Assert.That(actual, Is.EqualTo(""));
    }
            
    [Test]
    public async Task Part2( )
    {
        var expected = string.Empty;
        var actual = await day24.SolvePart2( );
        Assert.That(actual, Is.EqualTo(expected));
    }
}
