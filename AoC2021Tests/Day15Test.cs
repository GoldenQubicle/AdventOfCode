using AoC2021;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021Tests
{
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
            var actual = day15.SolvePart1( );
            Assert.AreEqual("40", actual);
        }

        [Test]
        public void Part2( )
        {
            var actual = day15.SolvePart2( );
            Assert.AreEqual("", actual);
        }
    }
}