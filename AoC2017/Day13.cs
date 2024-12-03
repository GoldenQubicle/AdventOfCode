namespace AoC2017;

public class Day13 : Solution
{
	private readonly Dictionary<int, Layer> layers;
	private int packet = -1;

	public Day13(string file) : base(file) => layers = Input
		.Select(line =>
			{
				var parts = line.Split(":", StringSplitOptions.TrimEntries);
				return new Layer(int.Parse(parts[0]), int.Parse(parts[1]));
			})
		.ToDictionary(l => l.Depth, l => l);




	public override async Task<string> SolvePart1()
	{
		var maxDepth = layers.Keys.Max();
		
		for (var t = 0; t < maxDepth; t++)
		{
			packet += 1;
			var caught = layers[packet].Current == 0;


		}

		return string.Empty;
	}

	public override async Task<string> SolvePart2()
	{
		return string.Empty;
	}

	internal class Layer(int depth, int range)
	{
		public int Depth { get; } = depth;

		public int Current { get; private set; }
		private int tick = 1;

		public void Tick()
		{
			Current += tick;

			if (Current == range - 1 || Current == 0)
				tick *= -1;

		}
	}
}
