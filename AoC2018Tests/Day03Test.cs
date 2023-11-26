using AoC2018;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AoC2018Tests
{
    public class Day03Test
    {
        Day03 day03;

        [SetUp]
        public void Setup( )
        {
            day03 = new Day03("day03test1");
        }
        
        [Test]
        public void Part1( )
        {
            var actual = day03.SolvePart1( );
            Assert.That(actual, Is.EqualTo("4"));
        }

        [Test]
        public void Part2( )
        {
            var actual = day03.SolvePart2( );
            Assert.That(actual, Is.EqualTo("3"));
        }
    }
}