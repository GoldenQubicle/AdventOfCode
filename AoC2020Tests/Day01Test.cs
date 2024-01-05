using AoC2020;
using NUnit.Framework;

namespace AoC2020Tests
{
    class Day01Test
    {
        private Day01 day01;

        [SetUp]
        public void Setup( )
        {
            day01 = new Day01("day01test1");
        }

        [Test]
        public void Part1( )
        {
            var expected = 514579.ToString( );
            var result = day01.SolvePart1( ).Result;
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Part2( )
        {
            var expected = 241861950.ToString( );
            var result = day01.SolvePart2( ).Result;
            Assert.AreEqual(expected, result);
        }
    }
}