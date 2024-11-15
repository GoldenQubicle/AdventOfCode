namespace AoC2016Tests
{
    public class Day14Test
    {
        Day14 day14;

        [SetUp]
        public void Setup( )
        {
            day14 = new Day14(new List<string> { "abc" });
        }
        
        [Test]
        public void Part1( )
        {
            var actual = day14.SolvePart1( ).Result;
            Assert.AreEqual("22728", actual);
        }

        [Test]
        public void Part2( )
        {
            var actual = day14.SolvePart2( ).Result;
            Assert.AreEqual("22551", actual);
        }
    }
}