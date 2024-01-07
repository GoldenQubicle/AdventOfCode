using AoC2017;

namespace AoC2017Tests;
            
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
        Assert.That(actual, Is.EqualTo("tknk"));
    }
            
    [Test]
    public async Task Part2( )
    {
        var actual = await day07.SolvePart2( );
        Assert.That(actual, Is.EqualTo("60"));
    }
}
