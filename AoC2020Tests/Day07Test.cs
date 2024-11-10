namespace AoC2020Tests
{
    public class Day07Test
    {
        Day07 day7;

        [SetUp]
        public void Setup()
        {
            day7 = new Day07("day07test1");
        }

        [Test]
        public void Part1( )
        {
            var expected = 4.ToString( );
            var actual = day7.SolvePart1( ).Result;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Part2()
        {
            var expected = 32.ToString( );
            var actual = day7.SolvePart2( ).Result;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Part2test2( )
        {
            day7 = new Day07("day07test2");
            var expected = 126;
            var actual = day7.SolvePart2( ).Result;
            Assert.AreEqual(expected.ToString( ), actual);
        }
    }
}
