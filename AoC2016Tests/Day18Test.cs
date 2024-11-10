namespace AoC2016Tests
{
    public class Day18Test
    {
        Day18 day18;

        [SetUp]
        public void Setup( )
        {
            day18 = new Day18("day18test1");
        }
        
        [Test]
        public void Part1( )
        {
            day18.TotalRows = 10;
            var actual = day18.SolvePart1( ).Result;
            Assert.AreEqual("38", actual);
        }

        [Test]
        public void Part2( )
        {
            var actual = day18.SolvePart2( ).Result;
            Assert.AreEqual("1935478", actual);
        }
    }
}