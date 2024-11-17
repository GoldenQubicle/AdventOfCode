namespace AoC2023Tests;
            
public class Day22Test
{
    Day22 day22;
            
    [SetUp]
    public void Setup( )
    {
        day22 = new Day22("day22test1");
    }
    
    [Test]
    public async Task Part1( )
    {
        var actual = await day22.SolvePart1( );
        Assert.That(actual, Is.EqualTo(""));
    }
            
    [Test]
    public async Task Part2( )
    {
        var expected = string.Empty;
        var actual = await day22.SolvePart2( );
        Assert.That(actual, Is.EqualTo(expected));
    }
}
