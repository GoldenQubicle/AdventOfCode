using AoC2023;

namespace AoC2023Tests;
            
public class Day10Test
{
    [TestCase("day10test1", "4")]
    [TestCase("day10test2", "8")]
    public void Part1(string file, string expected )
    {
	    var day10 = new Day10(file);
        var actual = day10.SolvePart1( ).Result;
        Assert.That(actual, Is.EqualTo(expected));
    }

	[TestCase("day10test3", "4")]
	[TestCase("day10test4", "8")]
	[TestCase("day10test5", "4")]
	[TestCase("day10test6", "10")]
	public void Part2(string file, string expected)
	{
		var day10 = new Day10(file);
		var actual = day10.SolvePart2( ).Result;
        Assert.That(actual, Is.EqualTo(expected));
    }


}
