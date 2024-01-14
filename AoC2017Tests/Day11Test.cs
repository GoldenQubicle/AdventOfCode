namespace AoC2017Tests;
            
public class Day11Test
{
    Day11 day11;
            
    [SetUp]
    public void Setup( )
    {
        day11 = new Day11("day11test1");
    }
    
    [TestCase("ne,ne,ne","3")] 
    [TestCase("ne,ne,sw,sw","0")] 
    [TestCase("ne,ne,s,s","2")] 
    [TestCase("se,sw,se,sw,sw","3")]
    public async Task Part1(string input, string expected )
    {
        day11 = new Day11(new List<string> { input } );
        var actual = await day11.SolvePart1( );
        Assert.That(actual, Is.EqualTo(expected));
    }
            
    [Test]
    public async Task Part2( )
    {
        var expected = string.Empty;
        var actual = await day11.SolvePart2( );
        Assert.That(actual, Is.EqualTo(expected));
    }
}
