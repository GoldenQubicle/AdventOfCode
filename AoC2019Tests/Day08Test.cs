using Common.Extensions;

namespace AoC2019Tests;
            
public class Day08Test
{
    Day08 day08;
            
    [SetUp]
    public void Setup( )
    {
	    Day08.Width = 3;
	    Day08.Height = 2;
		day08 = new Day08("day08test1");
    }
    
    [Test]
    public async Task Part1( )
    {
	    
        var actual = await day08.SolvePart1( );
        Assert.That(actual, Is.EqualTo("1"));
    }
            
    [Test]
    public async Task Part2( )
    {
		Day08.Width = 2;
		Day08.Height = 2;
		day08 = new Day08("day08test2");
		var actual = await day08.SolvePart2( );
        Assert.That(actual, Is.EqualTo("\n#.\n.#\n"));
    }
}
