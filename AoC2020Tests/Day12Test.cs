namespace AoC2020Tests
{
    class Day12Test
    {
        Day12 day12;

        [SetUp]
        public void Setup( )
        {
            day12 = new Day12("day12test1");
        }

        [Test]
        public void Part1( )
        {
            var actual = day12.SolvePart1( ).Result;
            Assert.AreEqual(25.ToString( ), actual);
        }

        [Test]
        public void Part2( )
        {
            var actual = day12.SolvePart2( ).Result;
            Assert.AreEqual(286.ToString( ), actual);
        }
    }
}
