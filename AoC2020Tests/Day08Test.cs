using AoC2020;
using NUnit.Framework;

namespace AoC2020Tests
{
    public class Day08Test
    {
        Day08 day8;

        [SetUp]
        public void Setup( )
        {
            day8 = new Day08("day08test1");
        }

        [Test]
        public void Part1( )
        {
            var expected = 5;
            var actual = day8.SolvePart1( );
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Part2( )
        {
            day8 = new Day08("day08test1");
            var expected = 8;
            var actual = day8.SolvePart2( );
            Assert.AreEqual(expected, actual);
        }


    }
}
