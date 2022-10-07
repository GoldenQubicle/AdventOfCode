using AoC2016;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AoC2016Tests
{
    public class Day11Test
    {
        Day11 day11;

        [SetUp]
        public void Setup( )
        {
            day11 = new Day11("day11");
        }
        
        [Test]
        public void Part1( )
        {
            var actual = day11.SolvePart1( );
            Assert.AreEqual("31", actual);
        }

        [Test]
        public void Part2( )
        {
            var actual = day11.SolvePart2( );
            Assert.AreEqual("55", actual);
        }
    }
}