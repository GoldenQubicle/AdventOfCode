namespace AoC2021Tests;
            
public class Day17Test
{
    Day17 day17;
            
    [SetUp]
    public void Setup( )
    {
        day17 = new Day17("day17test1");
    }
    
    [Test]
    public async Task Part1( )
    {
        var actual = await day17.SolvePart1( );
        Assert.That(actual, Is.EqualTo("45"));
    }
            
    [Test]
    public async Task Part2( )
    {
        var actual = await day17.SolvePart2( );
        Assert.That(actual, Is.EqualTo("112"));
    }

    [TestCase(7f, 2f, true)]
    [TestCase(6f, 3f, true)]
    [TestCase(9f, 0f, true)]
    [TestCase(17f, -4f, false)]
    public void DoesHitTargetAreaTest(float x, float y, bool expected)
    {
	    var actual = Day17.DoesHitTargetArea(x, y);
        Assert.That(actual.hit, Is.EqualTo(expected));
    }
}
