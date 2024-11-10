namespace AoC2020Tests
{
    class Day21Test
    {
        Day21 day21;

        [SetUp]
        public void Setup( )
        {
            day21 = new Day21("day21test1");
        }

        [Test]
        public void Part1( )
        {
            var actual = day21.SolvePart1( ).Result;
            Assert.AreEqual("5", actual);
        }

        [Test]
        public void Part2( )
        {
            var actual = day21.SolvePart2( ).Result;
            Assert.AreEqual("mxmxvkd,sqjhc,fvjkl", actual);
        }
    }
}
