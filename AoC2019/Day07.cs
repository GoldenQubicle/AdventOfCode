using Combinatorics.Collections;

namespace AoC2019;

public class Day07 : Solution
{
	public Day07(string file) : base(file) { }

	public override async Task<string> SolvePart1()
	{
		var signals = new List<long>( );
		var phases = new Permutations<long>(new List<long> { 0, 1, 2, 3, 4 });

		Parallel.ForEach(phases, phase =>
			signals.Add(phase.Aggregate(0L, (current, p) => RunIntCode(p, current))));

		return signals.Max( ).ToString( );
	}

	private long RunIntCode(long phase, long input)
	{
		var icc = new IntCodeComputer(Input)
		{
			Inputs = new( ) { phase, input }
		};

		icc.Execute( );
		return icc.Output;
	}


	public override async Task<string> SolvePart2()
	{
		var phases = new Permutations<int>(new List<int> { 5, 6, 7, 8, 9 });
		var signals = new List<long>();
		
		Parallel.ForEach(phases, phase => signals.Add(RunAmplifiersForPhase(phase)));

		return signals.Max().ToString();
	}

	private long RunAmplifiersForPhase(IReadOnlyList<int> phase)
	{
		var amplifiers = phase.WithIndex()
			.Select(p => new IntCodeComputer(Input)
			{
				Id = p.idx,
				Inputs = p.idx == 0 ? new() {  p.Value, 0 } : new() { p.Value },
				BreakOnOutput = true,
			}).ToList();

		var current = amplifiers.First();
		
		while (current.Execute())
		{
			if (amplifiers.All(a => a.IsFinished))
				break;

			var output = current.Output;
			current = amplifiers[current.Id == 4 ? 0 : current.Id + 1];
			current.Inputs.Add(output);
		}

		return amplifiers.Last().Output;
	}
}
