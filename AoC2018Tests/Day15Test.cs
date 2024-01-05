using AoC2018;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AoC2018Tests;
            
public class Day15Test
{
    Day15 day15;
            
    [SetUp]
    public void Setup( )
    {
        day15 = new Day15("day15test1");
    }
    
    [Test]
    public void Part1( )
    {
        var actual = day15.SolvePart1( ).Result;
        Assert.That(actual, Is.EqualTo(""));
    }

    public IEnumerable<(string path, int expected)> GetCombatCases()
    {
	    yield return ("day15test1", 27730);
	    yield return ("day15test2", 36334);
	    yield return ("day15test3", 39514);
	    yield return ("day15test4", 27755);
	    yield return ("day15test5", 28944);
	    yield return ("day15test6", 18740);
    }

            
    [Test]
    public void Part2( )
    {
        var expected = string.Empty;
        var actual = day15.SolvePart2( ).Result;
        Assert.That(actual, Is.EqualTo(expected));
    }
}
