namespace AoC2015Tests
{
    public class Day14Test
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
            day14.TravelSeconds = 1000;
            var actual = day14.SolvePart1( ).Result;
            Assert.AreEqual("1120", actual);
        }

        [Test]
        public void Part2( )
        {
            day14.TravelSeconds = 1000;
            var actual = day14.SolvePart2( ).Result;
            Assert.AreEqual("689", actual);
        }
    }
}