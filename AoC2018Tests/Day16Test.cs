namespace AoC2018Tests;
            
public class Day16Test
{
    Day16 day16;
            
    [SetUp]
    public void Setup( )
    {
        day16 = new Day16("day16test1");
    }
    
    [Test]
    public async Task Part1( )
    {
        var actual = await day16.SolvePart1( );
        Assert.That(actual, Is.EqualTo("1"));
    }

    [Ignore("Not Finished")]
    [Test]
    public async Task Part2( )
    {
        var expected = string.Empty;
        var actual = await day16.SolvePart2( );
        Assert.That(actual, Is.EqualTo(expected));
    }
}
