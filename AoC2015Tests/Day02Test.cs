namespace AoC2015Tests
{
    public class Day02Test
    {

        [TestCase("2x3x4", "58")]
        [TestCase("1x1x10", "43")]
        public void Part1(string input, string expected)
        {
            var day02 = new Day02(new List<string> { input });
            var actual = day02.SolvePart1( ).Result;
            Assert.AreEqual(expected, actual);
        }

        [TestCase("2x3x4", "34")]
        [TestCase("1x1x10", "14")]
        public void Part2(string input, string expected)
        {
            var day02 = new Day02(new List<string> { input });
            var actual = day02.SolvePart2( ).Result;
            Assert.AreEqual(expected, actual);
        }
    }
}