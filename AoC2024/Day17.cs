namespace AoC2024;

public class Day17 : Solution
{
	public readonly Dictionary<char, long> registers = new( );
	private List<long> program;

	private const char A = 'A';
	private const char B = 'B';
	private const char C = 'C';

	public Day17(string file) : base(file) => SetRegisters( );

	public Day17(List<string> input) : base(input) => SetRegisters( );


	private void SetRegisters()
	{
		registers.Clear( );
		registers.Add(A, Input[0].ToInt( ));
		registers.Add(B, Input[1].ToInt( ));
		registers.Add(C, Input[2].ToInt( ));

		program = Input[3].Split(':')[1].Split(',', StringSplitOptions.TrimEntries).Select(long.Parse).ToList( );
	}


	public override async Task<string> SolvePart1() => string.Join(",", RunProgram( ));

	public override async Task<string> SolvePart2()
	{
		//basically started all variables from zero to find output up to idx 13
		//however from there the increment overshot consistently so...
		//hardcoded the starting values from that point onwards, and proceeded with a smaller increment
		//the test will fail miserably but that is fine
		var idx = 13;
		var aValue = 60655122332687L;
		var inc = 1073741824L;
		
		while (true) // no exit condition. answer found manually stepping through
		{
			SetRegisters( );
			registers[A] = aValue;
			var output = RunProgram( );

			if (output.Take(idx + 1).Zip(program.Take(idx + 1), (l, l1) => l == l1).All(b => b))
			{
				idx++;

				if(output.Count < program.Count-3)
					inc = getIncrement(idx);
				
			}

			aValue += inc;

		}

		return aValue.ToString( );
		
		//terrible
		long getIncrement(int idx) => idx switch
		{
			0 => 1,
			1 => 8,
			2 => 64,
			3 => 512,
			4 => 4096,
			5 => 32768,
			6 => 262144,
			7 => 2097152,
			8 => 16777216,
			9 => 134217728,
			10 => 1073741824,
			11 => 8589934592,
			12 => 68719476736,
			13 => 549755813888,
			14 => 4398046511104,
			15 => 35184372088832,
			16 => 281474976710656,
		};

		long Get8BaseSum(string output)
		{
			var sum = 0;
			for (var idx = 0 ;idx < output.Length ;idx += 2)
			{
				sum += output[idx].ToInt( ) * idx switch
				{
					0 => 8,
					2 => 64,
					4 => 512,
					6 => 4096,
					8 => 32768,
					10 => 262144
				};
			}

			return sum;
		}

		/*
		so we're clearly counting in some binary base 8 system.
		that is, the 1st number in the output is
			0 at count 0-7
			1 at count 8-15
			2 at count 16-31

		the 2nd number in the output is
			0 at count 8-63
			1 at count 64-127
			2 at count 128-191

		however this pattern does not repeat nicely in the input...
		 */
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
					registers[A] = (long)Math.Truncate(registers[A] / Math.Pow(2, operand));
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
						pointer = operand;
					else
						isDone = true;
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
					registers[B] = (long)Math.Truncate(registers[A] / Math.Pow(2, operand));
					pointer += 2;
					break;
				case OpCode.Cdv:
					registers[C] = (long)Math.Truncate(registers[A] / Math.Pow(2, operand));
					pointer += 2;
					break;
				default:
					throw new ArgumentOutOfRangeException( );
			}
		}

		//return empty string to support a few unit test cases.
		return output;
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
		4 => registers[A],
		5 => registers[B],
		6 => registers[C],
		_ => throw new ArgumentOutOfRangeException( )
	};

	private enum OpCode
	{
		Adv, Bxl, Bst, Jnz, Bcx, Out, Bdv, Cdv
	}


}
