using AoC2023;

namespace AoC2023Tests;
            
public class Day11Test
{
    Day11 day11;
            
    [SetUp]
    public void Setup( )
    {
        day11 = new Day11("day11test1");
    }
    
    [Test]
    public void Part1( )
    {
        var actual = day11.SolvePart1( ).Result;
        Assert.That(actual, Is.EqualTo("374"));
    }
            
    [TestCase(10, "1030")]
    [TestCase(100, "8410")]
    public void Part2(int factor, string expected )
    {
        var actual = day11.GetDistanceSum(factor);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("day11test2", 2, "6")]
    [TestCase("day11test2", 3, "8")]
    [TestCase("day11test2", 4, "10")]
    [TestCase("day11test3", 2, "4")]
	public void ExpansionsFactor(string file, int factor, string expected)
    {
	    day11 = new Day11(file);
		var actual = day11.GetDistanceSum(factor );
	    Assert.That(actual, Is.EqualTo(expected));
    }
}
