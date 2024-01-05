using AoC2018;

namespace AoC2018Tests
{
    public class Day07Test
    {
        
        [Test]
        public void Part1( )
        {
	        var day07 = new Day07("day07test1");
			var actual = day07.SolvePart1( ).Result;
            Assert.That(actual, Is.EqualTo("CABDFE"));
        }

        [Test]
        public void Part2( )
        {
	        Day07.NumberOfWorkers = 2;
	        Day07.Offset = 64;
			var day07 = new Day07("day07test1");
            var actual = day07.SolvePart2( ).Result;
            Assert.That(actual, Is.EqualTo("15"));
        }
    }
}