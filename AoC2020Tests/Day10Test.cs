using AoC2020.Solutions;
using NUnit.Framework;

namespace AoC2020Tests
{
    class Day10Test
    {
        Day10 day10;

        [SetUp]
        public void Setup( )
        {
            day10 = new Day10("day10test1");
        }

        [TestCase("day10test1", 7, 5)]
        [TestCase("day10test2", 22, 10)]
        public void CalculateJoltDeltas(string file, int jd1, int jd3)
        {
            day10 = new Day10(file);
            var actual = day10.CalculateJoltDifferenes( );
            Assert.AreEqual(jd1, actual.jd1);
            Assert.AreEqual(jd3, actual.jd3);
        }

        [TestCase("day10test1", 8)]
        [TestCase("day10test2", 19208)]
        public void Part2(string file, int expected )
        {
            day10 = new Day10(file);
            var actual = day10.SolvePart2( );
            Assert.AreEqual(expected, actual);
        }
    }
}
