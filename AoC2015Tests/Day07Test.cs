using AoC2015;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AoC2015Tests
{
    public class Day07Test
    {
        Day07 day07;

        [SetUp]
        public void Setup( )
        {
            day07 = new Day07("day07");
        }

        [TestCase(123u, 456u, "AND", 72u)]
        [TestCase(123u, 456u, "OR", 507u)]
        [TestCase(123u, 2u, "LSHIFT", 492u)]
        [TestCase(456u, 2u, "RSHIFT", 114u)]
        [TestCase(0u, 123u, "NOT", 65412u)]
        [TestCase(0u, 456u, "NOT", 65079u)]
        [TestCase(1u, 47078u, "AND", 0u)]
        
        public void ConnectGateTest(uint a, uint b, string gate, uint expected)
        {
            var actual = day07.Connect((ushort)a, (ushort)b, gate);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Part1( )
        {
            var actual = day07.SolvePart1( );
            Assert.AreEqual("46065", actual);
        }

        [Test]
        public void Part2( )
        {
            var actual = day07.SolvePart2( );
            Assert.AreEqual("14134", actual);
        }
    }
}