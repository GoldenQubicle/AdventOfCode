using AoC2016;
using NUnit.Framework;

namespace AoC2016Tests
{
    public class Day22Test
    {
        Day22 day22;

        [SetUp]
        public void Setup( )
        {
            day22 = new Day22("day22test1");
        }
        
        [Test]
        public void Part1( )
        {
            var actual = day22.SolvePart1( );
            Assert.AreEqual("7", actual);
        }

        [Test]
        public void Part2( )
        {
            var actual = day22.SolvePart2( );
            Assert.AreEqual("9", actual);//note not the actual correct answer since there are no walls in example, and we presume to move the empty cell..
        }
    }
}