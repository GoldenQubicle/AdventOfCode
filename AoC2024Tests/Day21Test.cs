namespace AoC2024Tests;
            
public class Day21Test
{
    Day21 day21;
            
    [SetUp]
    public void Setup( )
    {
        day21 = new Day21("day21test1");
    }
    
    [Test]
    public async Task Part1( )
    {
        var actual = await day21.SolvePart1( );
        Assert.That(actual, Is.EqualTo("126384"));
    }
            
    [Test]
    public async Task Part2( )
    {
        var actual = await day21.SolvePart2( );
        Assert.That(actual, Is.EqualTo("154115708116294"));
    }

}
