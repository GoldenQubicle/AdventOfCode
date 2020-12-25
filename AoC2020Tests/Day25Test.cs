using AoC2020.Solutions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2020Tests
{
    class Day25Test
    {
        Day25 day25;

        [SetUp]
        public void Setup( )
        {
            day25 = new Day25("day25test1");
        }

        [Test]
        public void Part1( )
        {
            var actual = day25.SolvePart1( );

            var value = 7;
            var div = 20201227;
            var next = value % div;

            Assert.AreEqual(14897079, actual);
        }
    }
}
