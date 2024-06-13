namespace AoC2019;

public class Day13 : Solution
{
    public Day13(string file) : base(file, "\n") { }
    
    public override async Task<string> SolvePart1( )
    {
	    var grid = new Grid2d();
	    var icc = new IntCodeComputer(Input)
	    {
            BreakOnOutput = true
	    };

	    var instructions = new List<int>();

	    while (icc.Execute())
	    {
            instructions.Add((int)icc.Output);
            if(instructions.Count < 3) continue;

            grid.Add(new ((instructions[0], instructions[1]), instructions[2] switch
            {
                0 => '.',
                1 => 'W',
                2 => '#',
                3 => '_',
                4 => 'o',
            }));
            instructions.Clear();
	    }

    	return grid.Count(c => c.Character == '#').ToString();
    }

    public override async Task<string> SolvePart2( )
    {
    	return string.Empty;
    }
}
