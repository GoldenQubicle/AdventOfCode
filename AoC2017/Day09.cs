namespace AoC2017;

public class Day09 : Solution
{
	public Day09(string file) : base(file) { }

	public Day09(List<string> input) : base(input) { }

	public override async Task<string> SolvePart1() => ProcessStream( ).sum;

	public override async Task<string> SolvePart2() => ProcessStream( ).count;


	private (string sum, string count) ProcessStream()
	{
		var idx = -1;
		var stream = Input.First( );
		var nesting = 0L;
		var sum = 0L;
		var inGarbage = false;
		var garbageCount = 0;

		while (idx++ < stream.Length - 1)
		{
			switch (stream[idx])
			{
				case '!':
					idx++;
					break;
				case '>':
					inGarbage = false;
					break;
				case var _ when inGarbage:
					garbageCount++;
					break;
				case '<':
					inGarbage = true;
					break;
				case '{' when !inGarbage:
					nesting++;
					sum += nesting;
					break;
				case '}' when !inGarbage:
					nesting--;
					break;
			}
		}
		return (sum.ToString( ), garbageCount.ToString( ));
	}
}
