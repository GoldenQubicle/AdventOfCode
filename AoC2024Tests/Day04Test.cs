namespace AoC2024Tests;
            
public class Day04Test
{
    Day04 day04;
            
    [SetUp]
    public void Setup( )
    {
        day04 = new Day04("day04test1");
    }
    
    [Test]
    public async Task Part1( )
    {
        var actual = await day04.SolvePart1( );
        Assert.That(actual, Is.EqualTo("18"));
    }
            
    [Test]
    public async Task Part2( )
    {
        var actual = await day04.SolvePart2( );
        Assert.That(actual, Is.EqualTo("9"));
    }
}
