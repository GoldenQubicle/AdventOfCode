namespace AoC2024Tests;
            
public class Day05Test
{
    Day05 day05;
            
    [SetUp]
    public void Setup( )
    {
        day05 = new Day05("day05test1");
    }
    
    [Test]
    public async Task Part1( )
    {
        var actual = await day05.SolvePart1( );
        Assert.That(actual, Is.EqualTo("143"));
    }
            
    [Test]
    public async Task Part2( )
    {
        var actual = await day05.SolvePart2( );
        Assert.That(actual, Is.EqualTo("123"));
    }
}
