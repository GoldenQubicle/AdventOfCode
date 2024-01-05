using AoC2020;
using NUnit.Framework;

namespace AoC2020Tests
{
    class Day13Test
    {
        Day13 day13;


        [SetUp]
        public void Setup( )
        {
            day13 = new Day13("day13test1");
        }

        [Test]
        public void Part1( )
        {
            var actual = day13.SolvePart1( ).Result;
            Assert.AreEqual(295.ToString( ), actual);
        }

        [Test]
        public void Part2( )
        {
            var actual = day13.SolvePart2( ).Result;
            Assert.AreEqual(1068781.ToString( ), actual);
        }     
    }
}
