using AoC2016;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AoC2016Tests
{
    public class Day12Test
    {
        Day12 day12;

        [SetUp]
        public void Setup( )
        {
            day12 = new Day12("day12test1");
        }
        
        [Test]
        public void Part1( )
        {
            var actual = day12.SolvePart1( );
            Assert.AreEqual("42", actual);
        }

        [Test]
        public void Part2( )
        {
            var actual = day12.SolvePart2( );
            Assert.AreEqual("", actual);
        }
    }
}