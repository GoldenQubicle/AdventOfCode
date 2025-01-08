namespace AoC2021;

public class Day24 : Solution
{
	private readonly List<Monad> monads;

	public Day24(string file) : base(file) => monads = Input
		.Chunk(18)
		.Select(m => new Monad(
			long.Parse(m[5].Split(' ')[2]),
			long.Parse(m[15].Split(' ')[2]),
			int.Parse(m[4].Split(' ')[2]))).ToList();


	public override async Task<string> SolvePart1() => RunMonads(Enumerable.Range(1, 9).ToList());


	public override async Task<string> SolvePart2() => RunMonads(Enumerable.Range(1, 9).Reverse().ToList());


	private string RunMonads(List<int> digits)
	{
		var stack = new Stack<(int, long, string)>();
		stack.Push((0, 0, string.Empty));

		while (stack.TryPop(out var state))
		{
			var (depth, z, number) = state;

			if (z == 0 && depth == 14)
				return number;

			foreach (var w in digits)
			{
				var r = monads[depth].Run(z, w);
				if (r == long.MaxValue)
					continue;

				stack.Push((depth + 1, r, number + w));
			}
		}

		return string.Empty;
	}


	public record Monad(long X, long Y, int Type)
	{
		public long Run(long z, long w) => Type switch
		{
			1 => Y + w + 26 * z,
			26 when z % 26 + X == w => (long)MathF.Truncate(z / 26),
			_ => long.MaxValue
		};
	}
}