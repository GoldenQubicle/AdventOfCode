using AoC2017;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AoC2017Tests;
            
public class Day01Test
{
    Day01 day01;
            
    [SetUp]
    public void Setup( )
    {
        day01 = new Day01("day01test1");
    }
    
    [TestCase("1122","3")] 
    [TestCase("1111","4")] 
    [TestCase("1234","0")] 
    [TestCase("91212129","9")]
    public async Task Part1(string input, string expected )
    {
        day01 = new Day01(new List<string> { input } );
        var actual = await day01.SolvePart1( );
        Assert.That(actual, Is.EqualTo(expected));
    }
            
    [Test]
    public async Task Part2( )
    {
        var expected = string.Empty;
        var actual = await day01.SolvePart2( );
        Assert.That(actual, Is.EqualTo(expected));
    }
}
