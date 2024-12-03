using static AoC2017.Day13;

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
		var maxDepth = layers.Keys.Max( );
		var severity = new List<int>( );
		
		for (var t = 0 ;t <= maxDepth ;t++)
		{
			packet += 1;
			var caught = layers.TryGetValue(packet, out var layer) && layer.Current == 0;
			if (caught)
				severity.Add(layers[packet].Depth * layers[packet].Range);
			layers.Values.ForEach(l => l.Tick( ));
		}

		return severity.Sum( ).ToString( );
	}

	public override async Task<string> SolvePart2()
	{
		var acceptedDelays = new Dictionary<int, int>( );
		var delay = 0;

		while (delay++ < 5_000_000)
		{
			acceptedDelays.Add(delay, 0);
			layers.Values.ForEach(l =>
				acceptedDelays[delay] += (delay + l.Depth) % l.Cycle != 0 ? 1 : 0);
		}

		return acceptedDelays.First(kvp => kvp.Value == layers.Count).Key.ToString( );
	}

	internal class Layer(int depth, int range)
	{
		public int Depth { get; } = depth;
		public int Range { get; } = range;

		public int Current { get; private set; }
		public int Cycle => (Range * 2) - 2;

		private int tick = 1;

		public void Tick()
		{
			Current += tick;

			if (Current == Range - 1 || Current == 0)
				tick *= -1;

		}
	}
}
