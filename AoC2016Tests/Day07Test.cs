using AoC2016;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AoC2016Tests
{
    public class Day07Test
    {
        Day07 day07;

        [SetUp]
        public void Setup( )
        {
            day07 = new Day07(new List<string> { });
        }

        [TestCase("abba[mnop]qrst", true)]
        [TestCase("abcd[bddb]xyyx", false)]
        [TestCase("aaaa[qwer]tyui", false)]
        [TestCase("ioxxoj[asdfgh]zxcvbn", true)]
        public void Part1(string input, bool expected)
        {
            var actual = day07.HasTlsSupport(input);
            Assert.AreEqual(expected, actual);
        }


        [TestCase("aba[bab]xyz", true)]
        [TestCase("xyx[xyx]xyx", false)]
        [TestCase("aaa[kek]eke", true)]
        [TestCase("zazbz[bzb]cdb", true)]
        public void Part2(string input, bool expected)
        {
            var actual = day07.HasSslSupport(input);
            Assert.AreEqual(expected, actual);
        }
    }
}