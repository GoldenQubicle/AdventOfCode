using System.Collections.Concurrent;

namespace AoC2019;

public class IntCodeComputer
{
	public IntCodeComputer(IEnumerable<long> input) =>
		_memory = input.WithIndex( ).ToConcurrentDictionary(s => (long)s.idx, s => s.Value);

	public IntCodeComputer(List<string> dayInput) =>
		_memory = dayInput[0].Split(",").WithIndex( ).ToConcurrentDictionary(s => (long)s.idx, s => long.Parse(s.Value));


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

	public List<long> Memory() => _memory.Select(kvp => kvp.Value).ToList( );

	public void SetMemory(long address, long value)
	{
		if (!_memory.TryAdd(address, value))
		{
			_memory[address] = value;
		}
	}
	private ConcurrentDictionary<long, long> _memory { get; }
	public Queue<long> Inputs { get; set; }
	public long Output { get; private set; }
	public int Id { get; init; }
	public bool BreakOnOutput { get; init; }
	public bool IsFinished { get; private set; }
	private long GetInput() => Inputs.Dequeue( );
	private long pointer;
	private long offset;
	private bool doBreak;

	public bool Execute()
	{
		doBreak = false;

		while (true)
		{
			var (opCode, p1, p2, writeTo) = ParseInstruction( );

			if (opCode.IsWrite( ))
			{
				EnsureMemoryCapacity(writeTo);
				_memory[(int)writeTo] = opCode switch
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
				Console.WriteLine($"Wrote output: {Output}");
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

		return true;
	}

	private Instruction ParseInstruction()
	{
		//not too happy about the conversion back-and-forth..
		EnsureMemoryCapacity(pointer);
		var instruction = _memory[(int)pointer].ToString( ).PadLeft(5, '0');
		var opCode = (OpCode)int.Parse(instruction[^2..]);
		var modes = instruction[..3].Reverse( ).ToList( );
		var parameters = _memory
			.Skip((int)pointer + 1)
			.Take(InstructionLength(opCode) - 1)
			//.Select(kvp => kvp.Value)
			.WithIndex( )
			.Select(p =>
			{
				var immediate = p.Value.Value;
				var position = immediate >= 0 && _memory.TryGetValue(immediate, out var im) ? im : 0;
				var relative = immediate + offset >= 0 && _memory.TryGetValue(immediate + offset, out var rel) ? rel : 0;
				return new Parameter((Mode)modes[p.idx], immediate, position, relative, offset);
			}).ToList( );

		return new(opCode, parameters);
	}

	private void EnsureMemoryCapacity(long idx)
	{
		if (!_memory.ContainsKey(idx))
			_memory.TryAdd(idx, 0);
		//if (idx > int.MaxValue) return;

		//if (idx > _memory.Count - 1)
		//	_memory.AddRange(new long[idx - _memory.Count + 1]);
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