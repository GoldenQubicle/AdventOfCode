namespace AoC2024;

public class Day13 : Solution
{
	private readonly List<ClawMachine> machines;

	private const double Part2 = 10000000000000;

	public Day13(string file) : base(file) => machines = Input.Chunk(3)
		.Select(c => new ClawMachine(GetXY(c[0]), GetXY(c[1]), GetXY(c[2]))).ToList( );

	public override async Task<string> SolvePart1() => machines
		.Sum(GetMinimalCost).ToString( );

	public override async Task<string> SolvePart2() => machines
		.Select(m => m with { Prize = m.Prize.Add((Part2, Part2)) })
		.Sum(GetMinimalCost).ToString( );


	private double GetMinimalCost(ClawMachine machine)
	{
		const double sigma = 0.0001;

		var variables = Matrix<double>.Build.DenseOfArray(new[,] {
			{ machine.A.x, machine.B.x },
			{ machine.A.y, machine.B.y }
		});
		var answer = Vector<double>.Build.Dense([machine.Prize.x, machine.Prize.y]);
		var result = variables.Solve(answer).AsArray( );

		var x = Math.Round(result[0]);
		var y = Math.Round(result[1]);

		var deltaX = Math.Abs(result[0] - x);
		var deltaY = Math.Abs(result[1] - y);

		if (deltaX < sigma && deltaY < sigma)
			return x * 3 + y;

		return 0;
	}

	private record ClawMachine((double x, double y) A, (double x, double y) B, (double x, double y) Prize);

	private static (double x, double y) GetXY(string s)
	{
		var m = Regex.Matches(s, @"\d+");
		return (m.AsInt(0), m.AsInt(1));
	}
}
