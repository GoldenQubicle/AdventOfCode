namespace AoC2019;

public class Day09 : Solution
{
    public Day09(string file) : base(file) { }
    

    public override async Task<string> SolvePart1( )
    {
	    var icc = new IntCodeComputer(Input) { Inputs = new (){ 1 } };
	    icc.Execute();
	    return icc.Output.ToString();
    }

    public override async Task<string> SolvePart2( )
    {
		var icc = new IntCodeComputer(Input) { Inputs = new( ) { 2 } };
		icc.Execute( );
		return icc.Output.ToString( );
	}
}
