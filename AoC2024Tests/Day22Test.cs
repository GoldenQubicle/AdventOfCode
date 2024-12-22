using Common.Extensions;

namespace AoC2024Tests;
            
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
        Assert.That(actual, Is.EqualTo("37327623"));
    }
            
    [Test]
    public async Task Part2( )
    {
		var day22 = new Day22("day22test2");
		var actual = await day22.SolvePart2( );
        Assert.That(actual, Is.EqualTo("23"));
    }

    [Test]
    public void GetNextSecret()
    {
	    var secret = 123L;
	    var result = new List<long>();

        Enumerable.Range(0, 10).ForEach(n =>
        {
	        secret = day22.GetNextSecret(secret);
            result.Add(secret);
		});

        var expected = new List<long>
        {
	        15887950,
	        16495136,
	        527345,
	        704524,
	        1553684,
	        12683156,
	        11100544,
	        12249484,
	        7753432,
	        5908254
        };

        Assert.That(result.SequenceEqual(expected), Is.EqualTo(true));
	}

    [Test]
    public void Prune()
    {
	    var result = day22.Prune(100000000);
        Assert.That(result, Is.EqualTo(16113920));
    }

    [Test]
	public void Mix()
    {
	    var result = day22.Mix(42, 15);
        Assert.That(result, Is.EqualTo(37));
    }
}
