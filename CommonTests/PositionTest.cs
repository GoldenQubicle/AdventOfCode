using System.Collections.Generic;
using System.Linq;
using Common;
using NUnit.Framework;

namespace CommonTests
{
    public class PositionTest
    {

        [Test]
        public void PositionEqualsHashCodeTests()
        {
            var grid = new Dictionary<Position, int>
            {
                { new Position(1, 2), 5 },
                { new Position(2, 1), 15 }
            };

            var s = grid[new Position(2, 1)];
            Assert.AreEqual(15, s);

            Assert.IsTrue(grid.Keys.First() == new Position(1, 2));

            Assert.IsFalse(grid.Keys.First() == null);
        }

        [Test]
        public void PositionAddTest()
        {
            var p1 = new Position(2, 7);
            var p2 = new Position(3, -2);

            var expected = new Position(5, 5);
            var actual = p1 + p2;

            Assert.IsTrue(expected == actual);
        }
    }
}
