namespace AoC2022
{
    public class Day05 : Solution
    {
        private List<string> stacks;
        private List<(int q, int from, int to)> moves;

        public Day05(string file) : base(file) => Input.GroupBy(l => l.StartsWith("move")).ForEach(g =>
        {
            if (g.Key == false)
            {
                var totalStacks = g.Last().Split(" ").Max().AsInteger();
                stacks = Enumerable.Range(1, totalStacks).Select(n =>
                    new string(g.Take(totalStacks).Select(s => s[n + (n - 1) * 3]).ToArray()).Trim()).ToList();
            }
            else
            {
                moves = g.Select(l => Regex.Match(l, @"move (\d+) from (\d+) to (\d+)"))
                    .Select(m => (q: int.Parse(m.Groups[1].Value),
                                  from: int.Parse(m.Groups[2].Value) - 1,
                                  to: int.Parse(m.Groups[3].Value) - 1)).ToList();
            }
        });


        public override string SolvePart1() => DoContainerMoves(isPartOne: true);

        public override string SolvePart2() => DoContainerMoves(isPartOne: false);

        private string DoContainerMoves(bool isPartOne)
        {
            foreach (var (q, from, to) in moves)
            {
                var crates = stacks[from][..q];

                if (isPartOne)
                    crates = new(crates.Reverse().ToArray());

                stacks[from] = stacks[from][q..];
                stacks[to] = stacks[to].Insert(0, crates);
            }

            return new(stacks.Select(c => c.First()).ToArray());
        }
    }
}