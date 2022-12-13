namespace AoC2022
{
    public class Day13 : Solution
    {
        public Day13(string file) : base(file) { }

        public override string SolvePart1() => Input.Chunk(2)
            .Select(p => (left: ParsePackets(p[0][1..^1]), right: ParsePackets(p[1][1..^1])))
            .Select((p, idx) => (result: p.left.CompareTo(p.right), idx: idx + 1))
            .Where(r => r.result == -1)
            .Sum(r => r.idx).ToString();

        public override string SolvePart2()
        {
            var packets = Input.Select(line => ParsePackets(line[1..^1])).ToList();
            var d2 = GetDivider(2);
            var d6 = GetDivider(6);
            
            packets.AddRange(new List<Packet> { d2, d6 });
            packets.Sort();

            return ((packets.IndexOf(d2) + 1) * (packets.IndexOf(d6) + 1)).ToString();
        }

        private Packet GetDivider(int v)
        {
            var d = new Packet(null).Push();
            d.AddValue(v);
            return d.Pop();
        }

        private Packet ParsePackets(string line)
        {
            var current = new Packet(null);
            var idx = -1;
            while (idx++ < line.Length - 1)
            {
                current = line[idx] switch
                {
                    '[' => current.Push(),
                    ']' => current.Pop(),
                    _ => current
                };

                if (!char.IsDigit(line[idx])) continue;

                //dealing with integers when we get here, just need to find the first separator after it to parse it
                var (_, close) = line.Select((c, i) => (c, i)).FirstOrDefault(t => t.c is ',' or ']' && t.i > idx);
                var value = close == default ? line[idx..] : line[idx..close];
                current.AddValue(int.Parse(value));
            }

            return current;
        }

        internal class Packet : IComparable<Packet>
        {
            public int Count => Packets.Count;
            public bool IsValue => Value != -1;
            public int Value { get; init; } = -1;
            private Packet Parent { get; }
            private List<Packet> Packets { get; } = new();
            public Packet(Packet packet) => Parent = packet;
            public Packet(int value) => AddValue(value);
            public Packet this[int i] => Packets[i];
            public void AddValue(int v) => Packets.Add(new(this) { Value = v });
            public Packet Pop() => Parent;
            public Packet Push()
            {
                var newPacket = new Packet(this);
                Packets.Add(newPacket);
                return newPacket;
            }

            public override string ToString()
            {
                var sb = new StringBuilder();
                sb.Append('[');
                Packets.ForEach(p => sb.Append(p.IsValue ? $"{p.Value}," : p.ToString()));
                sb.Append(']');
                return sb.ToString();
            }

            public int CompareTo(Packet? other)
            {
                if (other is null) return 1;

                if (IsValue && other.IsValue)
                {
                    if (Value < other.Value) return -1;
                    if (Value == other.Value) return 0;
                    if (Value > other.Value) return 1;
                }

                // wrap single value in new packet in order to compare it
                var lp = IsValue ? new(Value) : this;
                var rp = other.IsValue ? new(other.Value) : other;

                var i = 0;
                while (i < lp.Count && i < rp.Count)
                {
                    var result = lp[i].CompareTo(rp[i]);
                    if (result != 0) return result;
                    i++;
                }

                // a packet ran out of items, however the values were inconclusive so now check packet lengths
                if (lp.Count < rp.Count) return -1;

                if (lp.Count == rp.Count) return 0;

                return 1;
            }
        }
    }
}