using AoC2016;
using NUnit.Framework;

namespace AoC2016Tests
{
    public class Day03Test
    {
        Day03 day03;

        [SetUp]
        public void Setup( )
        {
            day03 = new Day03(nameof(Day03));
        }
        
        [Test]
        public void Part1( )
        {
            var actual = day03.SolvePart1( );
            Assert.AreEqual("993", actual);
        }

        [Test]
        public void Part2( )
        {
            var actual = day03.SolvePart2( );
            Assert.AreEqual("1849", actual);
        }
    }
}