using NUnit.Framework;

namespace CommonTests
{
    public class CellularAutomatonTest
    {
        [Test]
        public void FirstUseCase2015Day18( )
        {
            var day18 = new AoC2015.Day18("day18");
            var part1 = day18.SolvePart1().Result;
            var part2 = day18.SolvePart2().Result;
            Assert.AreEqual("814", part1);
            Assert.AreEqual("924", part2);
        }

        [Test]
        public void UseCase2020Day11()
        {
	        var day18 = new AoC2020.Day11("day11");
	        //var part1 = day18.SolvePart1( ).Result;
            var part2 = day18.SolvePart2( ).Result;
            //Assert.AreEqual("2222", part1);
            Assert.AreEqual("2032", part2);
        }
	}
}
