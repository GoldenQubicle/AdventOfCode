namespace AoC2022Tests
{
    public class Day16Test
    {
        Day16 day16;

        [SetUp]
        public void Setup( )
        {
            day16 = new Day16("day16test1");
        }
        
        [Test]
        public void Part1( )
        {
            var actual = day16.SolvePart1( );
            Assert.AreEqual("1651", actual);
        }

        [Test]
        public void Part2( )
        {
            var actual = day16.SolvePart2( );
            Assert.AreEqual("", actual);
        }
    }
}