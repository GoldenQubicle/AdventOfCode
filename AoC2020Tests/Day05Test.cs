using AoC2020;
using NUnit.Framework;

namespace AoC2020Tests
{
    public class Day05Test
    {
        Day05 day5;
        
        [SetUp]
        public void Setup( )
        {
            day5 = new Day05("day05test1");
        }


        [Test]
        public void Part1( )
        {
            var actual = day5.SolvePart1( );
            Assert.AreEqual(820.ToString( ), actual);
        }

        [TestCase("BFFFBBFRRR", 70, 7, 567)]
        public void StringShouldBePartitioned(string s, int row, int col, int id)
        {
            var actual = day5.PartitionString(s);
            Assert.AreEqual(row, actual.row);
            Assert.AreEqual(col, actual.col);
            Assert.AreEqual(id, actual.id);
        }


        [TestCase('F', 0, 127, 128, 0, 63, 64)]
        [TestCase('B', 0, 63, 64, 32, 63, 32)]
        [TestCase('F', 32, 63, 32, 32, 47, 16)]
        [TestCase('B', 32, 47, 16, 40, 47, 8)]
        [TestCase('B', 40, 47, 8, 44, 47, 4)]        
        [TestCase('R', 0, 7, 8, 4, 7, 4)]        
        [TestCase('L', 4, 7, 4, 4, 5, 2)]        
        [TestCase('R', 4, 5, 2, 5, 5, 1)]        
        public void PartitioningTest(char c, int min, int max, int div, int minE, int maxE, int divE )
        {
            var actual = day5.Partition(c, (min, max, div));
            Assert.AreEqual(minE, actual.min);
            Assert.AreEqual(maxE, actual.max);
            Assert.AreEqual(divE, actual.div);
        }
    }
}
