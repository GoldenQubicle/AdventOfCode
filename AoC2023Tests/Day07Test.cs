using AoC2023;

namespace AoC2023Tests;

public class Day07Test
{
	Day07 day07;

	[SetUp]
	public void Setup()
	{
		day07 = new Day07("day07test1");
	}

	[Test]
	public void Part1()
	{
		var actual = day07.SolvePart1( );
		Assert.That(actual, Is.EqualTo("6440"));
	}

	[Test]
	public void Part2()
	{
		var actual = day07.SolvePart2( );
		Assert.That(actual, Is.EqualTo("5905"));
	}


	[TestCase("QJJQ2", 2)]
	[TestCase("T55J5", 2)]
	[TestCase("J0123", 6)]
	[TestCase("J0012", 4)]
	[TestCase("J0011", 3)]
	[TestCase("J0001", 2)]
	[TestCase("J0000", 1)]
	[TestCase("JJ123", 4)]
	[TestCase("JJ112", 2)]
	[TestCase("JJ111", 1)]
	[TestCase("JJJ00", 1)]
	[TestCase("JJJ12", 2)]
	[TestCase("JJJJ0", 1)]
	[TestCase("JJJJJ", 1)]
	public void CardType2ShouldBeCorrect(string hand, int expected)
	{
		var c = new Day07.Card(hand, 0);

		Assert.That(c.Type2, Is.EqualTo(expected));
	}


	[TestCase("AAAAA", 1)]
	[TestCase("AA8AA", 2)]
	[TestCase("23332", 3)]
	[TestCase("TTT98", 4)]
	[TestCase("23432", 5)]
	[TestCase("A23A4", 6)]
	[TestCase("23456", 7)]
	public void CardType1ShouldBeCorrect(string hand, int expected)
	{
		var c = new Day07.Card(hand, 0);

		Assert.That(c.Type1, Is.EqualTo(expected));
	}


	[TestCase("33332", "2AAAA", 1)]
	[TestCase("77888", "77788", 1)]
	[TestCase("KTJJT", "KTJJT", 0)]
	[TestCase("2AAAA", "33332", -1)]
	[TestCase("77788", "77888", -1)]
	[TestCase("AAAA3", "AAAA2", 1)]
	public void CardStrengthShouldBeComparedCorrect(string h1, string h2, int e)
	{
		var c1 = new Day07.Card(h1, 0);
		var c2 = new Day07.Card(h2, 0);
		Assert.That(c1.Type1 == c2.Type1);

		var c = new Day07.Comparer(Day07.LabelsPart1, true);

		Assert.That(c.Compare(c1, c2), Is.EqualTo(e));
	}
}