using AoC2015;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AoC2015Tests
{
    public class Day23Test
    {
        Day23 day23;

        [SetUp]
        public void Setup( )
        {
            day23 = new Day23("day23test1");
        }
        
        [Test]
        public void Part1( )
        {
            day23.Answer = "a";
            var actual = day23.SolvePart1( );
            Assert.AreEqual("2", actual);
        }

        [Test]
        public void Part2( )
        {
            day23 = new Day23(nameof(Day23));
            var actual = day23.SolvePart2( );
            Assert.AreEqual("", actual);
        }
    }
}