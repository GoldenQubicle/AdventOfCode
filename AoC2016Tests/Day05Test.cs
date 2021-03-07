using AoC2016;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AoC2016Tests
{
    public class Day05Test
    {
        Day05 day05;

        [SetUp]
        public void Setup( )
        {
        }
        
        [TestCase("abc","18F47A30")]
        public void Part1(string input, string expected )
        {
            day05 = new Day05(new List<string> { input } );
            var actual = day05.SolvePart1( );
            Assert.AreEqual(expected, actual);
        }

        [TestCase("abc", "05ACE8E3")]
        public void Part2(string input, string expected)
        {
            day05 = new Day05(new List<string> { input });
            var actual = day05.SolvePart2( );
            Assert.AreEqual(expected, actual);
        }
    }
}