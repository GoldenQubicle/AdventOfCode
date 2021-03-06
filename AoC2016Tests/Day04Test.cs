using AoC2016;
using NUnit.Framework;

namespace AoC2016Tests
{
    public class Day04Test
    {
        Day04 day04;

        [SetUp]
        public void Setup( )
        {
            day04 = new Day04("day04test1");
        }
        
        [Test]
        public void Part1( )
        {
            var actual = day04.SolvePart1( );
            Assert.AreEqual("1514", actual);
        }
        
        [Test]
        public void DecryptRoomNameTest( )
        {
            var room = ("qzmt-zixmtkozy-ivhz-", 343, string.Empty);
            var actual = day04.DecryptRoomName(room);
            Assert.AreEqual("very encrypted name ", actual);
        }
    }
}