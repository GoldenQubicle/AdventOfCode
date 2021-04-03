using AoC2016;
using NUnit.Framework;
using System.Collections.Generic;

namespace AoC2016Tests
{
    public class Day16Test
    {
        Day16 day16;

        [SetUp]
        public void Setup( )
        {
            day16 = new Day16(new List<string>{ "10000" });
        }

        [Test]
        public void Part1( )
        {
            day16.Length = 20;
            var actual = day16.SolvePart1();
            Assert.AreEqual("01100", actual);
        }

        [TestCase("1","100")] 
        [TestCase("0","001")] 
        [TestCase("11111","11111000000")] 
        [TestCase("111100001010","1111000010100101011110000")]
        public void DragonizeTest(string input, string expected )
        {
            var actual = day16.Dragonize(input);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("110010110100", "100")]
        [TestCase("10000011110010000111", "01100")]
        public void CheckSumTest(string input, string expected)
        {
            var actual = day16.CheckSum(input);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Part2( )
        {
            var actual = day16.SolvePart2( );
            Assert.AreEqual("", actual);
        }
    }
}