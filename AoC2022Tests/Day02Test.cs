using AoC2022;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AoC2022Tests
{
    public class Day02Test
    {
        Day02 day02;

        [SetUp]
        public void Setup( )
        {
            day02 = new Day02("day02test1");
        }
        
        [Test]
        public void Part1( )
        {
            var actual = day02.SolvePart1( );
            Assert.AreEqual("15", actual);
        }

        [Test]
        public void Part2( )
        {
            var actual = day02.SolvePart2( );
            Assert.AreEqual("12", actual);
        }
    }
}