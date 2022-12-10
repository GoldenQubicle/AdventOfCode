namespace AoC2022Tests
{
    public class Day10Test
    {
        Day10 day10;

        [SetUp]
        public void Setup()
        {
            day10 = new Day10("day10test1");
        }

        [TestCase("day10test1", "13140")]
        [TestCase("day10test0", "0")]
        public void Part1(string file, string expected)
        {
            day10 = new Day10(file);
            var actual = day10.SolvePart1();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Part2()
        {
            var actual = day10.SolvePart2().Replace(Environment.NewLine, string.Empty);
            var expected = "##..##..##..##..##..##..##..##..##..##.."+
                           "###...###...###...###...###...###...###."+
                           "####....####....####....####....####...."+
                           "#####.....#####.....#####.....#####....."+
                           "######......######......######......####"+
                           "#######.......#######.......#######.....";
            Assert.AreEqual(expected, actual);
        }
    }
}