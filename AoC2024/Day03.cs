namespace AoC2024;

public partial class Day03(string file) : Solution(file)
{
	public override async Task<string> SolvePart1() => Input
		.SelectMany(GetMatches)
		.Sum(DoMultiply).ToString( );


	public override async Task<string> SolvePart2() => Input
		.Aggregate(new StringBuilder( ), (builder, line) => builder.AppendLine(line)).ToString( )
		.Split("do()") 
		.Select(part => part.Split("don't()")[0]) 
		.SelectMany(GetMatches)
		.Sum(DoMultiply).ToString( );


	[GeneratedRegex(@"(?<mul>mul\(\d{1,3},\d{1,3}\))", RegexOptions.Compiled)]
	private static partial Regex GeneratedRegex();

	private static MatchCollection GetMatches(string input) => 
		GeneratedRegex( ).Matches(input);

	private static long DoMultiply(Match m) => 
		Multiply(m.Groups["mul"].Value.Split(","));

	private static long Multiply(string[] parts) => 
		parts[0].ToLong() * parts[1].ToLong();



}
