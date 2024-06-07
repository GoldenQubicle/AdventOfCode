namespace AoC2019Tests;
            
public class Day12Test
{
    Day12 day12;
            
    [SetUp]
    public void Setup( )
    {
    }
    
    [TestCase("day12test1", "179", 10)]
    [TestCase("day12test2", "1940", 100)]
    public async Task Part1(string file, string expected, int steps )
    {
		var day12 = new Day12(file);
		day12.Steps = steps;
		var actual = await day12.SolvePart1( );
        Assert.That(actual, Is.EqualTo(expected));
    }
            
    [Test]
    public async Task Part2( )
    {
        var expected = string.Empty;
        var actual = await day12.SolvePart2( );
        Assert.That(actual, Is.EqualTo(expected));
    }
}
