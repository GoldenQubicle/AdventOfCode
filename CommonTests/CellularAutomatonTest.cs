using Common;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CommonTests
{
    public class CellularAutomatonTest
    {
        [Test]
        public void PositionTests( )
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
    }
}
