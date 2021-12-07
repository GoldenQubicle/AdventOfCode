using AoC2021;
using NUnit.Framework;

namespace AoC2021Tests
{
    public class Day06Test
    {
        Day06 day06;

        [SetUp]
        public void Setup( )
        {
            day06 = new Day06("day06test1");
        }
        
        [Test]
        public void Part1()
        {
            var actual = day06.SolvePart1( );
            Assert.AreEqual("5934", actual);
        }

        [Test]
        public void Part2( )
        {
            var actual = day06.SolvePart2( );
            Assert.AreEqual("26984457539", actual);
        }
    }
}