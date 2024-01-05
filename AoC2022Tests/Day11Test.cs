namespace AoC2022Tests
{
    public class Day11Test
    {
        Day11 day11;

        [SetUp]
        public void Setup( )
        {
            day11 = new Day11("day11test1");
        }
        
        [Test]
        public void Part1( )
        {
            var actual = day11.SolvePart1( ).Result;
            Assert.AreEqual("10605", actual);
        }

        [Test]
        public void Part2( )
        {
            var actual = day11.SolvePart2( ).Result;
            Assert.AreEqual("2713310158", actual);
        }
    }
}