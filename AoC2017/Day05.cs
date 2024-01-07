namespace AoC2017;

public class Day05 : Solution
{
	private readonly List<int> offsets;

	public Day05(string file) : base(file) => offsets = Input.Select(int.Parse).ToList( );

	public override async Task<string> SolvePart1() => JumpAround( );

	public override async Task<string> SolvePart2() => JumpAround(isPart2: true);

	private string JumpAround(bool isPart2 = false)
	{
		var idx = 0;
		var steps = 0;
		while (idx < offsets.Count)
		{
			var jump = offsets[idx];
			offsets[idx] += isPart2 && jump >= 3 ? -1 : 1;
			idx += jump;
			steps++;
		}

		return steps.ToString( );
	}
}
