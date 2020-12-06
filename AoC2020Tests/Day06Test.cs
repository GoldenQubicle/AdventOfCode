using AoC2020.Solutions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2020Tests
{
    public class Day06Test
    {
        Day06 day6;

        [SetUp]
        public void Setup( )
        {
            day6 = new Day06("day06test1");
        }

        [Test]
        public void Part1( )
        {
            var expected = 11;
            var actual = day6.SolvePart1( );
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Part2( )
        {
            var expected = 6;
            var actual = day6.SolvePart2( );
            Assert.AreEqual(expected, actual);
        }
    }
}
