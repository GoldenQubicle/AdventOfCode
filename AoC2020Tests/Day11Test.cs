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
		var grid = new Grid2d(day11.Input);
		var result = Day11.GetNeighborsInSight(grid[(x,y)], grid);
		Assert.AreEqual(expected, result.Count(c => c.Character == '#'));
	}

	[TestCase(0, 0, 1, 2)]
	[TestCase(9, 0, 0, 3)]
	[TestCase(0, 9, 1, 2)]
	[TestCase(9, 9, 0, 3)]
	public void GetNeighborsInSight(int x, int y, int occupied, int empty)
	{
		day11 = new Day11("day11test2");
		var grid = new Grid2d(day11.Input);
		var result = Day11.GetNeighborsInSight(grid[(x, y)], grid);
		Assert.AreEqual(occupied, result.Count(c => c.Character == '#'));
		Assert.AreEqual(empty, result.Count(c => c.Character == 'L'));
	}
}