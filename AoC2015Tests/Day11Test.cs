using AoC2015;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AoC2015Tests
{
    public class Day11Test
    {
        Day11 day11;

        [SetUp]
        public void Setup( )
        {
            day11 = new Day11(new List<string> { });
        }

        [TestCase("hijklmmn","false")] 
        [TestCase("abbceffg","false")] 
        [TestCase("abbcegjk","false")] 
        [TestCase("abcdefgh","false")] 
        [TestCase("abcdffaa","true")] 
        [TestCase("ghijklmn","false")] 
        [TestCase("ghjaabcc","true")]
        public void ValidatePassword(string input, string expected )
        {
            var actual = day11.ValidatePassword(input);
            Assert.AreEqual(expected, actual.ToString().ToLower());
        }

        [TestCase("abcdefgh", "abcdffaa")]
        [TestCase("ghijklmn", "ghjaabcc")]
        public void GenerateNewPassword(string input, string expected)
        {
            var actual = day11.GenerateNewPassword(input);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Part2( )
        {
            var actual = day11.SolvePart2( );
            Assert.AreEqual("", actual);
        }
    }
}