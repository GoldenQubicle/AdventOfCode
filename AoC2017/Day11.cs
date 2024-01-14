namespace AoC2017;

public class Day11 : Solution
{

	Dictionary<string, (int x, int y, int z)> directions = new( )
	{
		{ "e", (1, -1, 0) },
		{ "se", (0, -1, 1) },
		{ "sw", (-1, 0, 1) },
		{ "w", (-1, 1, 0) },
		{ "nw", (0, 1, -1) },
		{ "ne", (1, 0, -1) },
	};

	public Day11(string file) : base(file) { }
    
    public Day11(List<string> input) : base(input) { }



    public override async Task<string> SolvePart1( ) 
    {
    	return string.Empty;
    }

    public override async Task<string> SolvePart2( )
    {
    	return string.Empty;
    }
}
