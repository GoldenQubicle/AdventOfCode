using Combinatorics.Collections;

namespace AoC2019;

public class Day07 : Solution
{
	public Day07(string file) : base(file) { }

	private List<int> GetMemory() =>
		Input[0].Split(",").Select(int.Parse).ToList( );

	public override async Task<string> SolvePart1()
	{
		var signals = new List<int>();
		var phases = new Permutations<int>(new List<int> { 0, 1, 2, 3, 4 });

		Parallel.ForEach(phases, phase => 
			signals.Add(phase.Aggregate(0, (current, p) => RunIntCode(p, current))));

		return signals.Max().ToString();
	}

	private int RunIntCode(int phase, int input)
	{
		var icc = new IntCodeComputer(GetMemory())
		{
			Input = new() { phase, input }
		};
	
		icc.Execute();
		return icc.Output;
	}


	public override async Task<string> SolvePart2()
	{
		var phases = new Permutations<int>(new List<int> { 5, 6, 7, 8, 9 });
		return string.Empty;
	}
}
