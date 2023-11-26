using AoC2018;
using NUnit.Framework;
using System.Collections.Generic;
namespace AoC2018Tests
{
    public class Day01Test
    {
        [TestCase("+1,+1,+1","3")] 
        [TestCase("+1,+1,-2","0")] 
        [TestCase("-1,-2,-3","-6")]
        [TestCase("+1, -2, +3, +1", "3")]
        public void Part1(string input, string expected )
        {
            var day01 = new Day01(input.Split(',').ToList());
            var actual = day01.SolvePart1( );
            Assert.That(actual, Is.EqualTo(expected));
        }

		[TestCase("+1, -2, +3, +1", "2")]
		[TestCase("+1, -1", "0")]
		[TestCase("+3, +3, +4, -2, -4", "10")]
		[TestCase("-6, +3, +8, +5, -6", "5")]
		[TestCase("+7, +7, -2, -7, -4", "14")]
		public void Part2(string input, string expected)
        {
			var day01 = new Day01(input.Split(',').ToList( ));
			var actual = day01.SolvePart2( );
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}