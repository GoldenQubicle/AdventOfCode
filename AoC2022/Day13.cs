namespace AoC2022
{
    public class Day13 : Solution
    {
        private readonly List<string[]> pairs;
        public Day13(string file) : base(file) => pairs = Input.Chunk(2).ToList();

        public override string SolvePart1()
        {
            var result = pairs.Select((p, idx) =>
            {
                var left = ParseData(p[0][1..^1]);
                var right = ParseData(p[1][1..^1]);

                return (rightOrder: ComparePackets(left, right), idx: idx + 1);

            }).Where(r => r.rightOrder == 1).ToList();

            return result.Sum(r => r.idx).ToString();

        }

        private int ComparePackets(Data left, Data right)
        {
            if (left.IsValue && right.IsValue)
            {
                if (left.Value < right.Value) return 1;
                if (left.Value == right.Value) return 0;
                if (left.Value > right.Value) return -1;
            }

            // wrap single value in 'list' in order to compare
            var ldl = left.IsValue ? new(left.Value) : left;
            var rdl = right.IsValue ? new(right.Value) : right;
            var i = 0;
            while (i < ldl.Count && i < rdl.Count)
            {
                var result = ComparePackets(ldl[i], rdl[i]);
                if (result != 0) return result;
                i++;
            }

            // a list ran out of items, however the values were inconclusive so now check list lengths
            if (ldl.Count == rdl.Count)
                return 0;

            if (ldl.Count < rdl.Count)
                return 1;

            return -1;

        }



        public override string SolvePart2() => null;


        private Data ParseData(string data)
        {
            var current = new Data(null);
            var idx = -1;
            while (idx++ < data.Length - 1)
            {
                if (data[idx] == '[')
                {
                    current = current.AddData();
                    continue;
                }

                if (data[idx] == ']')
                {
                    current = current.Parent;
                    continue;
                }

                if (!char.IsDigit(data[idx]))
                {
                    continue;
                }

                var (_, close) = data.Select((c, i) => (c, i)).FirstOrDefault(t => t.c is ',' or ']' && t.i > idx);
                var value = close == default ? data[idx..] : data[idx..close];
                current.AddValue(int.Parse(value));
            }

            return current;
        }

        internal class Data
        {
            public int Value { get; init; } = -1;
            private List<Data> NestedData { get; } = new();
            public Data Parent { get; init; }

            public Data(Data data) => Parent = data;

            public Data(int value) => AddValue(value);

            public bool IsValue => Value != -1;

            public void AddValue(int v) => NestedData.Add(new(this) { Value = v });

            public Data this[int i] => NestedData[i];

            public int Count => NestedData.Count;

            public Data AddData()
            {
                var newData = new Data(this);
                NestedData.Add(newData);
                return newData;
            }
        };
    }
}