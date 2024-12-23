namespace AoC2024Tests;
            
public class Day23Test
{
    Day23 day23;
            
    [SetUp]
    public void Setup( )
    {
        day23 = new Day23("day23test1");
    }
    
    [Test]
    public async Task Part1( )
    {
        var actual = await day23.SolvePart1( );
        Assert.That(actual, Is.EqualTo("7"));
    }
            
    [Test]
    public async Task Part2( )
    {
        var actual = await day23.SolvePart2( );
        Assert.That(actual, Is.EqualTo("co,de,ka,ta"));
    }
}
