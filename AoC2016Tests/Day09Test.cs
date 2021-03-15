using AoC2016;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AoC2016Tests
{
    public class Day09Test
    {
        Day09 day09;

        [SetUp]
        public void Setup( )
        {
            //day09 = new Day09(new List<string>());
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

        [Test]
        public void Part2( )
        {
            var actual = day09.SolvePart2( );
            Assert.AreEqual("", actual);
        }
    }
}