namespace AoC2021;

public class Day16 : Solution
{
	private readonly Dictionary<char, string> mapping = new( )
	{
		{ '0', "0000" },
		{ '1', "0001" },
		{ '2', "0010" },
		{ '3', "0011" },
		{ '4', "0100" },
		{ '5', "0101" },
		{ '6', "0110" },
		{ '7', "0111" },
		{ '8', "1000" },
		{ '9', "1001" },
		{ 'A', "1010" },
		{ 'B', "1011" },
		{ 'C', "1100" },
		{ 'D', "1101" },
		{ 'E', "1110" },
		{ 'F', "1111" },
	};

	private readonly string bits;

	public Day16(string file) : base(file) => bits = HexToBinaryString(Input.First( ));

	public Day16(List<string> input) : base(input) => bits = HexToBinaryString(Input.First( ));


	public override async Task<string> SolvePart1() => Packet.Parse(bits)
		.First( ).GetVersion( ).ToString( );

	public override async Task<string> SolvePart2() => Packet.Parse(bits)
		.First( ).GetValue( ).ToString( );


	private string HexToBinaryString(string input) => input
		.Aggregate(new StringBuilder( ), (sb, c) => sb.Append(mapping[c])).ToString( );


	private abstract class Packet(int version, int length, long value)
	{
		protected int Version { get; } = version;
		private int Length { get; } = length;
		protected long Value { get; } = value;

		public abstract int GetVersion();
		public abstract long GetValue();

		public static List<Packet> Parse(string bits, bool checkNext = true)
		{
			if (bits.Length <= 7)
				return new List<Packet>( );

			var idx = 0;
			var version = GetValue(3);
			var typeId = GetValue(3);

			if (typeId == 4)
			{
				var g = bits[idx..(idx += 5)];
				var sb = new StringBuilder( );
				while (g[0] == '1')
				{
					sb.Append(g[1..]);
					g = bits[idx..(idx += 5)];
				}

				sb.Append(g[1..]);
				var value = Convert.ToInt64(sb.ToString( ), 2);
				return CheckForNext(new LiteralPacket(version, idx, value));
			}

			var lengthType = bits[idx..(idx += 1)];

			switch (lengthType)
			{
				case "0":

					var subPacketLength = GetValue(15);
					var length = idx + subPacketLength;

					return CheckForNext(new OperatorPacket(version, length, typeId, bits[idx..(idx += subPacketLength)]));

				case "1":

					var subPacketsCount = GetValue(11);
					var subPackets = new List<Packet>( );
					
					while (subPackets.Count < subPacketsCount)
					{
						var t = bits[idx..];
						var subPacket = Parse(t, checkNext: false).First( );
						idx += subPacket.Length;
						subPackets.Add(subPacket);
					}

					return CheckForNext(new OperatorPacket(version, idx, typeId, subPackets));
			}


			return new( );


			int GetValue(int end) => Convert.ToInt32(bits[idx..(idx += end)], 2);


			List<Packet> CheckForNext(Packet packet) => checkNext
				? Parse(bits[idx..]).Expand(packet)
				: new( ) { packet };

		}
	}

	private class LiteralPacket(int version, int length, long value) : Packet(version, length, value)
	{
		public override int GetVersion() => Version;
		public override long GetValue() => Value;
	}


	private class OperatorPacket : Packet
	{
		private readonly int typeId;
		private List<Packet> SubPackets { get; }

		public OperatorPacket(int version, int length, int typeId, List<Packet> subPackets) : base(version, length, 0L)
		{
			this.typeId = typeId;
			SubPackets = subPackets;
		}

		public OperatorPacket(int version, int length, int typeId, string bits) : base(version, length, 0L)
		{
			this.typeId = typeId;
			SubPackets = Parse(bits);
			SubPackets.Reverse( );
		}

		public override int GetVersion() =>
			SubPackets.Count > 0 ? SubPackets.Sum(p => p.GetVersion( )) + Version : Version;

		public override long GetValue() => typeId switch
		{
			0 => SubPackets.Sum(sp => sp.GetValue( )),
			1 => SubPackets.Aggregate(1L, (l, packet) => l * packet.GetValue( )),
			2 => SubPackets.Min(sp => sp.GetValue( )),
			3 => SubPackets.Max(sp => sp.GetValue( )),
			5 => SubPackets[0].GetValue( ) > SubPackets[1].GetValue( ) ? 1 : 0,
			6 => SubPackets[0].GetValue( ) < SubPackets[1].GetValue( ) ? 1 : 0,
			7 => SubPackets[0].GetValue( ) == SubPackets[1].GetValue( ) ? 1 : 0,
			_ => throw new NotImplementedException( )
		};
	}
}
