namespace AoC2022
{
    public class Day11 : Solution
    {
        private readonly List<Monkey> monkeys = new();

        public Day11(string file) : base(file) => Input.Select((l, idx) => (l, idx)).ForEach(t =>
        {
            var (line, idx) = t;
            if (!line.StartsWith("Monkey")) return;

            var opParts = Input[idx + 2][17..].Split(" ");
            var div = long.Parse(Input[idx + 3].Split(" ").Last());

            monkeys.Add(new()
            {
                Divisor = div,
                Items = Input[idx + 1][15..].Split(",").Select(long.Parse).ToList(),
                Operation = old => opParts[1].Equals("+")
                    ? (old + (opParts[2].Equals("old") ? old : long.Parse(opParts[2])))
                    : (old * (opParts[2].Equals("old") ? old : long.Parse(opParts[2]))),
                Test = wl => wl % div == 0
                    ? int.Parse(Input[idx + 4].Split(" ").Last())
                    : int.Parse(Input[idx + 5].Split(" ").Last())
            });
        });


        public override async Task<string> SolvePart1() => DoMonkeyBusiness(20, wl => wl / 3);

        public override async Task<string> SolvePart2() => DoMonkeyBusiness(10_000, wl => wl % monkeys.Aggregate(1L, (mod, monkey) => mod * monkey.Divisor));

        private string DoMonkeyBusiness(int rounds, Func<long, long> getWorryLevel)
        {
            Enumerable.Range(0, rounds)
                .ForEach(round => monkeys.ForEach(m => m.SlingItems(getWorryLevel)
                    .ForEach(item => monkeys[item.idx].Items.Add(item.wl))));

            return monkeys
                .OrderByDescending(m => m.ItemCount)
                .Take(2)
                .Aggregate(1L, (i, monkey) => i * monkey.ItemCount).ToString();
        }


        internal class Monkey
        {
            public long Divisor { get; init; }
            public List<long> Items { get; init; }
            public Func<long, long> Operation { get; init; }
            public Func<long, int> Test { get; init; }
            public long ItemCount { get; private set; }

            public IEnumerable<(int idx, long wl)> SlingItems(Func<long, long> getWorryLevel)
            {
                foreach (var item in Items)
                {
                    ItemCount++;
                    var worryLevel = getWorryLevel(Operation(item));
                    yield return (Test(worryLevel), worryLevel);
                }
                Items.Clear();
            }
        }
    }
}