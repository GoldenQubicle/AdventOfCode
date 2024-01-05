namespace AoC2022Tests
{
    public class Day15Test
    {
        Day15 day15;

        [SetUp]
        public void Setup( )
        {
            day15 = new Day15("day15test1");
        }
        
        [Test]
        public void Part1( )
        {
            day15.RowToCheck = 10;
            var actual = day15.SolvePart1( ).Result;
            Assert.AreEqual("26", actual);
        }

        [Test]
        public void Part2( )
        {
            day15.SearchArea = (0, 20);
            var actual = day15.SolvePart2( ).Result;
            Assert.AreEqual("56000011", actual);
        }
    }
}