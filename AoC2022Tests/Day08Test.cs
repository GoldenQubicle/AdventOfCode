namespace AoC2022Tests
{
    public class Day08Test
    {
        Day08 day08;

        [SetUp]
        public void Setup( )
        {
            day08 = new Day08("day08test1");
        }
        
        [Test]
        public void Part1( )
        {
            var actual = day08.SolvePart1( ).Result;
            Assert.AreEqual("21", actual);
        }

        [Test]
        public void Part2( )
        {
            var actual = day08.SolvePart2( ).Result;
            Assert.AreEqual("8", actual);
        }
    }
}