using AoC2016;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AoC2016Tests
{
    public class Day25Test
    {
        Day25 day25;

        [SetUp]
        public void Setup( )
        {
            day25 = new Day25("day25");
        }
        
        [Test]
        public void Part1( )
        {
            var actual = day25.SolvePart1( );
            Assert.AreEqual("158", actual);
        }

        [Test]
        public void Part2( )
        {
            var actual = day25.SolvePart2( );
            Assert.AreEqual("", actual);
        }
    }
}