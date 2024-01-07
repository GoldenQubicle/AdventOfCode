namespace AoC2017;

public class Day04 : Solution
{
	public Day04(string file) : base(file) { }

	public Day04(List<string> input) : base(input) { }

	public override async Task<string> SolvePart1() => Input.Count(ValidatePassPhrase).ToString( );

	public override async Task<string> SolvePart2() => Input.Count(ValidatePassPhrase2).ToString( );

	public static bool ValidatePassPhrase(string phrase) => 
		!phrase.Split(" ").GroupBy(s => s).Any(g => g.Count( ) > 1);

	public static bool ValidatePassPhrase2(string phrase) => 
		!phrase.Split(" ").Select(g => g.Order( ).AsString( )).GroupBy(o => o).Any(g => g.Count( ) > 1);

}
