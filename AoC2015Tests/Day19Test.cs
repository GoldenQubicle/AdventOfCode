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
        
        [TestCase("HOH","4")] 
        [TestCase("HOHOHO","7")]
        public void Part1(string input, string expected )
        {
            day19 = new Day19(new List<string> { input } );
            day19.Mappings.Add("H", new List<string> { "HO", "OH" });
            day19.Mappings.Add("O", new List<string> { "HH" });
            var actual = day19.SolvePart1( ).Result;
            Assert.AreEqual(expected, actual);
        }
    }
}