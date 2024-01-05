namespace AoC2022Tests
{
    public class Day05Test
    {
        Day05 day05;

        [SetUp]
        public void Setup( )
        {
            day05 = new Day05("day05test2");
        }
        
        [Test]
        public void Part1( )
        {
            var actual = day05.SolvePart1( ).Result;
            Assert.AreEqual("CMZ", actual);
        }

        [Test]
        public void Part2( )
        {
            var actual = day05.SolvePart2( ).Result;
            Assert.AreEqual("MCD", actual);
        }
    }
}