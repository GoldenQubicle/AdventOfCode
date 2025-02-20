﻿namespace AoC2020Tests
{
    class Day15Test
    {
        Day15 day15;

        [TestCase("day15test1", 436)]
        public void SolvePart1(string file, int expected )
        {
            day15 = new Day15(file);
            var actual = day15.SolvePart1( ).Result;
            Assert.AreEqual(expected.ToString( ), actual);
        }

        [TestCase("day15test1", 175594)]
        public void SolvePart2(string file, int expected)
        {
            day15 = new Day15(file);
            var actual = day15.SolvePart2( ).Result;
            Assert.AreEqual(expected.ToString( ), actual);
        }
    }
}
