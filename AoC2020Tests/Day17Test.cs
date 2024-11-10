namespace AoC2020Tests
{
    class Day17Test
    {
        Day17 day17;

        [SetUp]
        public void Setup( )
        {
            day17 = new Day17("day17test1");
        }

        [Test]
        public void Part1( )
        {
            var actual = day17.SolvePart1( ).Result;
            Assert.AreEqual(112.ToString( ), actual);
        }

        [Test]
        public void Part2( )
        {
            var actual = day17.SolvePart2( ).Result;
            Assert.AreEqual(848.ToString( ), actual);
        }


        [Test]
        public void GetNeighbors( )
        {
            var pos = (1, 1, 0);
            var n = day17.GetNeighbors3D(pos);
            Assert.AreEqual(5, n.Values.Count(v => v == '#'));
            Assert.AreEqual(21, n.Count(kvp => kvp.Value == '.' ));
        }
    }
}
