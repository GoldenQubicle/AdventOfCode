using AoC2015;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AoC2015Tests
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
            var actual = day06.SolvePart1( ).Result;
            Assert.AreEqual("4", actual);
        }

        [Test]
        public void Part2( )
        {
            day06 = new Day06("day06test2");
            var actual = day06.SolvePart2( ).Result;
            Assert.AreEqual("2000000", actual);
        }
    }
}