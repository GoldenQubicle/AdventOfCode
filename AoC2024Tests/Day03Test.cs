namespace AoC2024Tests;
            
public class Day03Test
{
    [Test]
    public async Task Part1( )
    {
        var day03 = new Day03("day03test1");
		var actual = await day03.SolvePart1( );
        Assert.That(actual, Is.EqualTo("161"));
    }
            
    [Test]
    public async Task Part2( )
    {
        var day03 = new Day03("day03test2");
		var actual = await day03.SolvePart2( );
        Assert.That(actual, Is.EqualTo("48"));
    }
}
