using AoC2015;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AoC2015Tests
{
    public class Day19Test
    {
        Day19 day19;

        [SetUp]
        public void Setup( )
        {
            day19 = new Day19("day19");
        }

        [Test]
        public void bla( )
        {
            var p2 = day19.SolvePart2();
        }
        
        [TestCase("HOH","4")] 
        [TestCase("HOHOHO","7")]
        public void Part1(string input, string expected )
        {
            day19 = new Day19(new List<string> { input } );
            day19.mappings.Add("H", new List<string> { "HO", "OH" });
            day19.mappings.Add("O", new List<string> { "HH" });
            var actual = day19.SolvePart1( );
            Assert.AreEqual(expected, actual);
        }

        [TestCase("HOH", "3")]
        [TestCase("HOHOHO", "6")]
        public void Part2(string input, string expected )
        {
            day19 = new Day19(new List<string> { input });
            day19.mappings.Add("H", new List<string> { "HO", "OH" });
            day19.mappings.Add("O", new List<string> { "HH" });
            day19.mappings.Add("e", new List<string> { "H", "O" });

            var actual = day19.SolvePart2( );
            Assert.AreEqual(expected, actual);
        }
    }
}