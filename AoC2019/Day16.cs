namespace AoC2019;

public class Day16 : Solution
{
	private string signal;
	public int Phases { get; set; } = 100;
	public Day16(string file) : base(file) => signal = Input.First( );

	public Day16(List<string> input) : base(input) => signal = input.First( );

	public override async Task<string> SolvePart1() => RunFlawedFrequencyTransmission( )[..8];

	public override async Task<string> SolvePart2()
	{
		var offset = int.Parse(signal[..7]);
		signal = Enumerable.Range(0, 10000)
			.Aggregate(new StringBuilder(), (builder, _) => builder.Append(signal)).ToString();

		var output = RunFlawedFrequencyTransmission();
		var r = output[offset..(offset + 8)];

		return r;
	}

	private string RunFlawedFrequencyTransmission()
	{
		var repeats = Enumerable.Range(1, signal.Length)
			.Select(n => GetRanges(n, signal.Length)).ToList();
		
		for (var p = 0; p < Phases; p++)
		{
			var output = new StringBuilder();
			foreach (var repeat in repeats)
			{
				var sum = repeat.Sum(r =>
				{
					var s = signal[r.Start..(r.End >= signal.Length ? signal.Length : r.End)]
						.Aggregate(0, (sum, c) => sum + c.ToInt( ));
						
					return r.Sign == Sign.Minus ? -1 * s : s;
				});
				output.Append(Math.Abs(sum) % 10);
			}

			signal = output.ToString();
			Console.WriteLine(signal);
		}

		return signal;
	}



	public enum Sign { Plus, Minus }

	public record Range(Sign Sign, int Start, int End);

	//the idea here is to precompute all the positive and negative ranges
	//handling the situations wherein the patternLength < signalLength
	public static List<Range> GetRanges(int idx, double signalLength)
	{
		var patternLength = 4 * idx;
		var rangeSize = patternLength / 4;
		var patternRepeat = Math.Ceiling(signalLength / patternLength);
		var ranges = new List<Range>( );
		//0..rangeSize, rangeSize..rangeSize*2,rangeSize*2..rangeSize*3, rangeSize*3..rangeSize*4

		for (var i = 0 ;i < patternRepeat ;i++)
		{
			var offset = (i * patternLength) - 1;
			
			ranges.Add(new(Sign.Plus, rangeSize + offset, rangeSize * 2 + offset));
			ranges.Add(new(Sign.Minus, rangeSize  * 3 + offset, rangeSize  * 4 + offset));

		}

		return ranges.Where(r => r.Start < signalLength).ToList();
	}

}
