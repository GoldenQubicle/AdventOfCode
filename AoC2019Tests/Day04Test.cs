namespace AoC2019Tests;
            
public class Day04Test
{
    [TestCase("111123", true)]
    [TestCase("135679", true)]
    [TestCase("223450", false)]
    public async Task PassWordNeverDecreasesTests(string pw, bool expected)
    {
	    var actual = Day04.PassWordDoesNotDecrease(pw);
        Assert.That(actual, Is.EqualTo(expected));
    }


    [TestCase("111123", true)]
    [TestCase("122345", true)]
    [TestCase("135679", false)]
    [TestCase("223450", true)]
    public async Task PassWordHasDoubleDigitTests(string pw, bool expected)
    {
	    var actual = Day04.PassWordHasDoubleDigits(pw);
	    Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("111123", true)]
    [TestCase("135679", false)]
    [TestCase("223450", false)]
    public async Task PassWordIsValidTests(string pw, bool expected)
    {
	    var actual = Day04.PassWordIsValid(pw);
	    Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("112233", true)] 
    [TestCase("123444", false)] 
    [TestCase("111122", true)]
    public async Task PassWordHasExactlyDoubleDigit(string pw, bool expected)
    {
	    var actual = Day04.PassWordHasExactlyDoubleDigits(pw);
	    Assert.That(actual, Is.EqualTo(expected));
    }
}
