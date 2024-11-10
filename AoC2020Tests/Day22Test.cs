namespace AoC2020Tests
{
    class Day22Test
    {
        Day22 day22;

        [SetUp]
        public void Setup( )
        {
            day22 = new Day22("day22test1");
        }

        [Test]
        public void Part1( )
        {
            var actual = day22.SolvePart1( ).Result;
            Assert.AreEqual(306.ToString( ), actual);
        }

        [Test]
        public void Part2( )
        {
            var actual = day22.SolvePart2( ).Result;
            Assert.AreEqual(291.ToString( ), actual);
        }       
    }
}
