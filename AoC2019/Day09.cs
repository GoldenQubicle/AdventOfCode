namespace AoC2019;

public class Day09 : Solution
{
    public Day09(string file) : base(file) { }

    public override async Task<string> SolvePart1() => RunIntCode(1);

    public override async Task<string> SolvePart2( ) => RunIntCode(2);

	private string RunIntCode(int i)
    {
	    var icc = new IntCodeComputer(Input) { Inputs = new() { i } };
	    icc.Execute();
	    return icc.Output.Last( ).ToString();
    }
}
