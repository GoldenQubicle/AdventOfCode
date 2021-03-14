using AoC2015;
using NUnit.Framework;
using System.Collections.Generic;

namespace AoC2015Tests
{
    public class Day20Test
    {
        Day20 day20;

        [SetUp]
        public void Setup( )
        {
            //day20 = new Day20("day20test1");
        }

        [TestCase("29000000", "665280")]
        public void Part1(string input, string expected )
        {
            day20 = new Day20(new List<string> { input } );
            var actual = day20.SolvePart1( );
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Part2( )
        {
            day20 = new Day20(new List<string> { "29000000" });

            var actual = day20.SolvePart2( );
            Assert.AreEqual("705600", actual);
        }
    }
}