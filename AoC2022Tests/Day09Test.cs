namespace AoC2022Tests
{
    public class Day09Test
    {
        Day09 day09;
        
        [Test]
        public void Part1()
        {
            day09 = new Day09("day09test1");
            var actual = day09.SolvePart1();
            Assert.AreEqual("13", actual);
        }

        [Test]
        public void Part2()
        {
            day09 = new Day09("day09test2");
            var actual = day09.SolvePart2();
            Assert.AreEqual("36", actual);
        }
    }
}