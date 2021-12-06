using AoC2021;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021Tests
{
    public class Day05Test
    {
        Day05 day05;

        [SetUp]
        public void Setup( )
        {
            day05 = new Day05("day05test1");
        }
        
        [Test]
        public void Part1( )
        {
            var actual = day05.SolvePart1( );
            Assert.AreEqual("5", actual);
        }

        [Test]
        public void Part2( )
        {
            var actual = day05.SolvePart2( );
            Assert.AreEqual("12", actual);
        }
    }
}