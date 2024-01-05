using Common;

namespace AoC2023;

public class Day09 : Solution
{

	private readonly List<List<long>> reports;

	public Day09(string file) : base(file) => reports = Input
		.Select(l => l.Split(' ', StringSplitOptions.TrimEntries).Select(long.Parse).ToList( )).ToList( );

	public override async Task<string> SolvePart1() => GetInterpolatedSum(isPart2: false);

	public override async Task<string> SolvePart2() => GetInterpolatedSum(isPart2: true);
	
	private string GetInterpolatedSum(bool isPart2)
	{
		var result = new List<long>( );
		foreach (var report in reports)
		{
			if (isPart2)
				report.Reverse();

			var last = new Stack<long>( );
			last.Push(report.Last( ));
			var delta = GetDifferences(report);

			while (delta.Any(d => d != 0))
			{
				last.Push(delta.Last( ));
				delta = GetDifferences(delta);
			}

			var interpolated = last.Pop() + last.Pop();
			while (last.Count != 0)
			{
				interpolated += last.Pop();
			}
			result.Add(interpolated);
		}

		return result.Sum( ).ToString( );
	}

	private List<long> GetDifferences(List<long> input)
	{
		var result = new List<long>( );
		for (var i = 0 ;i < input.Count - 1 ;i++)
		{
			result.Add(input[i + 1] - input[i]);
		}

		return result;
	}
}
