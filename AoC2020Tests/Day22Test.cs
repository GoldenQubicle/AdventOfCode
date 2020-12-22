using AoC2020.Solutions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2020Tests
{
    class Day22Test
    {
        Day22 day22;

        [SetUp]
        public void Setup( )
        {
            day22 = new Day22("day22test1");
        }

        [Test]
        public void Part1( )
        {
            var actual = day22.SolvePart1( );
            Assert.AreEqual(306, actual);
        }

        [Test]
        public void Part2( )
        {
            var actual = day22.SolvePart2( );
            Assert.AreEqual(291, actual);
        }       
    }
}
