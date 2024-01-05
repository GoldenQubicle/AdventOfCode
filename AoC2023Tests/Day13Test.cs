using AoC2023;

namespace AoC2023Tests;
            
public class Day13Test
{
    Day13 day13;
            
    [SetUp]
    public void Setup( )
    {
        day13 = new Day13("day13test1");
    }
    
    [Test]
    public void Part1( )
    {
        var actual = day13.SolvePart1( ).Result;
        Assert.That(actual, Is.EqualTo("405"));
    }
            
    [Test]
    public void Part2( )
    {
        var actual = day13.SolvePart2( ).Result;
        Assert.That(actual, Is.EqualTo("400"));
    }
}
