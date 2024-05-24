namespace AoC2019;

public class Day05 : Solution
{
	private readonly IEnumerable<int> memory;

	public Day05(string file) : base(file) =>
		memory = Input[0].Split(",").Select(int.Parse);

    public override async Task<string> SolvePart1( )
    {
	    var icc = new IntCodeComputer(memory) { Input = 1 };
        icc.Execute();
    	return icc.Output.ToString();
    }

    public override async Task<string> SolvePart2( )
    {
		var icc = new IntCodeComputer(memory) { Input = 5 };
		icc.Execute( );
		return icc.Output.ToString( );
	}
}
