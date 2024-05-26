namespace AoC2019;

public class IntCodeComputer(IEnumerable<int> input)
{
	public int Input { get; set; }
	public int Output { get; set; }
	public List<int> Memory { get; init; } = input.ToList( );

	private int pointer;

	private enum OpCode
	{
		Add = 01,
		Mult = 02,
		Input = 03,
		Output = 04,
		JumpTrue = 05,
		JumpFalse = 06,
		LessThan = 07,
		Equals = 08,
		Halt = 99
	}

	private enum Mode
	{
		Position = '0',
		Immediate = '1'
	}

	private record Parameter(Mode Mode, int Value);

	private record Instruction(OpCode OpCode, int Increment, List<Parameter> Parameters)
	{
		private List<Parameter> Parameters { get; } = Parameters;

		public bool IsWrite => OpCode is OpCode.Add or OpCode.Mult or OpCode.Input or OpCode.LessThan or OpCode.Equals;

		public bool IsJump => OpCode is OpCode.JumpFalse or OpCode.JumpTrue;

		public int WriteTo => Parameters.Last().Value;

		public int GetParameter(int idx, List<int> memory) =>
			Parameters.Count - 1 >= idx
				? Parameters[idx].Mode == Mode.Position
					? memory[Parameters[idx].Value]
					: Parameters[idx].Value
				: 0;
	};

	public void Execute()
	{
		var instruction = ParseInstruction( );

		while (instruction.OpCode != OpCode.Halt)
		{
			var p1 = instruction.GetParameter(0, Memory);
			var p2 = instruction.GetParameter(1, Memory);

			if (instruction.IsWrite)
			{
				Memory[instruction.WriteTo] = instruction.OpCode switch
				{
					OpCode.Add => p1 + p2,
					OpCode.Mult => p1 * p2,
					OpCode.Input => Input,
					OpCode.LessThan => p1 < p2 ? 1 : 0,
					OpCode.Equals => p1 == p2 ? 1 : 0,
				};
			}

			if (instruction.OpCode == OpCode.Output)
			{
				Output = p1; 
				Console.WriteLine($"Wrote output {Output}");
			}

			if (instruction.IsJump)
			{
				pointer = instruction.OpCode switch
				{
					OpCode.JumpTrue => p1 != 0 ? p2 : pointer + instruction.Increment,
					OpCode.JumpFalse => p1 == 0 ? p2 : pointer + instruction.Increment,
				};
			}
			else
			{
				pointer += instruction.Increment;
			}
			
			instruction = ParseInstruction( );
		}
	}

	private Instruction ParseInstruction()
	{
		//not too happy about the conversion back-and-forth..
		var instruction = Memory[pointer].ToString( ).PadLeft(5, '0');
		var opCode = (OpCode)int.Parse(instruction[^2..]);
		var modes = instruction[..3].Reverse( ).ToList( );
		var parameters = Memory
			.Skip(pointer + 1)
			.Take(InstructionLength(opCode) - 1)
			.WithIndex( )
			.Select(p => new Parameter((Mode)modes[p.idx], p.Value))
			.ToList( );

		return new(opCode, InstructionLength(opCode), parameters);
	}

	/// <summary>
	/// Returns the total length of the instruction, inclusive with operation code. 
	/// </summary>
	/// <param name="opCode"></param>
	/// <returns></returns>
	/// <exception cref="ArgumentOutOfRangeException"></exception>
	private int InstructionLength(OpCode opCode) => opCode switch
	{
		OpCode.Halt => 1,
		OpCode.Input or OpCode.Output => 2,
		OpCode.JumpFalse or OpCode.JumpTrue => 3,
		OpCode.Add or OpCode.Mult or OpCode.Equals or OpCode.LessThan => 4,
		_ => throw new ArgumentOutOfRangeException(nameof(opCode), opCode, null)
	};
}