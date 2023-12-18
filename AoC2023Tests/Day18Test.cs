using AoC2023;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AoC2023Tests;
            
public class Day18Test
{
    Day18 day18;
            
    [SetUp]
    public void Setup( )
    {
        day18 = new Day18("day18test1");
    }
    
    [Test]
    public void Part1( )
    {
        var actual = day18.SolvePart1( );
        Assert.That(actual, Is.EqualTo("62"));
    }
            
    [Test]
    public void Part2( )
    {
        var actual = day18.SolvePart2( );
        Assert.That(actual, Is.EqualTo("952408144115"));
    }
}
