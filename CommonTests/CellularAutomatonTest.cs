using Common;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CommonTests
{
    public class CellularAutomatonTest
    {
        [Test]
        public void PositionEqualsHashCodeTests( )
        {
            var grid = new Dictionary<Position, int>
            {
                { new Position( new int[]{ 1, 2 }), 5 },
                { new Position( new int[]{ 2, 1 }), 15 }
            };

            var s = grid[new Position(new int[ ] { 2, 1 })];
            Assert.AreEqual(15, s);

            Assert.IsTrue(grid.Keys.First() == new Position(new int[ ] { 1, 2 }));

            Assert.IsFalse(grid.Keys.First() == null);
        }

        [Test]
        public void PositionAddTest( )
        {
            var p1 = new Position(new int[ ] { 2, 7 });
            var p2 = new Position(new int[ ] { 3, -2 });

            var expected = new Position(new int[ ] { 5, 5 });
            var actual = p1 + p2;

            Assert.IsTrue(expected == actual);
        }

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
