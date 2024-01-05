using AoC2016;
using NUnit.Framework;
using System.Collections.Generic;

namespace AoC2016Tests
{
    public class Day19Test
    {
        Day19 day19;

        [SetUp]
        public void Setup( )
        {
            day19 = new Day19(new List<string> { "5" });
        }

        [Test]
        public void Part1( )
        {
            var actual = day19.SolvePart1().Result;
            Assert.AreEqual("3", actual);
        }

        [Test]
        public void Part2( )
        {
            var actual = day19.SolvePart2().Result;
            Assert.AreEqual("2", actual);
        }
    }
}