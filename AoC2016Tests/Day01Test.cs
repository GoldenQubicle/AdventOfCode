using static AoC2016.Day01;
using AoC2016;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AoC2016Tests
{
    public class Day01Test
    {
        Day01 day01;

        [SetUp]
        public void Setup( )
        {
            day01 = new Day01(new List<string>());
        }

        [TestCase("R2, L3", "5")]
        [TestCase("R2, R2, R2", "2")]
        [TestCase("R5, L5, R5, R3", "12")]
        public void Part1(string input, string expected)
        {
            day01 = new Day01(new List<string> { input });
            var actual = day01.SolvePart1().Result;
            Assert.AreEqual(expected, actual);
        }

        [TestCase(Direction.North, Turn.Right, Direction.East)]
        [TestCase(Direction.South, Turn.Left, Direction.East)]
        [TestCase(Direction.South, Turn.Right, Direction.West)]
        public void GetDirectionTest(Direction current, Turn turn, Direction expected)
        {
            var actual = day01.GetDirection(current, turn);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("R8, R4, R4, R8", "4")]
        public void Part2(string input, string expected)
        {
            day01 = new Day01(new List<string> { input });
            var actual = day01.SolvePart2().Result;
            Assert.AreEqual(expected, actual);
        }
    }
}