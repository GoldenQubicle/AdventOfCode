namespace AoC2022Tests
{
    public class Day06Test
    {
        Day06 day06;

        [TestCase("mjqjpqmgbljsphdztnvjfqwrcgsmlb", "7")]
        [TestCase("bvwbjplbgvbhsrlpgdmjqwftvncz", "5")]
        [TestCase("nppdvjthqldpwncqszvftbrmjlhg", "6")]
        [TestCase("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", "10")]
        [TestCase("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", "11")]
        public void Part1(string input, string expected)
        {
            day06 = new Day06(new List<string> { input });
            var actual = day06.SolvePart1().Result;
            Assert.AreEqual(expected, actual);
        }

        [TestCase("mjqjpqmgbljsphdztnvjfqwrcgsmlb", "19")]
        [TestCase("bvwbjplbgvbhsrlpgdmjqwftvncz", "23")]
        [TestCase("nppdvjthqldpwncqszvftbrmjlhg", "23")]
        [TestCase("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", "29")]
        [TestCase("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", "26")]
        public void Part2(string input, string expected)
        {
            day06 = new Day06(new List<string> { input });
            var actual = day06.SolvePart2().Result;
            Assert.AreEqual(expected, actual);
        }
    }
}