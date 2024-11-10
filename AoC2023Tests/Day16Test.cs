namespace AoC2023Tests;
            
public class Day16Test
{
    Day16 day16;
            
    [SetUp]
    public void Setup( )
    {
        day16 = new Day16("day16test1");
    }
    
    [Test]
    public void Part1( )
    {
        var actual = day16.SolvePart1( ).Result;
        Assert.That(actual, Is.EqualTo("46"));
    }
            
    [Test]
    public void Part2( )
    {
        var actual = day16.SolvePart2( ).Result;
        Assert.That(actual, Is.EqualTo("51"));
    }
}
