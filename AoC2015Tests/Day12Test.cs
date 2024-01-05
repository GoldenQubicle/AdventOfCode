using AoC2015;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AoC2015Tests
{
    public class Day12Test
    {
        Day12 day12;

        [SetUp]
        public void Setup( )
        {
            day12 = new Day12(new List<string> { });
        }
        
        [TestCase("[1,2,3]","6")] 
        [TestCase("{\"a\":2,\"b\":4}", "6")] 
        [TestCase("[[[3]]]","3")] 
        [TestCase("{\"a\":{\"b\":4},\"c\":-1}", "3")]
        [TestCase("[-1,{\"a\":1}]", "0")]
        public void Part1(string input, string expected )
        {
            day12 = new Day12(new List<string> { input } );
            var actual = day12.SolvePart1( ).Result;
            Assert.AreEqual(expected, actual);
        }

        [TestCase("[1,{\"c\":\"red\",\"b\":2},3]", "4")]
        [TestCase("{\"d\":\"red\",\"e\":[1,2,3,4],\"f\":5}", "0")]
        [TestCase("[1,\"red\",5]", "6")]
        [TestCase("[1,{\"c\":\"red\",\"b\":2,\"a\":{\"a\":\"red\"}},3]", "4")]
        public void Part2(string input, string expected)
        {
            day12 = new Day12(new List<string> { input });
            var actual = day12.SolvePart2( ).Result;
            Assert.AreEqual(expected, actual);
        }
    }
}