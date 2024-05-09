using AoC2022;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AoC2022Tests
{
	public class Day17Test
    {
        Day17 day17;

        
		[SetUp]
        public void Setup( )
        {
            day17 = new Day17("day17test1");
        }
        
        [Test]
        public void Part1( )
        {
            var actual = day17.SolvePart1( ).Result;
            Assert.AreEqual("3068", actual);
        }

        [Test]
        public void Part2( )
        {
            var actual = day17.SolvePart2( ).Result;
            Assert.AreEqual("1514285714288", actual);
        }
    }
}