namespace AoC2015Tests
{
    public class Day24Test
    {
        Day24 day24;

        [SetUp]
        public void Setup( )
        {
            day24 = new Day24("day24test1");
        }
        
        [Test]
        public void Part1( )
        {
            var actual = day24.SolvePart1( ).Result;
            Assert.AreEqual("99", actual);
        }

        [Test]
        public void Part2( )
        {
            var actual = day24.SolvePart2( ).Result;
            Assert.AreEqual("44", actual);
        }
    }
}