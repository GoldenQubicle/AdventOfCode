namespace AoC2017Tests;
            
public class Day10Test
{
    [Test]
    public async Task Part1( )
    {
		var day10 = new Day10("day10test1")
		{
			List = new( ) { 0, 1, 2, 3, 4 }
		};

		var actual = await day10.SolvePart1( );
        Assert.That(actual, Is.EqualTo("12"));
    }

	[TestCase("", "a2582a3a0e66e6e86e3812dcb672a272")]
	[TestCase("AoC 2017", "33efeb34ea91902bb2f59c9920caa6cd")]
	[TestCase("1,2,3", "3efbe78a8d82f29979031a4aa0b16a9d")]
	[TestCase("1,2,4", "63960835bcdc130f0b66d7ff4f6a5a8e")]
	public async Task Part2(string input, string expected )
	{
		var day10 = new Day10( new List<string> { input } );
		var actual = await day10.SolvePart2( );
        Assert.That(actual, Is.EqualTo(expected));
    }
}
