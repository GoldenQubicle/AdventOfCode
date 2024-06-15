namespace AoC2019;

public class Day14 : Solution
{
	private Dictionary<string, float> Outputs = new( );
	private Dictionary<string, Dictionary<string, float>> Inputs = new( );
	private const string Ore = "ORE";
	private const string Fuel = "FUEL";

	public Day14(string file) : base(file, split: "\n")
	{

		Input.ForEach(l =>
		{
			var parts = l.Split(" => ");
			var output = Parse(parts[1]);
			Outputs.Add(output.name, output.amount);
			Inputs.Add(output.name, parts[0].Split(",").Select(Parse).ToDictionary(t => t.name, t => t.amount));
		});
	}


	public override async Task<string> SolvePart1()
	{
		var ore = Inputs.Where(kvp => kvp.Value.ContainsKey(Ore)).ToDictionary(kvp => kvp.Key, _ => 0f);
		var required = new Stack<(string name, float quanity)> { (Fuel, 1) };
		
		
		while (required.TryPop(out var current))
		{
			if (ore.ContainsKey(current.name))
				ore[current.name] += current.quanity;
			else
			{
				//so the problem here is this; there can already be an item on the stack with the same name which causes rounding up too much..
				//basically we need to aggregate over all the required outputs, and then resolve...
				required.PushAll(Inputs[current.name]
					.Select(i => (i.Key, i.Value * (float)Math.Ceiling(current.quanity / Outputs[current.name]))));

			}
			
		}


		return ore.Sum(o => Math.Ceiling(o.Value / Outputs[o.Key]) * Inputs[o.Key][Ore]).ToString();
	}

	public override async Task<string> SolvePart2()
	{
		return string.Empty;
	}

	private static (string name, float amount) Parse(string input)
	{
		var parts = input.Trim( ).Split(" ");
		return (parts[1], int.Parse(parts[0]));
	}
}
