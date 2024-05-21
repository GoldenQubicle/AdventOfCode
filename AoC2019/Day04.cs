namespace AoC2019;

public class Day04 : Solution
{
	private readonly (int start, int end) range;

	public Day04(string file) : base(file) => range = 
		(start: int.Parse(Input[0].Split('-')[0]), end: int.Parse(Input[0].Split('-')[1]));


	public override async Task<string> SolvePart1( ) =>
		Enumerable.Range(range.start, range.end - range.start)
			.Count(n => PassWordIsValid(n.ToString( ))).ToString( );


	public override async Task<string> SolvePart2() =>
		Enumerable.Range(range.start, range.end - range.start)
			.Count(n => PassWordIsValid(n.ToString( ), isPart2: true)).ToString( );

	public static bool PassWordDoesNotDecrease(string pw) =>
	    pw.WithIndex( ).Skip(1).All(c => pw[c.idx - 1] <= c.Value);

    public static bool PassWordHasDoubleDigits(string pw) =>
		pw.WithIndex().Skip(1).Any(c => pw[c.idx - 1] == c.Value);

    public static bool PassWordHasExactlyDoubleDigits(string pw) =>
	    PassWordHasDoubleDigits(pw) && pw.GroupBy(c => c).Any(g => g.Count() == 2);
	    

    public static bool PassWordIsValid(string pw, bool isPart2 = false) =>
	    isPart2
			? PassWordDoesNotDecrease(pw) && PassWordHasExactlyDoubleDigits(pw)
		    : PassWordDoesNotDecrease(pw) && PassWordHasDoubleDigits(pw);

}
