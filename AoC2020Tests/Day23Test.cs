using AoC2020.Solutions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2020Tests
{
    class Day23Test
    {
        Day23 day23;

        [SetUp]
        public void Setup( )
        {
            day23 = new Day23("day23test1");
        }

        [TestCase(10,  "92658374")]
        [TestCase(100, "67384529")]
        public void Part1(int iterations, string expected )
        {
            var actual = day23.SolvePart1(iterations);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Part2( )
        {
            var actual = day23.SolvePart2(10000000);
            Assert.AreEqual("149245887792", actual);
        }
    }
}
