namespace AoC2018Tests
{
    public class Day13Test
    {
        
        [Test]
        public void Part1( )
        {
	        var day13 = new Day13("day13test1");
			var actual = day13.SolvePart1( ).Result;
            Assert.That(actual, Is.EqualTo("7,3"));
        }

        [Test]
        public void Part2( )
        {
	        var day13 = new Day13("day13test2");
			var actual = day13.SolvePart2( ).Result;
            Assert.That(actual, Is.EqualTo("6,4"));
        }
    }
}