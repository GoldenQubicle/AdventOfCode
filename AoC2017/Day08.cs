namespace AoC2017;

public class Day08 : Solution
{
	private readonly Dictionary<string, int> registers = new( );
	private readonly IEnumerable<Instruction> instructions;

	public Day08(string file) : base(file) => instructions = Input.Select(l =>
	{
		var parts = l.Split(' ').Select(p => p.Trim( )).ToList( );
		var modify = parts[0];
		var increment = parts[1].Equals("inc");
		var amount = int.Parse(parts[2]);
		var check = parts[4];
		var condition = parts[5];
		var value = int.Parse(parts[6]);
		registers.TryAdd(modify, 0);
		registers.TryAdd(check, 0);
		return new Instruction(
			() =>
			{
				if (increment)
					registers[modify] += amount;
				else
					registers[modify] -= amount;
			},
			() => condition switch
			{
				">" => registers[check] > value,
				"<" => registers[check] < value,
				">=" => registers[check] >= value,
				"<=" => registers[check] <= value,
				"==" => registers[check] == value,
				"!=" => registers[check] != value,
			});
	});



	public override async Task<string> SolvePart1()
	{
		foreach (var i in instructions)
		{
			if (i.Condition( ))
				i.Modification( );
		}

		return registers.Values.Max( ).ToString( );
	}

	public override async Task<string> SolvePart2()
	{
		var max = 0;
		foreach (var i in instructions)
		{
			if (i.Condition( ))
				i.Modification( );
			max = registers.Values.Max( ) > max ? registers.Values.Max( ) : max;
		}

		return max.ToString( );
	}

	public record Instruction(Action Modification, Func<bool> Condition);
}
