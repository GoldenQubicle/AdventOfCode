using AoC2021;
using NUnit.Framework;

namespace AoC2021Tests
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
            var actual = day02.SolvePart1( ).Result;
            Assert.AreEqual("150", actual);
        }

        [Test]
        public void Part2( )
        {
            var actual = day02.SolvePart2( ).Result;
            Assert.AreEqual("900", actual);
        }
    }
}