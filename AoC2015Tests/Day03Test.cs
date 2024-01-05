using AoC2015;
using NUnit.Framework;
using System.Collections.Generic;

namespace AoC2015Tests
{
    public class Day03Test
    {                
        [TestCase(">", "2")]
        [TestCase("^>v<", "4")]
        [TestCase("^v^v^v^v^v", "2")]
        public void Part1(string input, string expected)
        {
            var day03 = new Day03(new List<string> { input });
            var actual = day03.SolvePart1( ).Result;
            Assert.AreEqual(expected, actual);
        }

        [TestCase("^v", "3")]
        [TestCase("^>v<", "3")]
        [TestCase("^v^v^v^v^v", "11")]
        public void Part2(string input, string expected )
        {
            var day03 = new Day03(new List<string> { input });
            var actual = day03.SolvePart2( ).Result;
            Assert.AreEqual(expected, actual);
        }
    }
}