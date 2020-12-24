using AoC2020.Solutions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2020Tests
{
    class Day24Test
    {
        Day24 day24;
        
        [SetUp]
        public void Setup( )
        {
            day24 = new Day24("day24test1");
        }

        [Test]
        public void Part1()
        {
            var actual = day24.SolvePart1( );
            Assert.AreEqual(10, actual);
        }

        [Test]
        public void Part2( )
        {
            var actual = day24.SolvePart2( );
            Assert.AreEqual(2208, actual);
        }

    }
}
