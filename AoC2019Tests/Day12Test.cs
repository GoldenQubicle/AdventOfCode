namespace AoC2019Tests;
            
public class Day12Test
{
    [TestCase("day12test1", "179", 10)]
    [TestCase("day12test2", "1940", 100)]
    public async Task Part1(string file, string expected, int steps )
    {
		var day12 = new Day12(file);
		day12.Steps = steps;
		var actual = await day12.SolvePart1( );
        Assert.That(actual, Is.EqualTo(expected));
    }

	[TestCase("day12test1", "2772")]
	[TestCase("day12test2", "4686774924")]
	public async Task Part2(string file, string expected)
    {
		var day12 = new Day12(file);
		var actual = await day12.SolvePart2( );
        Assert.That(actual, Is.EqualTo(expected));
    }
}
