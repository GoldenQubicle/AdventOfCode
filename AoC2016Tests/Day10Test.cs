using AoC2016;
using NUnit.Framework;

namespace AoC2016Tests
{
    public class Day10Test
    {
        Day10 day10;

        [SetUp]
        public void Setup( )
        {
            day10 = new Day10("day10test1");
        }
        
        [Test]
        public void Part1( )
        {
            day10.LookFor = (5, 2);
            var actual = day10.SolvePart1( ).Result;
            Assert.AreEqual("2", actual);
        }

        [Test]
        public void Part2( )
        {
            var actual = day10.SolvePart2( ).Result;
            Assert.AreEqual("30", actual);
        }
    }
}