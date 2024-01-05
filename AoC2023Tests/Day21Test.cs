using AoC2023;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AoC2023Tests;

[Ignore("not finished")]
public class Day21Test
{
    Day21 day21;
            
    [SetUp]
    public void Setup( )
    {
        day21 = new Day21("day21test1");
    }
    
    [Test]
    public void Part1( )
    {
	    Day21.Steps = 6;
        var actual = day21.SolvePart1( ).Result;
        Assert.That(actual, Is.EqualTo("16"));
    }

	[TestCase(6, 16)]
	[TestCase(10, 50)]
	[TestCase(50, 1594)]
	[TestCase(100, 6536)]
	[TestCase(500, 167004)]
	[TestCase(1000, 668697)]
	[TestCase(5000, 16733044)]
	public void Part2(int steps, int expected )
	{
		Day21.Steps = steps;
        var actual = day21.SolvePart2( ).Result;
        Assert.That(actual, Is.EqualTo(expected.ToString()));
    }
}
