namespace AoC2022Tests
{
    public class Day01Test
    {
        Day01 day01;

        [SetUp]
        public void Setup( )
        {
            day01 = new Day01("day01test1");
        }
        
        [Test]
        public void Part1( )
        {
            var actual = day01.SolvePart1( ).Result;
            Assert.AreEqual("24000", actual);
        }

        [Test]
        public void Part2( )
        {
            var actual = day01.SolvePart2( ).Result;
            Assert.AreEqual("45000", actual);
        }
    }
}