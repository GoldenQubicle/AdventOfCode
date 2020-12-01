using AoC2020;
using NUnit.Framework;

namespace AoC2020Tests
{
    public class Day01Test
    {
        private Day01 day01;

        [SetUp]
        public void Setup( )
        {

        }

        [Test]
        public void Part1( )
        {
            day01 = new Day01("day01test1");

            var expected = 514579;

            var result = day01.SolvePart1( );

            Assert.AreEqual(expected, result);

        }

        [Test]
        public void Part2( )
        {
            {
                day01 = new Day01("day01test1");

                var expected = 241861950;

                var result = day01.SolvePart2( );

                Assert.AreEqual(expected, result);
            }
        }
    }
}