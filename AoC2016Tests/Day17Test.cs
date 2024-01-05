using AoC2016;
using NUnit.Framework;
using System.Collections.Generic;

namespace AoC2016Tests
{
    public class Day17Test
    {
        Day17 day17;

        [TestCase("ihgpwlah", "DDRRRD")]
        [TestCase("kglvqrro", "DDUDRLRRUDRD")]
        [TestCase("ulqzkmiv", "DRURDRUDDLLDLUURRDULRLDUUDDDRR")]
        public void Part1(string input, string expected)
        {
            day17 = new Day17(new List<string> { input });
            var actual = day17.SolvePart1().Result;
            Assert.AreEqual(expected, actual);
        }

        [TestCase("ihgpwlah", "370")]
        [TestCase("kglvqrro", "492")]
        [TestCase("ulqzkmiv", "830")]
        public void Part2(string input, string expected)
        {
            day17 = new Day17(new List<string> { input });
            var actual = day17.SolvePart2().Result;
            Assert.AreEqual(expected, actual);
        }
    }
}