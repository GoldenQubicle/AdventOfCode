namespace AoC2024Tests;
            
public class Day19Test
{
    Day19 day19;
            
    [SetUp]
    public void Setup( )
    {
        day19 = new Day19("day19test1");
    }
    
    [Test]
    public async Task Part1( )
    {
        var actual = await day19.SolvePart1( );
        Assert.That(actual, Is.EqualTo("6"));
    }
            
    [Test]
    public async Task Part2( )
    {
        var actual = await day19.SolvePart2( );
        Assert.That(actual, Is.EqualTo("16"));
    }
}
