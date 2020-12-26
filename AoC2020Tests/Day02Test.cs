using AoC2020;
using NUnit.Framework;

namespace AoC2020Tests
{
    class Day02Test
    {
        private Day02 day02;

        [SetUp]
        public void Setup( )
        {
            day02 = new Day02("day02test1");
        }

        [Test]
        public void Part1( )
        {
            var expected = 2.ToString( );
            var actual = day02.SolvePart1( );
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Part2( )
        {
            var expected = 1.ToString( );
            var actual = day02.SolvePart2( );
            Assert.AreEqual(expected, actual);
        }
    }
}
