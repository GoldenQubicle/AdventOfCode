namespace AoC2017Tests;
            
public class Day13Test
{
    Day13 day13;
            
    [SetUp]
    public void Setup( )
    {
        day13 = new Day13("day13test1");
    }
    
    [Test]
    public async Task Part1( )
    {
        var actual = await day13.SolvePart1( );
        Assert.That(actual, Is.EqualTo("24"));
    }
            
    [Test]
    public async Task Part2( )
    {
        var actual = await day13.SolvePart2( );
        Assert.That(actual, Is.EqualTo("10"));
    }
}
