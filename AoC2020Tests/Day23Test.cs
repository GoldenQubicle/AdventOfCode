using AoC2020;
using NUnit.Framework;

namespace AoC2020Tests
{
    class Day23Test
    {
        Day23 day23;

        [SetUp]
        public void Setup( )
        {
            day23 = new Day23("day23test1");
        }

        [TestCase(10, "92658374")]
        [TestCase(100, "67384529")]
        public void Part1(int iterations, string expected)
        {
            day23.IteratePart1 = iterations;
            var actual = day23.SolvePart1( );
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Part2( )
        {
            day23.IteratePart2 = 10000000;
            var actual = day23.SolvePart2( );
            Assert.AreEqual("149245887792", actual);
        }
    }
}
