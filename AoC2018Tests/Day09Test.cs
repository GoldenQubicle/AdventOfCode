using AoC2018;

namespace AoC2018Tests
{
    public class Day09Test
    {
        Day09 day09;

        [SetUp]
        public void Setup( )
        {
            day09 = new Day09();
        }

		[TestCase(9, 25, 32)]
		[TestCase(10, 1618, 8317)] 
        [TestCase(13, 7999, 146373)] 
        [TestCase(17, 1104, 2764)] 
        [TestCase(21, 6111, 54718)] 
        [TestCase(30, 5807, 37305)]
        public void Part1(int players, int lastMarble, int expected )
        {
	        Day09.Players = players;
	        Day09.LastMarble = lastMarble;
            var actual = day09.SolvePart1( );
            Assert.That(actual, Is.EqualTo(expected.ToString()));
        }

        public void Part2( )
        {
            var expected = string.Empty;
            var actual = day09.SolvePart2( );
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}