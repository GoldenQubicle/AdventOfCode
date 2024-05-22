namespace AoC2019;

public class Day05 : Solution
{
	private readonly IEnumerable<int> memory;
	public Day05(string file) : base(file) =>
		memory = Input[0].Split(",").Select(int.Parse);

    public override async Task<string> SolvePart1( ) 
    {
        var pc = new IntCodeComputer(Input)
    	return string.Empty;
    }

    public override async Task<string> SolvePart2( )
    {
    	return string.Empty;
    }
}
