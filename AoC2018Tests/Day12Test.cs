using AoC2018;

namespace AoC2018Tests
{
    public class Day12Test
    {
        Day12 day12;

        [SetUp]
        public void Setup( )
        {
            day12 = new Day12("day12test1");
        }
        
        [Test]
        public void Part1( )
        {
            var actual = day12.SolvePart1( ).Result;
            Assert.That(actual, Is.EqualTo("325"));
        }

        [Ignore("No test case for part 2")]
        [Test]
        public void Part2( )
        {
            var expected = string.Empty;
            var actual = day12.SolvePart2( ).Result;
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}