namespace AoC2021Tests;

public class Day22Test
{
	[TestCaseSource(nameof(GetTestCasesPart1))]
	public async Task Part1((string file, string expected) test)
	{
		var day22 = new Day22(test.file);
		var actual = await day22.SolvePart1( );
		Assert.That(actual, Is.EqualTo(test.expected));
	}

	[Test]
	public async Task Part2()
	{
		var day22 = new Day22("day22test3");
		var actual = await day22.SolvePart2( );
		Assert.That(actual, Is.EqualTo("2758514936282235"));
	}

	[TestCaseSource(nameof(GetIntersectionTestCases))]
	public void CuboidIntersectTest((Day22.Cuboid step, bool expected) test)
	{
		var c1 = new Day22.Cuboid("on", (-1, 1), (-1, 1), (-1, 1));
		var result = c1.HasOverlap(test.step);
		Assert.That(result, Is.EqualTo(test.expected));
	}

	[TestCaseSource(nameof(GetIntersectionTestCases))]
	public void CuboidIntersectVolumeTest((Day22.Cuboid step, bool expected) test)
	{
		if (test.expected == false)
			return;

		var c1 = new Day22.Cuboid("on", (-1, 1), (-1, 1), (-1, 1));
		var result = c1.GetOverlap(test.step);
		Assert.That(result.Volume, Is.EqualTo(18));
	}

	public static IEnumerable<(string file, string expected)> GetTestCasesPart1()
	{
		yield return ("day22test1", "590784");
		yield return ("day22test2", "39");
	}

	public static IEnumerable<(Day22.Cuboid, bool)> GetIntersectionTestCases()
	{
		yield return (new Day22.Cuboid("on", (0, 2), (-2, 2), (-2, 2)), true);
		yield return (new Day22.Cuboid("on", (10, 12), (-2, 2), (-2, 2)), false);
		yield return (new Day22.Cuboid("on", (-2, 2), (0, 2), (-2, 2)), true);
		yield return (new Day22.Cuboid("on", (-2, 2), (10, 12), (-2, 2)), false);
		yield return (new Day22.Cuboid("on", (-2, 2), (-2, 2), (0, 2)), true);
		yield return (new Day22.Cuboid("on", (-2, 2), (-2, 2), (10, 12)), false);

	}
	[Test]
	public void VolumeTest()
	{
		var c = new Day22.Cuboid("on", (0, 1), (0, 1), (0, 1));
		var v = c.Volume;
		Assert.That(v, Is.EqualTo(8));
	}
}
