namespace AoC2015Tests
{
    public class Day01Test
    {
        [TestCase("(())", "0")]
        [TestCase(")())())", "-3")]
        [TestCase("))(", "-1")]
        public void Part1(string input, string expected)
        {
            var day01 = new Day01(new List<string> { input });
            var actual = day01.SolvePart1( ).Result;
            Assert.AreEqual(expected, actual);
        }

        [TestCase(")", "1")]
        [TestCase("()())", "5")]
        public void Part2(string input, string expected)
        {
            var day01 = new Day01(new List<string> { input });
            var actual = day01.SolvePart2( ).Result;
            Assert.AreEqual(expected, actual);
        }
    }
}