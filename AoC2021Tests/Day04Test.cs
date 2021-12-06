using AoC2021;
using NUnit.Framework;

namespace AoC2021Tests
{
    public class Day04Test
    {
        Day04 day04;

        [SetUp]
        public void Setup( )
        {
            day04 = new Day04("day04test1");
        }
        
        [Test]
        public void Part1( )
        {
            var actual = day04.SolvePart1( );
            Assert.AreEqual("4512", actual);
        }

        [Test]
        public void Part2( )
        {
            var actual = day04.SolvePart2( );
            Assert.AreEqual("1924", actual);
        }
    }
}