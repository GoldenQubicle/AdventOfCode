namespace AoC2015Tests
{
    public class Day25Test
    {
        Day25 day25;

        [SetUp]
        public void Setup( )
        {
            day25 = new Day25(new List<string>());
        }
        
        [Test]
        public void Part1( )
        {
            var actual = day25.SolvePart1( ).Result;
            Assert.AreEqual("2650453", actual);
        }
    }
}