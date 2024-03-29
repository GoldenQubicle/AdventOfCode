﻿using System.Collections.Generic;
using System.Linq;
using Common;
using NUnit.Framework;

namespace CommonTests
{
    class Grid2dTest
    {
        private Grid2d grid;
        
   
        [TestCase(0, 0, 'f', 'b', 'g')]
        [TestCase(2, 2, 'g', 'l', 'q', 'h', 'r', 'i', 'n', 's')]
        [TestCase(4, 4, 's', 'x', 't')]
        public void GetNeighborTest(int x, int y, params char[] expected)
        {
            grid = CreateGrid( diagonals: true);
            var cell = new Grid2d.Cell((x, y), 'c');
            var cells = grid.GetNeighbors(cell);
            
            var c = cells.Select(n => n.Character).ToArray();
            
            Assert.AreEqual(expected, c);
            Assert.AreEqual(expected.Length, cells.Count);
        }

        [TestCase(-2, -2, '.', '.', '.', '.', '.', '.', '.', '.')]
        [TestCase(0, 0, '.', '.', '.', '.', 'f', '.', 'b', 'g')]
        [TestCase(2, 2, 'g', 'l', 'q', 'h', 'r', 'i', 'n', 's')]
        [TestCase(4, 4, 's', 'x', '.', 't', '.', '.', '.', '.')]
        public void GetNeighborTestInfinite(int x, int y, params char[ ] expected)
        {
	        grid = CreateGrid(diagonals: true, infinite: true);
	        var cell = new Grid2d.Cell((x, y), 'c');
	        var cells = grid.GetNeighbors(cell);

	        var c = cells.Select(n => n.Character).ToArray( );

	        Assert.AreEqual(expected, c);
	        Assert.AreEqual(expected.Length, cells.Count);
        }

		[Test]
        public void GetCellByQueryTest()
        {
	        grid = CreateGrid(diagonals: true);
			var actual = grid.GetCells(gc => gc.Character == 'q').First();
            var expected = new Grid2d.Cell ((1, 3), 'q');
            Assert.AreEqual(expected, actual);
        }

        private Grid2d CreateGrid(bool diagonals, bool infinite = false) => new (new List<string>
        {
	        "abcde",
	        "fghij",
	        "klmno",
	        "pqrst",
	        "uvwxy"
        }, diagonalAllowed: diagonals, isInfinite: infinite);
	}
}
