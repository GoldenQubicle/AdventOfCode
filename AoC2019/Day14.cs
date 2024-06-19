namespace AoC2019;

public class Day14 : Solution
{
	private Dictionary<string, int> Outputs = new( );
	private Dictionary<string, Dictionary<string, int>> Inputs = new( );
	private const string Ore = "ORE";
	private const string Fuel = "FUEL";

	public Day14(string file) : base(file, split: "\n") => Input.ForEach(l =>
	{
		var parts = l.Split(" => ");
		var output = Parse(parts[1]);
		Outputs.Add(output.name, output.amount);
		Inputs.Add(output.name, parts[0].Split(",").Select(Parse).ToDictionary(t => t.name, t => t.amount));
	});


	public override async Task<string> SolvePart1() => ProduceFuel(1).ToString( );

	public override async Task<string> SolvePart2()
	{
		var totalOre = 1000000000000d;
		var orePerFuel = ProduceFuel(1);

		//floor this time
		var fuel = (long)Math.Floor(totalOre / orePerFuel);
		var ore = ProduceFuel(fuel);
		var ratio = totalOre / ore;

		fuel = (long)Math.Floor(fuel * ratio); 
		ore = ProduceFuel(fuel);

		ratio = totalOre / ore;
		fuel = (long)Math.Floor(fuel * ratio); 

		return fuel.ToString( );
	}

	private long ProduceFuel(long amount)
	{
		var required = new Dictionary<string, long> { { Fuel, amount } };
		var ore = 0L;

		while (required.Values.Any(v => v > 0))
		{
			var current = required.First(kvp => kvp.Value > 0);
			
			var n =  (long)Math.Ceiling(current.Value / (float)Outputs[current.Key]);
			required[current.Key] -= n * Outputs[current.Key];
			
			Inputs[current.Key].ForEach(c =>
			{
				var needed = n * c.Value;
				if (c.Key.Equals(Ore))
				{
					ore += needed;
					return;
				}

				if (!required.TryAdd(c.Key, needed))
				{

					required[c.Key] += needed;
				}
			});
		}

		return ore;
	}


	private static (string name, int amount) Parse(string input)
	{
		var parts = input.Trim( ).Split(" ");
		return (parts[1], int.Parse(parts[0]));
	}
}
