using AoC2016;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AoC2016Tests
{
    public class Day21Test
    {
        Day21 day21;

        [SetUp]
        public void Setup( )
        {
            day21 = new Day21("day21test1");
        }

        [Test]
        public void Part1( )
        {
            day21.PassWord = "abcde";
            var actual = day21.SolvePart1().Result;
            Assert.AreEqual("decab", actual);
        }

        [Test]
        public void Part2( )
        {
            day21.Scrambled = "decab";
            var actual = day21.SolvePart2().Result;
            Assert.AreEqual("abcde", actual);
        }

        [TestCase("abcde", "ebcda", 0, 4)]
        [TestCase("012345", "013245", 2, 3)]
        public void SwapPositionTest(string input, string expected, int d1, int d2)
        {
            var actual = day21.SwapPosition(input, d1, d2);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("ebcda", "edcba",   "d", "b")]
        [TestCase("012345", "013245", "2", "3")]
        public void SwapLetterTest(string input, string expected, string d1, string d2)
        {
            var actual = day21.SwapLetter(input, d1, d2);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("abcd", "dabc", 1, true)]
        [TestCase("abcd", "bcda", 1, false)]
        [TestCase("abcde", "cdeab", 3, true)]
        [TestCase("abcde", "deabc", 3, false)]
        [TestCase("abcde", "bcdea", 4, true)]
        [TestCase("abcde", "eabcd", 4, false)]
        [TestCase("decab", "ecabd", 1, false)]
        public void RotateTest(string input, string expected, int steps, bool isRight)
        {
            var actual = day21.Rotate(input, steps, isRight);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("abdec", "ecabd", "b")]
        [TestCase("ecabd", "decab", "d")]
        public void RotatedLetterTest(string input, string expected, string letter)
        {
            var actual = day21.Rotate(input, letter);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("edcba", "abcde", 0, 4)]
        [TestCase("abcdef", "aedcbf", 1, 4)]
        public void ReverseTest(string input, string expected, int d1, int d2)
        {
            var actual = day21.Reverse(input, d1, d2);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("bcdea", "bdeac", 1, 4)]
        [TestCase("bdeac", "abdec", 3, 0)]
        public void MoveTest(string input, string expected, int d1, int d2)
        {
            var actual = day21.Move(input, d1, d2);
            Assert.AreEqual(expected, actual);
        }
    }
}