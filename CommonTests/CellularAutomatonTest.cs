using System.Threading.Tasks;
using NUnit.Framework;

namespace CommonTests
{
    public class CellularAutomatonTest
    {
        [Test]
        public async Task UseCase2015Day18( )
        {
            var day18 = new AoC2015.Day18("day18");
            
            Assert.AreEqual("814", await day18.SolvePart1( ));
            Assert.AreEqual("924", await day18.SolvePart2( ));
        }

        [Test]
        public async Task UseCase2020Day11()
        {
	        var day18 = new AoC2020.Day11("day11");
	        
            Assert.AreEqual("2222", await day18.SolvePart1( ));
            Assert.AreEqual("2032", await day18.SolvePart2( ));
        }
	}
}
