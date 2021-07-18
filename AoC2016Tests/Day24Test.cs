using AoC2016;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AoC2016Tests
{
    public class Day24Test
    {
        Day24 day24;

        [SetUp]
        public void Setup( )
        {
            day24 = new Day24("day24test1");
        }
        
        [Test]
        public void Part1( )
        {
            var actual = day24.SolvePart1( );
            Assert.AreEqual("14", actual);
        }

        [Test]
        public void Part2( )
        {
            var actual = day24.SolvePart2( );
            Assert.AreEqual("", actual);
        }
    }
}