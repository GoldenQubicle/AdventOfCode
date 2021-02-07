using AoC2015;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AoC2015Tests
{
    public class Day18Test
    {
        Day18 day18;

        [SetUp]
        public void Setup( )
        {
            day18 = new Day18("day18test1");
            day18.Steps = 4;
        }
        
        [Test]
        public void Part1( )
        {
            var actual = day18.SolvePart1( );
            Assert.AreEqual("4", actual);
        }

        [Test]
        public void Part2( )
        {
            day18.Steps = 5;
            var actual = day18.SolvePart2( );
            Assert.AreEqual("17", actual);
        }
    }
}