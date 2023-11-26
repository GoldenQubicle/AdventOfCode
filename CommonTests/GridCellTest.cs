using Common;
using NUnit.Framework;

namespace CommonTests
{
    class GridCellTest
    {
        [TestCase(2, 5, '#', 2, 5, '#', true)]
        [TestCase(2, 5, '#', 2, 5, '.', false)]
        [TestCase(2, 5, '#', 1, 5, '#', false)]
        [TestCase(5, 1, '#', 1, 5, '#', false)]
        public void GridCellShouldBeEquatable(int x1, int y1, char c1, int x2, int y2, char c2, bool expected)
        {
            var gc1 = new Grid2d.Cell((x1, y1), c1);
            var gc2 = new Grid2d.Cell((x2, y2), c2);

            Assert.AreEqual(gc1 == gc2, expected);
            Assert.AreEqual(gc1 != gc2, !expected);
            Assert.AreEqual(gc2.Equals(gc1), expected);
        }
    }
}
