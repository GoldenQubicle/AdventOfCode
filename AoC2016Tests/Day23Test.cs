using AoC2016;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AoC2016Tests
{
    public class Day23Test
    {
        Day23 day23;

        [SetUp]
        public void Setup( )
        {
            
        }
        
        [Test]
        public void Part1( )
        {
            day23 = new Day23("day23");
            var actual = day23.SolvePart1( );
            Assert.AreEqual("13685", actual);
        }

        [Test]
        public void Part2( )
        {
            var actual = day23.SolvePart2( );
            Assert.AreEqual("479010245", actual);
        }
    }
}