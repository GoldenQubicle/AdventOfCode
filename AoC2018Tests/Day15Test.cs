using AoC2018;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace AoC2018Tests;
            
public class Day15Test
{
    Day15 day15;
            
    [SetUp]
    public void Setup( )
    {
        day15 = new Day15("day15test1");
    }
    
    [TestCaseSource(nameof(GetCombatCases))]
    public void Part1((string file, int expected) testCase )
    {
        var day15 = new Day15(testCase.file);
		var actual = day15.SolvePart1( ).Result;
        Assert.That(actual, Is.EqualTo(testCase.expected.ToString()));
    }

    public static IEnumerable<(string path, int expected)> GetCombatCases()
    {
	    yield return ("day15test1", 27730);
	    yield return ("day15test2", 36334);
	    yield return ("day15test3", 39514);
	    yield return ("day15test4", 27755);
	    yield return ("day15test5", 28944);
	    yield return ("day15test6", 18740);
    }

    [Test]
    public void UnitOrderShouldBeCorrect()
    {
	    var day15 = new Day15("day15_unit_order");
	    var units = day15.GetUnits();
	    var expected = new List<Grid2d.Cell>
	    {
		    new((2, 1), 'G'),
		    new((4, 1), 'E'),
		    new((1, 2), 'E'),
		    new((3, 2), 'G'),
		    new((5, 2), 'E'),
		    new((2, 3), 'G'),
		    new((4, 3), 'E'),
	    };

        Assert.That(units, Is.EquivalentTo(expected));
    }

    [Test]
    public void UnitMovementShouldBeCorrect()
    {
	    var day15 = new Day15("day15_unit_movement");
	    var result = day15.SolvePart1();
    }


    [Test]
    public void Part2( )
    {
        var expected = string.Empty;
        var actual = day15.SolvePart2( ).Result;
        Assert.That(actual, Is.EqualTo(expected));
    }
}
