using AoC2015;
using NUnit.Framework;

namespace AoC2015Tests
{
    public class Day01Test
    {
        Day01 day01;

        [SetUp]
        public void Setup( )
        {
            day01 = new Day01("day01test1");
        }

        [TestCase("day01test1", "0")]
        [TestCase("day01test2", "-3")]
        public void Part1(string input, string expected)
        {
            day01 = new Day01(input);
            var actual = day01.SolvePart1( );
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Part2( )
        {
            day01 = new Day01("day01test3");
            var actual = day01.SolvePart2( );
            Assert.AreEqual("5", actual);
        }
    }
}