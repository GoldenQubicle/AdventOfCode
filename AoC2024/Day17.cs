namespace AoC2024;

public class Day17 : Solution
{
	private readonly Dictionary<char, int> registers = new( );
	private readonly List<int> program;

	private const char A = 'A';
	private const char B = 'B';
	private const char C = 'C';

	public Day17(string file) : base(file)
	{
		registers.Add(A, Input[0].ToInt( ));
		registers.Add(B, Input[1].ToInt( ));
		registers.Add(C, Input[2].ToInt( ));

		program = Input[3].Split(':')[1].Split(',', StringSplitOptions.TrimEntries).Select(int.Parse).ToList( );

	}


	public override async Task<string> SolvePart1()
	{
		var pointer = 0;
		var output = new List<int>();
		while (pointer < program.Count)
		{
			var (opcode, operand) = Read(pointer);

			switch (opcode)
			{
				case OpCode.Adv:
					registers[A] = (int)Math.Truncate(registers[A] / Math.Pow(2, operand));
					pointer += 2;
					break;
				case OpCode.Bxl:
					registers[B] ^= operand;
					pointer += 2;
					break;
				case OpCode.Bst:
					registers[B] = operand % 8;
					pointer += 2;
					break;
				case OpCode.Jnz:
					if (registers[A] > 0)
						pointer += operand;
					break;
				case OpCode.Bcx:
					registers[B] ^= registers[C];
					pointer += 2;
					break;
				case OpCode.Out:
					output.Add(operand % 8);
					pointer += 2;
					break;
				case OpCode.Bdv:
					registers[B] = (int)Math.Truncate(registers[A] / Math.Pow(2, operand));
					pointer += 2;
					break;
				case OpCode.Cdv:
					registers[C] = (int)Math.Truncate(registers[A] / Math.Pow(2, operand));
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		

		return string.Join(",", output);
	}

	private (OpCode opcode, int operand) Read(int pointer)
	{
		var opcode = (OpCode)program[pointer];
		var operand = GetOperand(opcode, pointer);
		return (opcode, operand);
	}

	private int GetOperand(OpCode opcode, int pointer)
	{
		var isLiteral = opcode is OpCode.Bxl or OpCode.Jnz;
		var operand = program[pointer + 1];

		return isLiteral ? operand : GetCombo(operand);
	}

	private int GetCombo(int operand) => operand switch
	{
		>= 0 and <= 3 => operand,
		4 => registers[A],
		5 => registers[B],
		6 => registers[C],
		_ => throw new ArgumentOutOfRangeException()
	};

	private enum OpCode
	{
		Adv, Bxl, Bst, Jnz, Bcx, Out, Bdv, Cdv
	}

	public override async Task<string> SolvePart2()
	{
		return string.Empty;
	}
}
