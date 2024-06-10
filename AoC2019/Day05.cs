namespace AoC2019;

public class Day05 : Solution
{
	public Day05(string file) : base(file) { }
		

    public override async Task<string> SolvePart1( )
    {
	    var icc = new IntCodeComputer(Input) { Inputs = new(){ 1 } };
        icc.Execute();
    	return icc.Output.Last().ToString();
    }

    public override async Task<string> SolvePart2( )
    {
		var icc = new IntCodeComputer(Input) { Inputs = new() { 5 } };
		icc.Execute( );
		return icc.Output.Last( ).ToString( );
	}
}
