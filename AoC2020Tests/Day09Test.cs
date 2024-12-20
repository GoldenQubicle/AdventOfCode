﻿namespace AoC2020Tests
{
    class Day09Test
    {
        Day09 day9;

        [SetUp]
        public void Setup( )
        {
            day9 = new Day09("day09test1");
            day9.Preamble = 5;

        }

        [Test]
        public void Part1( )
        {
            var expected = 127L.ToString( );
            var actual = day9.SolvePart1().Result;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Part2( )
        {
            var expected = 62L.ToString( );
            var actual = day9.SolvePart2().Result;
            Assert.AreEqual(expected, actual);
        }

    }
}
