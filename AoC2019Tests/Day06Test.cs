namespace AoC2019Tests;
            
public class Day06Test
{
    [Test]
    public async Task Part1( )
    {
        var day06 = new Day06("day06test1");
		var actual = await day06.SolvePart1( );
        Assert.That(actual, Is.EqualTo("42"));
    }
            
    [Test]
    public async Task Part2( )
    {
		var day06 = new Day06("day06test2");
		var actual = await day06.SolvePart2( );
        Assert.That(actual, Is.EqualTo("4"));
    }
}
