namespace AoC2024;

public class Day24 : Solution
{
	private readonly Dictionary<string, int> wires;
	private readonly List<Gate> gates;

	public Day24(string file) : base(file, doRemoveEmptyLines: false)
	{
		wires = Input
			.TakeWhile(line => !string.IsNullOrEmpty(line))
			.Select(line =>
			{
				var parts = line.Split(":", StringSplitOptions.TrimEntries);
				return (parts[0], int.Parse(parts[1]));
			}).ToDictionary(t => t.Item1, t => t.Item2);

		gates = Input
			.SkipWhile(line => line.Length == 6 || string.IsNullOrEmpty(line))
			.Select(line =>
			{
				var parts = line.Split(" ", StringSplitOptions.TrimEntries);
				wires.TryAdd(parts[0], -1);
				wires.TryAdd(parts[2], -1);
				wires.TryAdd(parts[4], -1);
				return new Gate(parts[1], parts[0], parts[2], parts[4]);
			}).ToList( );
	}


	public override async Task<string> SolvePart1()
	{
		while (!AllOutput())
			gates.Where(HasInput).ForEach(SetValue);
		

		var binary = string.Empty;
		wires.Where(g => g.Key.StartsWith("z")).OrderByDescending(k => k.Key ).ForEach(k =>
		{
			Console.WriteLine($"{k.Key}: {k.Value}");
			binary += $"{k.Value}";
		});

		var number = Convert.ToInt64(binary, 2).ToString();

		return number;
	}

	public override async Task<string> SolvePart2()
	{
		return string.Empty;
	}

	private bool HasInput(Gate gate) => wires[gate.In1] != -1 && wires[gate.In2] != -1;

	private bool AllOutput() => !wires.Where(w => w.Key.StartsWith("z")).Any(w => w.Value == -1);

	private void SetValue(Gate gate)
	{
		switch (gate.Op)
		{
			case "AND":
				wires[gate.Out] = WiresHaveSameValue(gate, 1) ? 1 : 0;
				break;
			case "OR":
				wires[gate.Out] = WiresHaveSameValue(gate, 0) ? 0 : 1;
				break;
			case "XOR":
				wires[gate.Out] = WiresHaveSameValue(gate, 0) || WiresHaveSameValue(gate, 1) ? 0 : 1;
				break;
		}
	}

	private bool WiresHaveSameValue(Gate gate, int value) =>
		wires[gate.In1] == value && wires[gate.In2] == value;


	public record Gate(string Op, string In1, string In2, string Out);

}
