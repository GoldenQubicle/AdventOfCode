using AoC2015;
using NUnit.Framework;

namespace AoC2015Tests
{
    public class Day08Test
    {
        Day08 day08;

        [SetUp]
        public void Setup( )
        {
            day08 = new Day08("day08test1");
        }
        
        [Test]
        public void Part1( )
        {
            var actual = day08.SolvePart1( );
            Assert.AreEqual("12", actual);
        }

        [Test]
        public void Part2( )
        {
            var actual = day08.SolvePart2( );
            Assert.AreEqual("19", actual);
        }
    }
}