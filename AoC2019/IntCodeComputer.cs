namespace AoC2019;

public class IntCodeComputer
{
	public IntCodeComputer(IEnumerable<long> input) =>
		Memory = input.ToList( );

	public IntCodeComputer(List<string> dayInput, Action<List<long>> SetMemory = default)
	{
		Memory = dayInput[0].Split(",").Select(long.Parse).ToList( );
		SetMemory?.Invoke(Memory);
	}

	private IntCodeComputer(List<long> memory, long p)
	{
		Memory = memory;
		pointer = p;
	}

	public IntCodeComputer Copy() => new(Memory.Select(m => m).ToList( ), pointer) { BreakOnOutput = BreakOnOutput };


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
		Offset = 09,
		Halt = 99
	}

	public List<long> Memory { get; }
	public Queue<long> Inputs { get; set; } = new( );
	public long Output { get; private set; }
	public int Id { get; init; }
	public bool BreakOnOutput { get; init; }
	public bool IsFinished { get; private set; }
	private long GetInput() => Inputs.TryDequeue(out var i) ? i : GetExternalInput( );

	public Func<int> GetExternalInput { get; set; }

	private long pointer;
	private long offset;
	private bool doBreak;

	public bool Execute()
	{
		doBreak = false;

		while (!IsFinished)
		{
			var (opCode, p1, p2, writeTo) = ParseInstruction( );

			if (opCode.IsWrite( ))
			{
				EnsureMemoryCapacity(writeTo);
				Memory[(int)writeTo] = opCode switch
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
				//Console.WriteLine($"Wrote output: {Output}");
				if (BreakOnOutput)
					doBreak = true;
			}

			if (opCode == OpCode.Offset)
			{
				offset += p1;
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

		return !IsFinished;
	}

	private Instruction ParseInstruction()
	{
		//not too happy about the conversion back-and-forth..
		var instruction = Memory[(int)pointer].ToString( ).PadLeft(5, '0');
		var opCode = (OpCode)int.Parse(instruction[^2..]);
		var modes = instruction[..3].Reverse( ).ToList( );
		var parameters = Memory
			.Skip((int)pointer + 1)
			.Take(InstructionLength(opCode) - 1)
			.WithIndex( )
			.Select(p =>
			{
				var immediate = p.Value;
				var position = immediate >= 0 && immediate <= Memory.Count - 1 ? Memory[(int)immediate] : 0;
				EnsureMemoryCapacity(immediate + offset);
				var relative = immediate + offset >= 0 && modes[p.idx] == '2' ? Memory[(int)(immediate + offset)] : 0;
				return new Parameter((Mode)modes[p.idx], immediate, position, relative, offset);
			}).ToList( );

		return new(opCode, parameters);
	}

	private void EnsureMemoryCapacity(long idx)
	{
		if (idx > int.MaxValue)
			return;

		if (idx > Memory.Count - 1)
			Memory.AddRange(new long[idx - Memory.Count + 1]);
	}

	private record Instruction(OpCode OpCode, List<Parameter> Parameters)
	{
		public void Deconstruct(out OpCode opCode, out long p1, out long p2, out long writeTo)
		{
			opCode = OpCode;
			p1 = GetParameter(0);
			p2 = GetParameter(1);
			writeTo = OpCode.IsWrite( )
				? Parameters.Last( ).Mode == Mode.Relative
					? Parameters.Last( ).Value + Parameters.Last( ).Offset
					: Parameters.Last( ).Value
				: 0;
		}

		private long GetParameter(int idx) =>
			Parameters.Count - 1 >= idx
				? Parameters[idx].Mode == Mode.Position
					? Parameters[idx].Position
					: Parameters[idx].Mode == Mode.Relative
					? Parameters[idx].Relative
					: Parameters[idx].Value
				: 0;
	}

	private record Parameter(Mode Mode, long Value, long Position, long Relative, long Offset);

	private enum Mode { Position = '0', Immediate = '1', Relative = '2' }

	private int InstructionLength(OpCode opCode) => opCode switch
	{
		OpCode.Halt => 1,
		OpCode.Input or OpCode.Output or OpCode.Offset => 2,
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