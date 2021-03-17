using AoC2016;
using NUnit.Framework;
using System.Collections.Generic;

namespace AoC2016Tests
{
    public class Day09Test
    {
        Day09 day09;

        [SetUp]
        public void Setup( )
        {
            day09 = new Day09(nameof(Day09));
        }
        
        [TestCase("ADVENT","6")] 
        [TestCase("A(1x5)BC","7")] 
        [TestCase("(3x3)XYZ","9")] 
        [TestCase("A(2x2)BCD(2x2)EFG","11")] 
        [TestCase("(6x1)(1x3)A","6")] 
        [TestCase("X(8x2)(3x3)ABCY","18")]
        public void Part1(string input, string expected )
        {
            day09 = new Day09(new List<string> { input } );
            var actual = day09.SolvePart1( );
            Assert.AreEqual(expected, actual);
        }

        [TestCase("(3x3)XYZ", "9")]
        [TestCase("X(8x2)(3x3)ABCY", "20")]
        [TestCase("(27x12)(20x12)(13x14)(7x10)(1x12)A", "241920")]
        [TestCase("(25x3)(3x3)ABC(2x3)XY(5x2)PQRSTX(18x9)(3x2)TWO(5x7)SEVEN", "445")]
        public void Part2(string input, string expected)
        {
            day09 = new Day09(new List<string> { input });

            var actual = day09.SolvePart2( );
            Assert.AreEqual(expected, actual);
        }
    }
}