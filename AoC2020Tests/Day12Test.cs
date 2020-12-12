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

        [TestCase('R', 90, Direction.South)]
        [TestCase('L', 90, Direction.North)]
        [TestCase('L', 270, Direction.South)]
        [TestCase('R', 270, Direction.North)]
        [TestCase('L', 180, Direction.West)]
        public void ChangeDirection(char action, int value, Direction expected)
        {
            var ins = new Instruction { Action = action, Value = value };
            var actual = day12.ChangeDirection(ins);
            Assert.AreEqual(expected, actual);
        }

        [TestCase('N', 5, 0, 5)]
        [TestCase('E', 10, 10, 0)]
        [TestCase('S', 15, 0, -15)]
        [TestCase('W', 20, -20, 0)]
        public void MoveAbsolute(char action, int value, int xe, int ye)
        {
            var ins = new Instruction { Action = action, Value = value };
            var actual = day12.MoveAbsolute(ins);
            Assert.AreEqual(xe, actual.x);
            Assert.AreEqual(ye, actual.y);
        }
    }
}
