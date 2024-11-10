namespace AoC2018Tests
{
    public class Day10Test
    {
        Day10 day10;

        [SetUp]
        public void Setup( )
        {
            day10 = new Day10("day10test1");
        }

        [Ignore("No tests this day")]
        [Test]
        public void Part1( )
        {
            var actual = day10.SolvePart1( ).Result;
            Assert.That(actual, Is.EqualTo("HI"));
        }

        [Ignore("No tests this day")]
		[Test]
        public void Part2( )
        {
            var expected = string.Empty;
            var actual = day10.SolvePart2( ).Result;
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}