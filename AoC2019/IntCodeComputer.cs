namespace AoC2019;

public class IntCodeComputer(IEnumerable<int> input)
{
	public List<int> Memory { get; } = input.ToList( );
	public Queue<int> Inputs { get; set; }
	public int Output { get; private set; }
	public int Id { get; init; }
	public bool BreakOnOutput { get; init; }
	public bool IsFinished { get; private set; }
	private int GetInput() => Inputs.Dequeue( );
	private int pointer;
	private bool doBreak;

	public enum OpCode
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

	public bool Execute()
	{
		doBreak = false;
		while (true)
		{
			var (opCode, p1, p2, writeTo) = ParseInstruction( );

			if (opCode.IsWrite( ))
			{
				Memory[writeTo] = opCode switch
				{
					OpCode.Add => p1 + p2,
					OpCode.Mult => p1 * p2,
					OpCode.Input => GetInput( ),
					OpCode.LessThan => p1 < p2 ? 1 : 0,
					OpCode.Equals => p1 == p2 ? 1 : 0,
				};
			}

			if (opCode == OpCode.Output)
			{
				Output = p1;

				if (BreakOnOutput)
					doBreak = true;
			}

			var next = pointer + InstructionLength(opCode);

			pointer = opCode switch
			{
				OpCode.JumpTrue => p1 != 0 ? p2 : next,
				OpCode.JumpFalse => p1 == 0 ? p2 : next,
				_ => next
			};

			IsFinished = opCode == OpCode.Halt;

			if (IsFinished || doBreak)
				break;
		}

		return true;
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
			.Select(p =>
			{
				var value = p.Value;
				var position = p.Value >= 0 && p.Value <= Memory.Count - 1 ? Memory[p.Value] : 0;
				return new Parameter((Mode)modes[p.idx], value, position);
			}).ToList( );

		return new(opCode, parameters);
	}

	private record Instruction(OpCode OpCode, List<Parameter> Parameters)
	{
		public void Deconstruct(out OpCode opCode, out int p1, out int p2, out int writeTo)
		{
			opCode = OpCode;
			p1 = GetParameter(0);
			p2 = GetParameter(1);
			writeTo = OpCode.IsWrite( ) ? Parameters.Last( ).Value : 0;
		}

		private int GetParameter(int idx) =>
			Parameters.Count - 1 >= idx
				? Parameters[idx].Mode == Mode.Position
					? Parameters[idx].Position
					: Parameters[idx].Value
				: 0;
	}

	private record Parameter(Mode Mode, int Value, int Position);

	private enum Mode { Position = '0', Immediate = '1' }

	private int InstructionLength(OpCode opCode) => opCode switch
	{
		OpCode.Halt => 1,
		OpCode.Input or OpCode.Output => 2,
		OpCode.JumpFalse or OpCode.JumpTrue => 3,
		OpCode.Add or OpCode.Mult or OpCode.Equals or OpCode.LessThan => 4,
		_ => throw new ArgumentOutOfRangeException(nameof(opCode), opCode, null)
	};
}

public static class OpCodeExtensions
{
	public static bool IsWrite(this IntCodeComputer.OpCode opCode) => opCode is
		IntCodeComputer.OpCode.Add or
		IntCodeComputer.OpCode.Mult or
		IntCodeComputer.OpCode.Input or
		IntCodeComputer.OpCode.Equals or
		IntCodeComputer.OpCode.LessThan;
}