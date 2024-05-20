namespace AoC2019;

public class IntCodeComputer(IEnumerable<int> instructions)
{
	public List<int> Memory { get; init; } = instructions.ToList( );

	private int _pointer;

	public enum OpCode
	{
		Add = 1,
		Mult = 2,
		Halt = 99
	}

	public void Execute()
	{
		var opCode = (OpCode)Memory[_pointer];

		while (opCode != OpCode.Halt)
		{
			var read1 = Memory[_pointer + 1];
			var read2 = Memory[_pointer + 2];
			var write = Memory[_pointer + 3];

			if (read1 >= Memory.Count || read2 >= Memory.Count || write >= Memory.Count)
			{
				opCode = OpCode.Halt;
				continue;
			}

			Memory[write] = opCode switch
			{
				OpCode.Add =>  Memory[read1] + Memory[read2],
				OpCode.Mult => Memory[read1] * Memory[read2],
				_ => throw new ArgumentOutOfRangeException( )
			};

			opCode = (OpCode)Memory[_pointer+=InstructionLength(opCode)];
		}
	}

	private int InstructionLength(OpCode op) => op switch
	{
		OpCode.Add => 4,
		OpCode.Mult => 4,
		OpCode.Halt => 1,
		_ => throw new ArgumentOutOfRangeException(nameof(op), op, null)
	};
}