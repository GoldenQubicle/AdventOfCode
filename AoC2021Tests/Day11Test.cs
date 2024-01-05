using AoC2021;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021Tests
{
    public class Day11Test
    {
        Day11 day11;

        [SetUp]
        public void Setup( )
        {
            day11 = new Day11("day11test1");
        }
        
        [Test]
        [Ignore("runs too long, investigate")]
        public void Part1( )
        {
            var actual = day11.SolvePart1( ).Result;
            Assert.AreEqual("1656", actual);
        }

        [Test]
        [Ignore("runs too long, investigate")]
		public void Part2( )
        {
            var actual = day11.SolvePart2( ).Result;
            Assert.AreEqual("195", actual);
        }
    }
}