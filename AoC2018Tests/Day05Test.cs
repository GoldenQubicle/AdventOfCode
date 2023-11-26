using AoC2018;

namespace AoC2018Tests
{
    public class Day05Test
    {
        Day05 day05;

        [TestCase("aA","0")] 
        [TestCase("abBA","0")] 
        [TestCase("abAB","4")] 
        [TestCase("aabAAB","6")] 
        [TestCase("dabAcCaCBAcCcaDA","10")]
        public void Part1(string input, string expected )
        {
            day05 = new Day05(new List<string> { input } );
            var actual = day05.SolvePart1( );
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase("dabAcCaCBAcCcaDA")]
        public void Part2(string input)
        {
	        day05 = new Day05(new List<string> { input });
			var actual = day05.SolvePart2( );
            Assert.That(actual, Is.EqualTo("4"));
        }
    }
}