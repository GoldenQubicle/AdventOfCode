using AoC2015;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AoC2015Tests
{
    public class Day01Test
    {
        Day01 day01;

        [SetUp]
        public void Setup( )
        {
            day01 = new Day01("day01test1");
        }

        [Test]
        public void Part1( )
        {
            var actual = day01.SolvePart1( );
            Assert.AreEqual("15", actual);
        }

        [Test]
        public void Part2( )
        {
            var actual = day01.SolvePart2( );
            Assert.AreEqual("", actual);
        }
    }
}