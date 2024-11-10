namespace AoC2015Tests
{
    public class Day10Test
    {
        Day10 day10;

        [SetUp]
        public void Setup( )
        {
            day10 = new Day10(new List<string>());
        }
        
        [TestCase("1","11")] 
        [TestCase("11","21")] 
        [TestCase("21","1211")] 
        [TestCase("1211","111221")] 
        [TestCase("111221","312211")]
        public void LookAndSayTest(string input, string expected )
        {
            var actual = day10.LookAndSay(input);
            Assert.AreEqual(expected, actual);
        }
    }
}