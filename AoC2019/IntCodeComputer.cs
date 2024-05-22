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

	
	private record struct Instruction(OpCode OpCode, int P1, int P2, int P3);

	public void Execute()
	{
		var instruction = ParseInstruction( );

		while (instruction.OpCode != OpCode.Halt)
		{
			Memory[instruction.P3] = instruction.OpCode switch
			{
				OpCode.Add =>  instruction.P1 + instruction.P2,
				OpCode.Mult => instruction.P1 * instruction.P2,

				_ => throw new ArgumentOutOfRangeException( )
			};

			pointer += InstructionLength(instruction.OpCode);
			instruction = ParseInstruction( );
		}
	}

	private Instruction ParseInstruction()
	{
		//not too happy about the conversion back-and-forth..
		var instruction = Memory[pointer].ToString( ).PadLeft(5, '0');
		var opCode = (OpCode)int.Parse(instruction[^2..]);

		if (opCode == OpCode.Halt)
			return new(opCode, 0, 0, 0);

		var modes = instruction[..3].Reverse( ).ToList( );

		//not happy either with this, it's messy and hard to follow.
		//Ideally any instruction which writes has a flag to indicate as such, with a write parameter
		var parameters = Memory
			.Skip(pointer + 1)
			.Take(3) // just take next 3 entries from memory
			.WithIndex( )
			.Select(p => modes[p.idx] == '0' && p.idx < 2 //when writing, i.e. the third parameter is always the memory value!
				? Memory[p.Value] // position mode
				: p.Value).ToList( ); // immediate mode, again, when writing this is correct!



		return new(opCode, parameters[0], parameters[1], parameters[2]);
	}

	/// <summary>
	/// Returns the total length of the instruction, inclusive with operation code. 
	/// </summary>
	/// <param name="opCode"></param>
	/// <returns></returns>
	/// <exception cref="ArgumentOutOfRangeException"></exception>
	private int InstructionLength(OpCode opCode) => opCode switch
	{
		OpCode.Add => 4,
		OpCode.Mult => 4,
		OpCode.Halt => 1,
		OpCode.Input => 2,
		OpCode.Output => 2,
		_ => throw new ArgumentOutOfRangeException(nameof(opCode), opCode, null)
	};

	public static IEnumerable<int> Convert(List<string> input) => 
		input[0].Split(",").Select(int.Parse);

}