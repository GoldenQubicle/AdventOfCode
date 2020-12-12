using AoC2020.Solutions;
using NUnit.Framework;
using System;
using System.Drawing;
using System.Numerics;
using static AoC2020.Solutions.Day12;
using Instruction = AoC2020.Solutions.Day12.Instruction;

namespace AoC2020Tests
{
    class Day12Test
    {
        Day12 day12;

        [SetUp]
        public void Setup( )
        {
            day12 = new Day12("day12test1");
        }

        [Test]
        public void Part1( )
        {
            var actual = day12.SolvePart1( );
            Assert.AreEqual(25, actual);
        }

        [Test]
        public void Part2( )
        {
            var actual = day12.SolvePart2( );
            Assert.AreEqual(286, actual);
        }       

    }
}
