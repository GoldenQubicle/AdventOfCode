namespace AoC2023Tests;
            
public class Day15Test
{
    Day15 day15;
            
    [SetUp]
    public void Setup( )
    {
        day15 = new Day15("day15test1");
    }
    
    [Test]
    public void Part1( )
    {
        var actual = day15.SolvePart1( ).Result;
        Assert.That(actual, Is.EqualTo("1320"));
    }
            
    [Test]
    public void Part2( )
    {
        var actual = day15.SolvePart2( ).Result;
        Assert.That(actual, Is.EqualTo("145"));
    }

    [TestCase('H', 0, 200)]
    [TestCase('A', 200, 153)]
    [TestCase('S', 153, 172)]
    [TestCase('H', 172, 52)]
    
    public void HashShouldBeCorrect(char input, int v, int expected)
    {
	    var actual = Day15.CalculateHash(input, v);
        Assert.That(actual, Is.EqualTo(expected));
    }
}
