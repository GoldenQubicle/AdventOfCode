namespace AoC2024Tests;
            
public class Day17Test
{
    //Day17 day17;
            
    //[SetUp]
    //public void Setup( )
    //{
    //    //day17 = new Day17("day17test1");
    //}

	[TestCaseSource(nameof(GetCasesPart1))]
	public async Task Part1((string file, string expected) testCase)
    {
		var day17 = new Day17(testCase.file);
		var actual = await day17.SolvePart1( );
        Assert.That(actual, Is.EqualTo(testCase.expected));
    }

    [TestCaseSource(nameof(GetRegisterTestCases))]
	public void TestRegisters((List<string> input, char register, int expected) test)
	{
        var day17 = new Day17(test.input);

        day17.SolvePart1();

        Assert.That(day17.registers[test.register], Is.EqualTo(test.expected));
	}
            
    [Test]
    public async Task Part2( )
    {
	    var day17 = new Day17(["2024", "0", "0", " :0,3,5,4,3,0"]);
	    var actual = await day17.SolvePart2();
	    Assert.That(actual, Is.EqualTo("117440"));
    }

    private static IEnumerable<(List<string> input, char register, int expected)> GetRegisterTestCases()
    {
	    yield return (["0", "0", "9", " : 2,6"], 'B', 1);
	    yield return (["0", "29", "0", " : 1,7"], 'B', 26);
	    yield return (["0", "2024", "43690", " : 4,0"], 'B', 44354);
	    yield return (["2024", "0", "43690", " : 0,1,5,4,3,0"], 'A', 0);
    }


    private static IEnumerable<(string file, string expected)> GetCasesPart1()
    {
	    yield return ("day17test1", "4,6,3,5,6,3,5,2,1,0");
	    yield return ("day17test3", "0,1,2");
	    yield return ("day17test4", "4,2,5,6,7,7,7,7,3,1,0");
    }
}
