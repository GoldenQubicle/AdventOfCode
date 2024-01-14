namespace AoC2018;

public class Day16 : Solution
{
	private static int[ ] Registers = { 0, 0, 0, 0, };

	internal Dictionary<string, Action<int, int, int>> Ops = new( )
	{
		{ "addr", (a, b, c) => Registers[c] = Registers[a] + Registers[b] },
		{ "addi", (a, b, c) => Registers[c] = Registers[a] + b },
		{ "mulr", (a, b, c) => Registers[c] = Registers[a] * Registers[b] },
		{ "muli", (a, b, c) => Registers[c] = Registers[a] * b },
		{ "banr", (a, b, c) => Registers[c] = Registers[a] & Registers[b] },
		{ "bani", (a, b, c) => Registers[c] = Registers[a] & b },
		{ "borr", (a, b, c) => Registers[c] = Registers[a] | Registers[b] },
		{ "bori", (a, b, c) => Registers[c] = Registers[a] | b },
		{ "setr", (a, b, c) => Registers[c] = Registers[a] },
		{ "seti", (a, b, c) => Registers[c] = a },
		{ "gtir", (a, b, c) => Registers[c] = a > Registers[b] ? 1 : 0 },
		{ "gtri", (a, b, c) => Registers[c] = Registers[a] > b ? 1 : 0 },
		{ "gtrr", (a, b, c) => Registers[c] = Registers[a] > Registers[b] ? 1 : 0 },
		{ "eqir", (a, b, c) => Registers[c] = a == Registers[b] ? 1 : 0 },
		{ "eqri", (a, b, c) => Registers[c] = Registers[a] == b ? 1 : 0 },
		{ "eqrr", (a, b, c) => Registers[c] = Registers[a] == Registers[b] ? 1 : 0}
	};

	private readonly List<Sample> samples;
	private readonly Dictionary<int, HashSet<string>> opsMapping = new( );
	private readonly List<Instruction> program;

	internal record Instruction(int[ ] Data)
	{
		public int Op = Data[0];
		public int A = Data[1];
		public int B = Data[2];
		public int C = Data[3];
	};

	internal record Sample(int[ ] Before, Instruction Instruction, int[ ] After);

	public Day16(string file) : base(file, split: "\n")
	{
		samples = Input
			.Chunk(3)
			.TakeWhile(sample => sample[0].StartsWith("Before"))
			.Select(sample => new Sample(
				sample[0].Split(':', StringSplitOptions.TrimEntries)[1].Where(char.IsDigit).Select(c => c.ToInt( )).ToArray( ),
				new Instruction(sample[1].Split(' ', StringSplitOptions.TrimEntries).Select(int.Parse).ToArray( )),
				sample[2].Split(':', StringSplitOptions.TrimEntries)[1].Where(char.IsDigit).Select(c => c.ToInt( )).ToArray( )
				)).ToList( );

		program = Input
			.Skip(3 * samples.Count)
			.Select(l => new Instruction(l.Split(' ', StringSplitOptions.TrimEntries).Select(int.Parse).ToArray( )))
			.ToList( );
	}

	public override async Task<string> SolvePart1()
	{
		var sampleCount = 0;
		foreach (var sample in samples)
		{
			var opCount = 0;
			foreach (var op in Ops)
			{
				sample.Before.CopyTo(Registers, 0);

				op.Value(sample.Instruction.A, sample.Instruction.B, sample.Instruction.C);

				if (Registers.WithIndex( ).All(e => sample.After[e.idx] == e.Value))
				{
					if (!opsMapping.ContainsKey(sample.Instruction.Op))
						opsMapping.Add(sample.Instruction.Op, new HashSet<string> { op.Key });
					else
						opsMapping[sample.Instruction.Op].Add(op.Key);

					opCount++;
				}
			}

			if (opCount >= 3)
				sampleCount++;
		}

		return sampleCount.ToString( );
	}

	public override async Task<string> SolvePart2()
	{
		await SolvePart1( );

		var ops = new Dictionary<int, string>( );

		while (opsMapping.Count > 0)
		{
			opsMapping
				.Where(m => m.Value.Count == 1)
				.ForEach(k =>
				{
					ops.TryAdd(k.Key, k.Value.First( ));
					opsMapping.Remove(k.Key);
					opsMapping.Values.ForEach(s => s.Remove(k.Value.First( )));
				});

		}

		Registers = new int[ ] { 0, 0, 0, 0 };

		program.ForEach(p =>
		{
			Ops[ops[p.Op]](p.A, p.B, p.C);
		});

		return Registers[0].ToString( );
	}
}
