using AoC2015;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AoC2015Tests
{
    public class Day13Test
    {
        Day13 day13;

        [SetUp]
        public void Setup( )
        {
            day13 = new Day13("day13");
        }
        
        [Test]
        public void Part1( )
        {
            var actual = day13.SolvePart1( );
            Assert.AreEqual("330", actual);
        }

        [Test]
        public void Part2( )
        {
            var actual = day13.SolvePart2( );
            Assert.AreEqual("", actual);
        }
    }
}