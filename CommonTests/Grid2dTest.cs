using System.Collections.Generic;
using System.Linq;
using Common;
using NUnit.Framework;

namespace CommonTests
{
    class Grid2dTest
    {
        private Grid2d grid;

        [SetUp]
        public void Setup()
        {
            grid = new Grid2d(new List<string>
            {
                "abcde",
                "fghij",
                "klmno",
                "pqrst",
                "uvwxy"
            });
        }

        [TestCase(0, 0, 'f', 'b', 'g')]
        [TestCase(2, 2, 'g', 'l', 'q', 'h', 'r', 'i', 'n', 's')]
        [TestCase(4, 4, 's', 'x', 't')]
        public void GetNeighborTest(int x, int y, params char[] expected)
        {
            var cell = new Grid2d.Cell(new Position(x, y), 'c');
            var cells = grid.GetNeighbors(cell);
            
            var c = cells.Select(n => n.Character).ToArray();
            
            Assert.AreEqual(expected, c);
            Assert.AreEqual(expected.Length, cells.Count);
        }

        [Test]
        public void GetCellByQueryTest()
        {
            var actual = grid.QueryCells(gc => gc.Character == 'q').First();
            var expected = new Grid2d.Cell (new Position(1, 3), 'q');
            Assert.AreEqual(expected, actual);
        }
    }
}
