using AoC2020.Solutions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2020Tests
{
    class Day19Test
    {
        Day19 day19;
        
        [SetUp]
        public void Setup( )
        {
            day19 = new Day19("day19test1");
        }

        [Test]
        public void Part1( )
        {
            var actual = day19.SolvePart1( );
            Assert.AreEqual(2, actual);
        }

        [Test]
        public void Part1Test2( )
        {
            day19 = new Day19("day19test2");
            var actual = day19.SolvePart1( );
            Assert.AreEqual(3, actual);            
        }

        [Test]
        public void Part2Test2( )
        {
            day19 = new Day19("day19test2");
            var actual = day19.SolvePart2( );
            Assert.AreEqual(12, actual);
        }
    }
}
