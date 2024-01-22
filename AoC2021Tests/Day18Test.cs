namespace AoC2021Tests;

public class Day18Test
{
	Day18 day18;

	[SetUp]
	public void Setup()
	{
		day18 = new Day18("day18test1");
	}

	[Test]
	public async Task Part1()
	{
		var actual = await day18.SolvePart1( );
		Assert.That(actual, Is.EqualTo("4140"));
	}

	[Test]
	public async Task Part2()
	{
		var actual = await day18.SolvePart2( );
		Assert.That(actual, Is.EqualTo("3993"));
	}

	[Test]
	public void Add()
	{
		var actual = Day18.Add("[1,2]", "[[3,4],5]");
		Assert.That(actual, Is.EqualTo("[[1,2],[[3,4],5]]"));
	}

	[TestCase("[[[[[9,8],1],2],3],4]", "[[[[0,9],2],3],4]")]
	[TestCase("[7,[6,[5,[4,[3,2]]]]]", "[7,[6,[5,[7,0]]]]")]
	[TestCase("[[6,[5,[4,[3,2]]]],1]", "[[6,[5,[7,0]]],3]")]
	[TestCase("[[3,[2,[1,[7,3]]]],[6,[5,[4,[3,2]]]]]", "[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]")]
	[TestCase("[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]", "[[3,[2,[8,0]]],[9,[5,[7,0]]]]")]
	[TestCase("[[[[12,12],[6,14]],[[15,0],[17,[8,1]]]],[2,9]]", "[[[[12,12],[6,14]],[[15,0],[25,0]]],[3,9]]")]
	public void Explode(string n, string expected)
	{
		Day18.TryExplode(n, out var actual);
		Assert.That(actual, Is.EqualTo(expected));
	}

	[TestCase("[[[[0,7],4],[15,[0,13]]],[1,1]]", "[[[[0,7],4],[[7,8],[0,13]]],[1,1]]")]
	[TestCase("[[[[0,7],4],[[7,8],[0,13]]],[1,1]]", "[[[[0,7],4],[[7,8],[0,[6,7]]]],[1,1]]")]
	public void Split(string n, string expected)
	{
		Day18.TrySplit(n, out var actual);
		Assert.That(actual, Is.EqualTo(expected));
	}

	[TestCase("[[1,2],[[3,4],5]]", 143)]
	[TestCase("[[[[0,7],4],[[7,8],[6,0]]],[8,1]]", 1384)]
	[TestCase("[[[[1,1],[2,2]],[3,3]],[4,4]]", 445)]
	[TestCase("[[[[3,0],[5,3]],[4,4]],[5,5]]", 791)]
	[TestCase("[[[[5,0],[7,4]],[5,5]],[6,6]]", 1137)]
	[TestCase("[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]", 3488)]
	[TestCase("[[9,1],[1,9]]", 129)]
	public void GetMagnitude(string n, int expected)
	{
		var actual = Day18.CalculateMagnitude(n);
		Assert.That(actual, Is.EqualTo(expected));
	}

	[TestCaseSource(nameof(GetReduceTestCases))]
	public void Reduce((List<string> input, string expected) test )
	{
		var actual = Day18.Reduce(test.input);
		Assert.That(actual, Is.EqualTo(test.expected));
	}

	[Test]
	public void ActuallyReduce()
	{
		var actual = Day18.ActuallyReduce("[[[[[6,6],[6,6]],[[6,0],[6,7]]],[[[7,7],[8,9]],[8,[8,1]]]],[2,9]]");
		Assert.That(actual, Is.EqualTo("[[[[6,6],[7,7]],[[0,7],[7,7]]],[[[5,5],[5,6]],9]]"));
	}

	private static IEnumerable<(List<string> input, string expected)> GetReduceTestCases()
	{
		yield return (new List<string> { "[[[[4,3],4],4],[7,[[8,4],9]]]", "[1,1]" }, "[[[[0,7],4],[[7,8],[6,0]]],[8,1]]");
		yield return (new List<string> { "[1,1]", "[2,2]", "[3,3]", "[4,4]" }, "[[[[1,1],[2,2]],[3,3]],[4,4]]");
		yield return (new List<string> { "[1,1]", "[2,2]", "[3,3]", "[4,4]", "[5,5]" }, "[[[[3,0],[5,3]],[4,4]],[5,5]]");
		yield return (new List<string> { "[1,1]", "[2,2]", "[3,3]", "[4,4]", "[5,5]", "[6,6]" }, "[[[[5,0],[7,4]],[5,5]],[6,6]]");
	}
}
