namespace AoC2015Tests
{
    public class Day16Test
    {
        Day16 day16;

        [SetUp]
        public void Setup( )
        {
            //note no test for this day, using actual input
            day16 = new Day16("day16");
        }
        
        [Test]
        public void Part1( )
        {
            var actual = day16.SolvePart1( ).Result;
            Assert.AreEqual("103", actual);
        }

        [Test]
        public void Part2( )
        {
            var actual = day16.SolvePart2( ).Result;
            Assert.AreEqual("405", actual);
        }
    }
}