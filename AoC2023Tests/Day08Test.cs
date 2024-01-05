using AoC2023;

namespace AoC2023Tests;
            
public class Day08Test
{
    [TestCase("day08test1","2")] 
    [TestCase("day08test2","6")]
    public void Part1(string input, string expected )
    {
	    var day08 = new Day08(input);
		var actual = day08.SolvePart1( ).Result;
        Assert.That(actual, Is.EqualTo(expected));
    }
            
    [Test]
    public void Part2( )
    {
	    var day08 = new Day08("day08test3");
		var actual = day08.SolvePart2( ).Result;
        Assert.That(actual, Is.EqualTo("6"));
    }
}
