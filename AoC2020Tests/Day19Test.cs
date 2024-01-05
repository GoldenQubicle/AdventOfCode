using AoC2020;
using NUnit.Framework;

namespace AoC2020Tests
{
    class Day19Test
    {
        Day19 day19;
        
        [SetUp]
        public void Setup( )
        {
            day19 = new Day19("day19test1");
        }

        [Test]
        public void Part1( )
        {
            var actual = day19.SolvePart1( ).Result;
            Assert.AreEqual(2.ToString( ), actual);
        }

        [Test]
        public void Part1Test2( )
        {
            day19 = new Day19("day19test2");
            var actual = day19.SolvePart1( ).Result;
            Assert.AreEqual(3.ToString( ), actual);            
        }

        [Test]
        public void Part2Test2( )
        {
            day19 = new Day19("day19test2");
            var actual = day19.SolvePart2( ).Result;
            Assert.AreEqual(12.ToString( ), actual);
        }
    }
}
