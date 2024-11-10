namespace AoC2020Tests
{
    class Day25Test
    {
        Day25 day25;

        [SetUp]
        public void Setup( )
        {
            day25 = new Day25("day25test1");
        }

        [Test]
        public void Part1( )
        {
            var actual = day25.SolvePart1( ).Result;         
            Assert.AreEqual(14897079.ToString( ), actual);
        }
    }
}
