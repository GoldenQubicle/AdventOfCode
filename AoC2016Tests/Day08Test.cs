using AoC2016;
using NUnit.Framework;
using System.Collections.Generic;

namespace AoC2016Tests
{
    public class Day08Test
    {
        Day08 day08;

        [SetUp]
        public void Setup( )
        {
            day08 = new Day08(new List<string>());
        }

        [Test]
        public void Part1( )
        {
            day08 = new Day08(nameof(Day08));
            var actual = day08.SolvePart1();
            Assert.AreEqual("121", actual);
        }

        [Test]
        public void UseCaseTest( )
        {
            day08.InitializeScreen(7, 3);
            day08.AddRect(3, 2);
            
            day08.ShiftColumn(1, 1);
            var shift1 = new List<List<int>>
            {
                new(){1,0,1,0,0,0,0},
                new(){1,1,1,0,0,0,0},
                new(){0,1,0,0,0,0,0},
            };
            Assert.AreEqual(shift1, day08.Screen);

            day08.ShiftRow(0, 4);
            var shift2 = new List<List<int>>
            {
                new(){0,0,0,0,1,0,1},
                new(){1,1,1,0,0,0,0},
                new(){0,1,0,0,0,0,0},
            };
            Assert.AreEqual(shift2, day08.Screen);

            day08.ShiftColumn(1, 1);
            var shift3 = new List<List<int>>
            {
                new(){0,1,0,0,1,0,1},
                new(){1,0,1,0,0,0,0},
                new(){0,1,0,0,0,0,0},
            };
            Assert.AreEqual(shift3, day08.Screen);
        }

        [Test]
        public void AddRectToScreenTest( )
        {
            day08.InitializeScreen(8, 3);
            day08.AddRect(3, 2);
            var expected = new List<List<int>>
            {
                new() {1, 1, 1, 0, 0, 0, 0, 0},
                new() {1, 1, 1, 0, 0, 0, 0, 0},
                new() {0, 0, 0, 0, 0, 0, 0, 0},
            };
            Assert.AreEqual(expected, day08.Screen);
        }


        [Test]
        public void ShiftPixelsTest( )
        {
            var column = new[ ] { "a", "b", "c", "d", "e", };
            var shift = 2;
            var expected = new[ ] { "d", "e", "a", "b", "c", };
            var actual = day08.ShiftPixels(column, shift);
            Assert.AreEqual(expected, actual);
        }
    }
}