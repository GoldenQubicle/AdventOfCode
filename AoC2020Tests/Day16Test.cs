using AoC2020;
using NUnit.Framework;

namespace AoC2020Tests
{
    class Day16Test
    {
        Day16 day16;

        [SetUp]
        public void Setup( )
        {
            day16 = new Day16("day16test1");
        }

        [Test]
        public void Part1( )
        {
            var actual = day16.SolvePart1( );
            Assert.AreEqual(71, actual);
        }

        [Test]
        public void Part2( )
        {
            day16 = new Day16("day16test1");
            var actual = day16.SolvePart2( );
            //Assert.AreEqual(71, actual);
        }
    }
}
