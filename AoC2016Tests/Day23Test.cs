namespace AoC2016Tests
{
    public class Day23Test
    {
        Day23 day23;

        [SetUp]
        public void Setup()
        {
            day23 = new Day23("day23");
        }

        [Test]
        public void Part1()
        {
            var actual = day23.SolvePart1().Result;
            Assert.AreEqual("13685", actual);
        }

        [Test]
        public void Part2()
        {
            var actual = day23.SolvePart2().Result;
            Assert.AreEqual("479010245", actual);
        }
    }
}