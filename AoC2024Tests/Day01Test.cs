namespace AoC2024Tests;
            
public class Day01Test
{
    Day01 day01;
            
    [SetUp]
    public void Setup( )
    {
        day01 = new Day01("day01test1");
    }
    
    [Test]
    public async Task Part1( )
    {
        var actual = await day01.SolvePart1( );
        Assert.That(actual, Is.EqualTo("11"));
    }
            
    [Test]
    public async Task Part2( )
    {
        var actual = await day01.SolvePart2( );
        Assert.That(actual, Is.EqualTo("31"));
    }
}
