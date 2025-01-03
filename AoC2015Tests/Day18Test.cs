namespace AoC2015Tests
{
    public class Day18Test
    {
        Day18 day18;

        [SetUp]
        public void Setup( )
        {
            day18 = new Day18("day18test1");
            day18.Steps = 4;
        }
        
        [Test]
        public void Part1( )
        {
            var actual = day18.SolvePart1( ).Result;
            Assert.AreEqual("4", actual);
        }

        [Test]
        public void Part2( )
        {
            day18.Steps = 5;
            var actual = day18.SolvePart2( ).Result;
            Assert.AreEqual("17", actual);
        }
    }
}