using Common.Extensions;

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
        Assert.That(actual, Is.EqualTo(""));
    }

    [Test]
    public void Checksum()
    {
	    var fileSystem = "00992111777.44.333....5555.6666.....8888..";
	    var sum = 0;
	    for (int i = 0; i < fileSystem.Length; i++)
	    {
            if (fileSystem[i] is '.') continue;
		    sum += (i * fileSystem[i].ToInt());
	    }

        Assert.That(sum, Is.EqualTo(1928));
    }
}
