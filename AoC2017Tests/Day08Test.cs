using AoC2017;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AoC2017Tests;
            
public class Day08Test
{
    Day08 day08;
            
    [SetUp]
    public void Setup( )
    {
        day08 = new Day08("day08test1");
    }
    
    [Test]
    public async Task Part1( )
    {
        var actual = await day08.SolvePart1( );
        Assert.That(actual, Is.EqualTo("1"));
    }
            
    [Test]
    public async Task Part2( )
    {
        var actual = await day08.SolvePart2( );
        Assert.That(actual, Is.EqualTo("10"));
    }
}
