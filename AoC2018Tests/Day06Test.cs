using AoC2018;

namespace AoC2018Tests
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
        public void Part1( )
        {
            var actual = day06.SolvePart1( ).Result;
            Assert.That(actual, Is.EqualTo("17"));
        }

        [Test]
        public void Part2( )
        {
	        day06.Threshold = 32;
            var actual = day06.SolvePart2( ).Result;
            Assert.That(actual, Is.EqualTo("16"));
        }
    }
}