using AoC2021;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021Tests
{
    public class Day13Test
    {
        Day13 day13;

        [SetUp]
        public void Setup( )
        {
            day13 = new Day13("day13test1");
        }
        
        [Test]
        public void Part1( )
        {
            var actual = day13.SolvePart1( );
            Assert.AreEqual("17", actual);
        }

        [Test]
        public void Part2( )
        {
            var expected = "\r\n#####\r\n#...#\r\n#...#\r\n#...#\r\n#####";
            var actual = day13.SolvePart2( );
            Assert.AreEqual(expected, actual);
        }
    }
}