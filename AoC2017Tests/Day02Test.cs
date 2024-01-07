namespace AoC2017Tests;
            
public class Day02Test
{
	[Test]
    public async Task Part1( )
    {
	    var day02 = new Day02("day02test1");
		var actual = await day02.SolvePart1( );
        Assert.That(actual, Is.EqualTo("18"));
    }
            
    [Test]
    public async Task Part2( )
    {
		var day02 = new Day02("day02test2");
		var actual = await day02.SolvePart2( );
        Assert.That(actual, Is.EqualTo("9"));
    }
}
