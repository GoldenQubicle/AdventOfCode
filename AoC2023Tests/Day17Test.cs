namespace AoC2023Tests;

[Ignore("not finished")]
public class Day17Test
{
    Day17 day17;
            
    [SetUp]
    public void Setup( )
    {
        day17 = new Day17("day17test1");
    }
    
    [Test]
    public void Part1( )
    {
        var actual = day17.SolvePart1( ).Result;
        Assert.That(actual, Is.EqualTo("102"));
    }
            
    [Test]
    public void Part2( )
    {
        var expected = string.Empty;
        var actual = day17.SolvePart2( ).Result;
        Assert.That(actual, Is.EqualTo(expected));
    }
}
