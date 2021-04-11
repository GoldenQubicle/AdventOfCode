using AoC2016;
using NUnit.Framework;

namespace AoC2016Tests
{
    public class Day20Test
    {
        Day20 day20;

        [SetUp]
        public void Setup( )
        {
            day20 = new Day20("day20test1");
        }
        
        [Test]
        public void Part1( )
        {
            var actual = day20.SolvePart1( );
            Assert.AreEqual("3", actual);
        }

        [Test]
        public void Part2( )
        {
            day20.MaxIp = 9;
            var actual = day20.SolvePart2( );
            Assert.AreEqual("2", actual);
        }
    }
}