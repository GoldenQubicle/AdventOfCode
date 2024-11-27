namespace AoC2023Tests;
            
public class Day25Test
{
    Day25 day25;
            
    [SetUp]
    public void Setup( )
    {
        day25 = new Day25("day25test1");
    }
    
    [Test]
    public async Task Part1( )
    {
        var actual = await day25.SolvePart1( );
        Assert.That(actual, Is.EqualTo("54"));
    }
            
    [Test]
    public async Task Part2( )
    {
        var expected = string.Empty;
        var actual = await day25.SolvePart2( );
        Assert.That(actual, Is.EqualTo(expected));
    }
}
