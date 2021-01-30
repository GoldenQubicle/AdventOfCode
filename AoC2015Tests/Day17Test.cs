using AoC2015;
using NUnit.Framework;

namespace AoC2015Tests
{
    public class Day17Test
    {
        Day17 day17;

        [SetUp]
        public void Setup( )
        {
            day17 = new Day17("day17test1");
            day17.Liters = 25;
        }

        [Test]
        public void Part1( )
        {
            var actual = day17.SolvePart1( );
            Assert.AreEqual("4", actual);
        }

        [Test]
        public void Part2( )
        {
            var actual = day17.SolvePart2( );
            Assert.AreEqual("3", actual);
        }
    }
}