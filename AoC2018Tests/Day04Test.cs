using AoC2018;

namespace AoC2018Tests
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
            var actual = day04.SolvePart1( ).Result;
            Assert.That(actual, Is.EqualTo("240"));
        }

        [Test]
        public void Part2( )
        {
            var actual = day04.SolvePart2( ).Result;
            Assert.That(actual, Is.EqualTo("4455"));
        }
    }
}