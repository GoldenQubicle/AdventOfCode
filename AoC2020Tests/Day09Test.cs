using AoC2020.Solutions;
using NUnit.Framework;

namespace AoC2020Tests
{
    class Day09Test
    {
        Day09 day9;
        
        [SetUp]
        public void Setup( )
        {
            day9 = new Day09("day09test1");
        }
        
        [Test]
        public void Part1( )
        {
            var expected = 127L;
            var actual = day9.SolvePart1( );
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Part2( )
        {
            var expected = 62L;
            var actual = day9.SolvePart2( );
            Assert.AreEqual(expected, actual);
        }

    }
}
