namespace AoC2024Tests;
            
public class Day02Test
{
    Day02 day02;
            
    [SetUp]
    public void Setup( )
    {
        day02 = new Day02("day02test1");
    }
    
    [Test]
    public async Task Part1( )
    {
        var actual = await day02.SolvePart1( );
        Assert.That(actual, Is.EqualTo("2"));
    }
            
    [Test]
    public async Task Part2( )
    {
        var actual = await day02.SolvePart2( );
        Assert.That(actual, Is.EqualTo("4"));
    }
}
