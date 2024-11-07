namespace AoC2019Tests;

[Ignore("No Tests Today")]
public class Day11Test
{
    Day11 day11;
            
    [SetUp]
    public void Setup( )
    {
        day11 = new Day11("day11test1");
    }
    
    [Test]
    public async Task Part1( )
    {
        var actual = await day11.SolvePart1( );
        Assert.That(actual, Is.EqualTo(""));
    }
            
    [Test]
    public async Task Part2( )
    {
        var expected = string.Empty;
        var actual = await day11.SolvePart2( );
        Assert.That(actual, Is.EqualTo(expected));
    }
}
