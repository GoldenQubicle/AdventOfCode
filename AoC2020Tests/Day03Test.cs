using AoC2020;
using NUnit.Framework;

namespace AoC2020Tests
{
    class Day03Test
    {
        private Day03 day03;

        [SetUp]
        public void Setup( )
        {
            day03 = new Day03("day03test1");
        }

        [Test]
        public void Part1( )
        {
            var expected = 7.ToString( );
            var actual = day03.SolvePart1( ).Result;

            Assert.AreEqual(expected, actual);
        }      

        [TestCase(0, 0, '.')]
        [TestCase(11, 1, '#')]
        [TestCase(12, 1, '.')]
        [TestCase(15, 9 , '#')]
        [TestCase(22, 8 , '#')]
        public void GetTerrainTileShouldNotGoOutOfBounds(int x, int y, char expected)
        {
            var actual = day03.GetTerrainTile((x, y));
            Assert.AreEqual(expected, actual);
        }

        [TestCase(1,1,2)]
        [TestCase(3,1,7)]
        [TestCase(5,1,3)]
        [TestCase(7,1,4)]
        [TestCase(1,2,2)]
        public void TraverseSlopeShouldFindCorrectTrees(int xIncr, int yIncr, int expected  )
        {
            var actual = day03.TraverseSlope((xIncr, yIncr));
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Part2( )
        {
            var expected = 336.ToString( );
            var actual = day03.SolvePart2( ).Result;

            Assert.AreEqual(expected, actual);
        }
    }
}
