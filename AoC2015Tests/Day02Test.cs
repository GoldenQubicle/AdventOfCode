using AoC2015;
using NUnit.Framework;

namespace AoC2015Tests
{
    public class Day02Test
    {
        Day02 day02;

        [SetUp]
        public void Setup( )
        {
            day02 = new Day02("day02test1");
        }

        [Test]
        public void Part1( )
        {
            var actual = day02.SolvePart1( );
            Assert.AreEqual("58", actual);
        }

        [Test]
        public void Part2( )
        {
            var actual = day02.SolvePart2( );
            Assert.AreEqual("48", actual);
        }
    }
}