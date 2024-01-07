namespace AoC2017Tests;

public class Day04Test
{
	[TestCase("aa bb cc dd ee", true)]
	[TestCase("aa bb cc dd aa", false)]
	[TestCase("aa bb cc dd aaa", true)]
	public void ValidPassPhrase(string phrase, bool expected)
	{
		var actual = Day04.ValidatePassPhrase(phrase);
		Assert.That(actual, Is.EqualTo(expected));
	}

	[TestCase("abcde fghij", true)]
	[TestCase("abcde xyz ecdab", false)]
	[TestCase("a ab abc abd abf abj", true)]
	[TestCase("iiii oiii ooii oooi oooo", true)]
	[TestCase("oiii ioii iioi iiio", false)]
	public void ValidPassPhrase2(string phrase, bool expected)
	{
		var actual = Day04.ValidatePassPhrase2(phrase);
		Assert.That(actual, Is.EqualTo(expected));
	}

}
