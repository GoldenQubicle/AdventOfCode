namespace AoC2021Tests;
            
public class Day22Test
{
    Day22 day22;
            
    [SetUp]
    public void Setup( )
    {
        day22 = new Day22("day22test1");
    }
    
    [Test]
    public async Task Part1( )
    {
        var actual = await day22.SolvePart1( );
        Assert.That(actual, Is.EqualTo("590784"));
    }
            
    [Test]
    public async Task Part2( )
    {
        var expected = string.Empty;
        var actual = await day22.SolvePart2( );
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCaseSource(nameof(GetIntersectionTestCases))]
    public void CuboidIntersectTest((Day22.Cuboid step, bool expected) test)
    {
	    var c1 = new Day22.Cuboid("on", (-1, 1), (-1, 1), (-1, 1));
	    var result = Day22.Intersects(c1, test.step);
        Assert.That(result, Is.EqualTo(test.expected));
	}

    [TestCaseSource(nameof(GetIntersectionTestCases))]
	public void CuboidIntersectVolumeTest((Day22.Cuboid step, bool expected) test)
	{
		if (test.expected == false) return;

		var c1 = new Day22.Cuboid("on", (-1, 1), (-1, 1), (-1, 1));
		var result = Day22.GetIntersectVolume(c1, test.step);
        Assert.That(result.GetVolume(), Is.EqualTo(4));
	}

    public static IEnumerable <(Day22.Cuboid, bool)> GetIntersectionTestCases()
    {
	    yield return (new Day22.Cuboid("on", (0, 2), (-2, 2), (-2, 2)), true);
	    yield return (new Day22.Cuboid("on", (10, 12), (-2, 2), (-2, 2)), false);
		yield return (new Day22.Cuboid("on", (-2, 2), (0, 2), (-2, 2)), true);
		yield return (new Day22.Cuboid("on", (-2, 2), (10, 12), (-2, 2)), false);
	    yield return (new Day22.Cuboid("on", (-2, 2), (-2, 2), (0, 2)), true);
	    yield return (new Day22.Cuboid("on", (-2, 2), (-2, 2), (10, 12)), false);
	    
	}
}
