using AoC2020;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AoC2020Tests;

class Day11Test
{
	Day11 day11;

	[SetUp]
	public void Setup( )
	{
		day11 = new Day11("day11test1");
	}

	[Test]
	public void Part1( )
	{
		var actual = day11.SolvePart1( ).Result;
		Assert.AreEqual(37.ToString( ), actual);
	}

	[Test]
	public void Part2( )
	{
		day11 = new Day11("day11test6");
		var actual = day11.SolvePart2( ).Result;
		Assert.AreEqual(26.ToString( ), actual);
	}

	[TestCase("day11test3", 8, 3, 4)]
	[TestCase("day11test4", 0, 3, 3)]
	[TestCase("day11test5", 0, 3, 3)]
	public void EmptyEight(string file, int expected, int x, int y)
	{
		day11 = new Day11(file);
		var result = day11.GetNeighborsInSight(x, y, day11.GetInitialState( ));
		Assert.AreEqual(expected, result.Where(c => c == '#').Count( ));
	}

	[TestCase(0, 0, 1, 2)]
	[TestCase(9, 0, 0, 3)]
	[TestCase(0, 9, 1, 2)]
	[TestCase(9, 9, 0, 3)]
	public void GetNeighborsInSight(int x, int y, int occupied, int empty)
	{
		day11 = new Day11("day11test2");
		var result = day11.GetNeighborsInSight(x, y, day11.GetInitialState( ));
		Assert.AreEqual(occupied, result.Where(c => c == '#').Count( ));
		Assert.AreEqual(empty, result.Where(c => c == 'L').Count( ));
	}

	[TestCase(0, 0, 1, 2)]
	[TestCase(9, 0, 0, 3)]
	[TestCase(0, 9, 2, 1)]
	[TestCase(9, 9, 1, 2)]
	public void GetNeighbors(int x, int y, int floor, int seat)
	{
		var result = day11.GetNeighbors(x, y, day11.GetInitialState( ));
		Assert.AreEqual(seat, result.Where(c => c == 'L').Count( ));
		Assert.AreEqual(floor, result.Where(c => c == '.').Count( ));
	}

	[TestCase('b', true)]
	[TestCase('c', false)]
	public void SameState(char d, bool expected)
	{
		var old = new List<char[ ]> { new char[ ] { 'a', 'b' }, new char[ ] { 'a', 'b' } };
		var update = new List<char[ ]> { new char[ ] { 'a', d }, new char[ ] { 'a', 'b' } };
		var result = day11.SameState(old, update);
		Assert.AreEqual(expected, result);
	}
}