using NUnit.Framework;

namespace CommonTests
{
    public class CellularAutomatonTest
    {
        [Test]
        public void FirstUseCase2015Day18( )
        {
            var day18 = new AoC2015.Day18("day18");
            var part1 = day18.SolvePart1();
            var part2 = day18.SolvePart2();
            Assert.AreEqual("814", part1);
            Assert.AreEqual("924", part2);
        }
    }
}
