using AoC2018;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AoC2018Tests
{
    public class Day06Test
    {
        Day06 day06;

        [SetUp]
        public void Setup( )
        {
            day06 = new Day06("day06test1");
        }
        
        [Test]
        public void Part1( )
        {
	        Console.SetOut(TestContext.Out);
            var actual = day06.SolvePart1( );
            Assert.That(actual, Is.EqualTo("17"));
        }

        [Test]
        public void Part2( )
        {
            var expected = string.Empty;
            var actual = day06.SolvePart2( );
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}