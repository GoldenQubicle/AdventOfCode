namespace AoC2024Tests;
            
public class Day09Test
{
    Day09 day09;
            
    [SetUp]
    public void Setup( )
    {
        day09 = new Day09("day09test1");
    }
    
    [Test]
    public async Task Part1( )
    {
        var actual = await day09.SolvePart1( );
        Assert.That(actual, Is.EqualTo("1928"));
    }
            
    [Test]
    public async Task Part2( )
    {
        var actual = await day09.SolvePart2( );
        Assert.That(actual, Is.EqualTo("2858"));
    }
}
