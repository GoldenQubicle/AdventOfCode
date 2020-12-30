using AoC2015;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AoC2015Tests
{
    public class Day05Test
    {
        Day05 day05;

        [SetUp]
        public void Setup( )
        {
            day05 = new Day05(new List<string> { });
        }

      
        [TestCase("ugknbfddgicrmopn", true)]
        [TestCase("aaa", true)]
        [TestCase("jchzalrnumimnmhp", false)]
        [TestCase("haegwjzuvuyypxyu", false)]
        [TestCase("dvszwmarrgswjxmb", false)]
        public void IsValidStringPart1Test(string input, bool expected)
        {
            var actual = day05.IsValidStringPart1(input);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("qjhvhtzxzqqjkmpb", true)]
        [TestCase("xxyxx", true)]
        [TestCase("uurcxstgmygtbstg", false)]
        [TestCase("ieodomkazucvgmuy", false)]
        public void IsValidStrinPart2(string input, bool expected)
        {
            var actual = day05.IsValidStringPart2(input );
            Assert.AreEqual(expected, actual);
        }
    }
}