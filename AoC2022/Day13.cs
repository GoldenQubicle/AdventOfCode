namespace AoC2022
{
    public class Day13 : Solution
    {
        private readonly List<(Packet left, Packet right)> pairs;
        public Day13(string file) : base(file) => pairs = Input.Chunk(2)
            .Select(p => (ParsePackets(p[0][1..^1]), ParsePackets(p[1][1..^1]))).ToList();

        public override string SolvePart1() => pairs
            .Select((p, idx) => (result: ComparePackets(p.left, p.right), idx: idx + 1))
            .Where(r => r.result == 1)
            .Sum(r => r.idx).ToString();

        private int ComparePackets(Packet left, Packet right)
        {
            if (left.IsValue && right.IsValue)
            {
                if (left.Value < right.Value) return 1;
                if (left.Value == right.Value) return 0;
                if (left.Value > right.Value) return -1;
            }

            // wrap single value in new packet in order to compare it
            var lp = left.IsValue ? new(left.Value) : left;
            var rp = right.IsValue ? new(right.Value) : right;

            var i = 0;
            while (i < lp.Count && i < rp.Count)
            {
                var result = ComparePackets(lp[i], rp[i]);
                if (result != 0) return result;
                i++;
            }

            // a packet ran out of items, however the values were inconclusive so now check packet lengths
            if (lp.Count < rp.Count) return 1;

            if (lp.Count == rp.Count) return 0;

            return -1;

        }

        public override string SolvePart2() => null;


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

        internal class Packet
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
        }
    }
}