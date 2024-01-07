using AoC2017;

namespace AoC2017Tests;
            
public class Day09Test
{
    //Day09 day09;
            
    //[SetUp]
    //public void Setup( )
    //{
    //    day09 = new Day09("day09test1");
    //}

	[TestCase("{}", "1")]
	[TestCase("{{{}}}", "6")]
	[TestCase("{{},{}}", "5")]
	[TestCase("{{{},{},{{}}}}", "16")]
	[TestCase("{<a>,<a>,<a>,<a>}", "1")]
	[TestCase("{{<ab>},{<ab>},{<ab>},{<ab>}}", "9")]
	[TestCase("{{<!!>},{<!!>},{<!!>},{<!!>}}", "9")]
	[TestCase("{{<a!>},{<a!>},{<a!>},{<ab>}}", "3")]
	[TestCase("<{!>}>", "0")]
	public async Task Part1(string input, string expected )
	{
		var day09 = new Day09(new List<string> { input });
        var actual = await day09.SolvePart1( );
        Assert.That(actual, Is.EqualTo(expected));
    }
            
    [Test]
    public async Task Part2( )
    {
	    var day09 = new Day09(new List<string> { "'" });
		var expected = string.Empty;
        var actual = await day09.SolvePart2( );
        Assert.That(actual, Is.EqualTo(expected));
    }
}
