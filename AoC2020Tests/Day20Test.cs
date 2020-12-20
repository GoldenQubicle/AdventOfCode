using AoC2020.Solutions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC2020Tests
{
    class Day20Test
    {
        Day20 day20;

        [SetUp]
        public void Setup( )
        {
            day20 = new Day20("day20test1");
        }

        [Test]
        public void Part1( )
        {
            var actual = day20.SolvePart1( );
            Assert.AreEqual(20899048083289, actual);
        }

        [Test]
        public void Part2( )
        {
            var actual = day20.SolvePart2( );
            Assert.AreEqual(273, actual);
        }

        [Test]
        public void TestTileFlips( )
        {
            var contents = new List<string>
            {
                "123",
                "456",
                "789"
            };

            var tile = new Tile(0, contents);

            var up = tile.GetEdgeUp( );
            Assert.AreEqual("123", up);
            var down = tile.GetEdgeDown( );
            Assert.AreEqual("789", down);
            var left = tile.GetEdgeLeft( );
            Assert.AreEqual("147", left);
            var right = tile.GetEdgeRight( );
            Assert.AreEqual("369", right);

            tile.FlipHorizontal( );

            up = tile.GetEdgeUp( );
            Assert.AreEqual("789", up);
            down = tile.GetEdgeDown( );
            Assert.AreEqual("123", down);
            left = tile.GetEdgeLeft( );
            Assert.AreEqual("741", left);
            right = tile.GetEdgeRight( );
            Assert.AreEqual("963", right);

            tile.FlipVertical( );

            up = tile.GetEdgeUp( );
            Assert.AreEqual("987", up);
            down = tile.GetEdgeDown( );
            Assert.AreEqual("321", down);
            left = tile.GetEdgeLeft( );
            Assert.AreEqual("963", left);
            right = tile.GetEdgeRight( );
            Assert.AreEqual("741", right);

        }

        [Test]
        public void TestTileClockwise( )
        {
            var contents = new List<string>
            {
                "123",
                "456",
                "789"
            };

            var tile = new Tile(0, contents);

            tile.Rotate90Clockwise( );

            var up = tile.GetEdgeUp( );
            Assert.AreEqual("741", up);
            var right = tile.GetEdgeRight( );
            Assert.AreEqual("123", right);
            var down = tile.GetEdgeDown( );
            Assert.AreEqual("963", down);
            var left = tile.GetEdgeLeft( );
            Assert.AreEqual("789", left);
        }

        [Test]
        public void TestTileAntiClockwise( )
        {
            var contents = new List<string>
            {
                "123",
                "456",
                "789"
            };

            var tile = new Tile(0, contents);

            tile.Rotate90AntiClockwise( );

            var up = tile.GetEdgeUp( );
            Assert.AreEqual("369", up);
            var right = tile.GetEdgeRight( );
            Assert.AreEqual("987", right);
            var down = tile.GetEdgeDown( );
            Assert.AreEqual("147", down);
            var left = tile.GetEdgeLeft( );
            Assert.AreEqual("321", left);
        }

        [Test]
        public void TestTileCropped( )
        {
            var contents = new List<string>
            {
                "1234",
                "5678",
                "9ABC",
                "DEFG"
            };

            var tile = new Tile(0, contents);
            tile.TrimEdges( );
            var up = tile.GetEdgeUp( );
            Assert.AreEqual("67", up);
            var right = tile.GetEdgeRight( );
            Assert.AreEqual("7B", right);
            var down = tile.GetEdgeDown( );
            Assert.AreEqual("AB", down);
            var left = tile.GetEdgeLeft( );
            Assert.AreEqual("6A", left);
        }

        [Test]
        public void SearchSeaMonster( )
        {
            day20 = new Day20("day20test2");
            var tile = day20.tiles.Values.First( );
            tile.Rotate90AntiClockwise( );
            tile.FlipHorizontal( );

            var upper = new Regex(".{18}#{1}.{1}#{1}");
            var middle = new Regex("#{1}.{4}#{2}.{4}#{2}.{4}#{3}");
            var lower = new Regex(".{1}#{1}.{2}#{1}.{2}#{1}.{2}#{1}.{2}#{1}.{2}#{1}.{3}");

            var matches = tile.Contents
                .Select(s => middle.Matches(s))
                .Select((m, i) => m.Count == 1 &&
                        upper.IsMatch(tile.Contents[i - 1]) &&
                        lower.IsMatch(tile.Contents[i + 1]) ? true : false)
                .Count(b => b);

            var roughWater = tile.Contents.Sum(s => s.Count(c => c == '#')) - ( matches * 15 );
        }
    }
}
