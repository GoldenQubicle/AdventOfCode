namespace AoC2018Tests
{
    public class Day02Test
    {
        [Test]
        public void Part1( )
        {
	        var day02 = new Day02("day02test1");
			var actual = day02.SolvePart1( ).Result;
            Assert.That(actual, Is.EqualTo("12"));
        }

        [Test]
        public void Part2( )
        {
	        var day02 = new Day02("day02test2");
            var actual = day02.SolvePart2( ).Result;
            Assert.That(actual, Is.EqualTo("fgij"));
        }
    }
}