namespace AoC2024;

public class Day07 : Solution
{
	private readonly List<(long result, List<long>)> equations;

	public Day07(string file) : base(file) => equations = Input
		.Select(line =>
		{
			var parts = line.Split(":", StringSplitOptions.TrimEntries);
			return (long.Parse(parts[0]), parts[1]
				.Split(" ", StringSplitOptions.TrimEntries)
				.Select(long.Parse).ToList( ));
		}).ToList( );


	public override async Task<string> SolvePart1() => equations
		.Where(e => IsValid(e, isPart2: false))
		.Sum(e => e.result).ToString( );


	public override async Task<string> SolvePart2() => equations
		.Where(e => IsValid(e, isPart2: true))
		.Sum(e => e.result).ToString( );


	private bool IsValid((long result, List<long> input) input, bool isPart2 = false)
	{
		var queue = new Queue<(long sum, List<long> equation)> { (0L, input.input) };

		while (queue.TryDequeue(out var current))
		{
			var (sum, equation) = current;

			if (equation.Count == 0)
				return false;

			var add = sum + equation[0];
			var mult = sum * equation[0];
			var concat = long.Parse($"{sum}{equation[0]}");

			if ((add == input.result ||
				 mult == input.result ||
				 (concat == input.result && isPart2))
				&& equation.Count == 1) // make sure all numbers are consumed. Not explicitly stated however we'll return true prematurely otherwise.

				return true;

			queue.Enqueue((add, equation[1..]));
			queue.Enqueue((mult, equation[1..]));
			
			if (isPart2)
				queue.Enqueue((concat, equation[1..]));
		}

		return false;
	}
}
