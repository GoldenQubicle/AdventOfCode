namespace AoC2019Tests;
            
public class Day07Test
{
    Day07 day07;
            
    [SetUp]
    public void Setup( )
    {
        day07 = new Day07("day07test1");
    }
    
    [Test]
    public async Task Part1( )
    {
        var actual = await day07.SolvePart1( );
        Assert.That(actual, Is.EqualTo(""));
    }
            
    [Test]
    public async Task Part2( )
    {
        var expected = string.Empty;
        var actual = await day07.SolvePart2( );
        Assert.That(actual, Is.EqualTo(expected));
    }
}
