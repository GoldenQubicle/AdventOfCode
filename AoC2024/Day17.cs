namespace AoC2024;

public class Day17 : Solution
{
	public readonly Dictionary<char, long> Registers = new( );
	private List<long> program;

	private const char A = 'A';
	private const char B = 'B';
	private const char C = 'C';

	public Day17(string file) : base(file) => SetRegisters( );

	public Day17(List<string> input) : base(input) => SetRegisters( );

	public override async Task<string> SolvePart1() => string.Join(",", RunProgram( ));

	public override async Task<string> SolvePart2()
	{
		var idx = 0;
		var aValue = 0L;
		var inc = 1L;
	
		while (true) 
		{
			SetRegisters( );
			Registers[A] = aValue;
			var output = RunProgram( );
			
			if (OutputMatches(output))
			{
				idx++;

				if (output.Count < program.Count - 4) // clamp the increment to prevent overshoot for solution
					inc = (long)Math.Pow(8, idx);

				if (OutputMatches(output) && output.Count == program.Count)
					return aValue.ToString( );
			}
			aValue += inc;
		}
		bool OutputMatches(List<long> output) => output.Take(idx + 1).SequenceEqual(program.Take(idx + 1));
	}


	private List<long> RunProgram()
	{
		var pointer = 0L;
		var output = new List<long>( );
		var isDone = false;
		while (pointer < program.Count && !isDone)
		{
			var (opcode, operand) = Read(pointer);

			switch (opcode)
			{
				case OpCode.Adv:
					Registers[A] = (long)Math.Truncate(Registers[A] / Math.Pow(2, operand));
					pointer += 2;
					break;
				case OpCode.Bxl:
					Registers[B] ^= operand;
					pointer += 2;
					break;
				case OpCode.Bst:
					Registers[B] = operand % 8;
					pointer += 2;
					break;
				case OpCode.Jnz:
					if (Registers[A] > 0)
						pointer = operand;
					else
						isDone = true;
					break;
				case OpCode.Bcx:
					Registers[B] ^= Registers[C];
					pointer += 2;
					break;
				case OpCode.Out:
					output.Add(operand % 8);
					pointer += 2;
					break;
				case OpCode.Bdv:
					Registers[B] = (long)Math.Truncate(Registers[A] / Math.Pow(2, operand));
					pointer += 2;
					break;
				case OpCode.Cdv:
					Registers[C] = (long)Math.Truncate(Registers[A] / Math.Pow(2, operand));
					pointer += 2;
					break;
				default:
					throw new ArgumentOutOfRangeException( );
			}
		}
		return output;
	}

	private void SetRegisters()
	{
		Registers.Clear( );
		Registers.Add(A, Input[0].ToInt( ));
		Registers.Add(B, Input[1].ToInt( ));
		Registers.Add(C, Input[2].ToInt( ));
		program = Input[3].Split(':')[1].Split(',', StringSplitOptions.TrimEntries).Select(long.Parse).ToList( );
	}


	private (OpCode opcode, long operand) Read(long pointer)
	{
		var opcode = (OpCode)program[(int)pointer];
		var operand = GetOperand(opcode, pointer);
		return (opcode, operand);
	}

	private long GetOperand(OpCode opcode, long pointer)
	{
		var isLiteral = opcode is OpCode.Bxl or OpCode.Jnz;
		var operand = program[(int)pointer + 1];
		return isLiteral ? operand : GetCombo(operand);
	}

	private long GetCombo(long operand) => operand switch
	{
		<= 3 => operand,
		4 => Registers[A],
		5 => Registers[B],
		6 => Registers[C],
		_ => throw new ArgumentOutOfRangeException( )
	};

	private enum OpCode
	{
		Adv, Bxl, Bst, Jnz, Bcx, Out, Bdv, Cdv
	}
}
