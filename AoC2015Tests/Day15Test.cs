using AoC2015;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AoC2015Tests
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
            var actual = day15.SolvePart1( ).Result;
            Assert.AreEqual("62842880", actual);
        }

        [Test]
        public void Part2( )
        {
            var actual = day15.SolvePart2( ).Result;
            Assert.AreEqual("57600000", actual);
        }
    }
}