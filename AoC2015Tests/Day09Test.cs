namespace AoC2015Tests
{
    public class Day09Test
    {
        Day09 day09;

        [SetUp]
        public void Setup( )
        {
            day09 = new Day09("day09test1");
        }
        
        [Test]
        public void Part1( )
        {
            var actual = day09.SolvePart1( ).Result;
            Assert.AreEqual("605", actual);
        }

        [Test]
        public void Part2( )
        {
            var actual = day09.SolvePart2( ).Result;
            Assert.AreEqual("982", actual);
        }
    }
}