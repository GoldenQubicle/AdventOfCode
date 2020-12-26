using AoC2020;
using NUnit.Framework;
using System.Collections.Generic;

namespace AoC2020Tests
{
    class Day14Test
    {
        Day14 day14;

        [SetUp]
        public void Setup( )
        {
            day14 = new Day14("day14test1");
        }

        [Test]
        public void Part1( )
        {
            var actual = day14.SolvePart1( );
            Assert.AreEqual(165, actual);
        }

        [Test]
        public void Part2( )
        {
            day14 = new Day14("day14test2");
            var actual = day14.SolvePart2( );
            Assert.AreEqual(208, actual);
        }

        [Test]
        public void GenerateFloatingAdresses( )
        {
            var floating = "000000000000000000000000000000X1101X";
            var actual = day14.GenerateFloatingAdresses(floating);
            Assert.AreEqual(4, actual.Count);
            var expected = new List<string>
            {
                "000000000000000000000000000000011010",
                "000000000000000000000000000000011011",
                "000000000000000000000000000000111010",
                "000000000000000000000000000000111011",
            };

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ApplyBitMaskAdress( )
        {
            var mask = "000000000000000000000000000000X1001X";
            var adress = day14.To36Bit(42);
            var expected = "000000000000000000000000000000X1101X";
            var actual = day14.ApplyBitMaskToAdress(adress, mask);
            Assert.AreEqual(expected, actual);

        }

        [TestCase(8, "000000000000000000000000000000001000")]
        [TestCase(11, "000000000000000000000000000000001011")]
        [TestCase(73, "000000000000000000000000000001001001")]
        [TestCase(101, "000000000000000000000000000001100101")]
        public void To36Bit(int v, string e)
        {
            var a = day14.To36Bit(v);
            Assert.AreEqual(e, a);
        }

        [TestCase("000000000000000000000000000000001000", 8)]
        [TestCase("000000000000000000000000000000001011", 11)]
        [TestCase("000000000000000000000000000001001001", 73)]
        [TestCase("000000000000000000000000000001100101", 101)]
        public void From36Bit(string v, long e)
        {
            var a = day14.From36Bit(v);
            Assert.AreEqual(e, a);
        }

        [TestCase(11, 73, "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X")]
        [TestCase(0, 64, "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X")]
        public void ApplyMask(long input, long expected, string mask)
        {
            var actual = day14.ApplyBitMask(input, mask);
            Assert.AreEqual(expected, actual);
        }
    }
}
