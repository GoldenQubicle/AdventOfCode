using AoC2015;
using NUnit.Framework;
using System.Collections.Generic;

namespace AoC2015Tests
{
    public class Day04Test
    {
        [TestCase("abcdef", "609043")]
        [TestCase("pqrstuv", "1048970")]
        public void Part1(string input, string expected)
        {
            var day04 = new Day04(new List<string> { input });
            var actual = day04.SolvePart1( );
            Assert.AreEqual(expected, actual);
        }
    }
}