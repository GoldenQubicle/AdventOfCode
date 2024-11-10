namespace AoC2023Tests;
            
public class Day19Test
{
    Day19 day19;
            
    [SetUp]
    public void Setup( )
    {
        day19 = new Day19("day19test1");
    }
    
    [Test]
    public void Part1( )
    {
        var actual = day19.SolvePart1( ).Result;
        Assert.That(actual, Is.EqualTo("19114"));
    }

	[Ignore("not finished")]
	[Test]
    public void Part2( )
    {
        var actual = day19.SolvePart2( ).Result;
        Assert.That(actual, Is.EqualTo("167409079868000"));
    }
}
