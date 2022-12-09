namespace AoC2022Tests
{
    public class Day09Test
    {
        Day09 day09;

        [SetUp]
        public void Setup()
        {
            day09 = new Day09("day09test1");
        }

        [Test]
        public void Part1()
        {
            var actual = day09.SolvePart1();
            Assert.AreEqual("13", actual);
        }

        [Test]
        public void Part2()
        {
            var actual = day09.SolvePart2();
            Assert.AreEqual("", actual);
        }

        [TestCase(0, 0, 0, 0, true)]
        [TestCase(1, 1, 0, 0, true)]
        [TestCase(2, 1, 0, 0, false)]
        [TestCase(1, 2, 0, 0, false)]
        [TestCase(1, -2, 1, -1, true)]
        public void OverlapTest(int hx, int hy, int tx, int ty, bool expected)
        {
            var actual = Day09.OverLaps((hx, hy), (tx, ty));
            Assert.AreEqual(actual, expected);
        }
    }
}