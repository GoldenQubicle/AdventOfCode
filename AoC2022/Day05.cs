namespace AoC2022
{
    public class Day05 : Solution
    {
        private List<string> containers;
        private List<(int q, int from, int to)> moves;
        public Day05(string file) : base(file) => Input.GroupBy(l => l.StartsWith("move"))
            .ForEach(g =>
            {
                if (g.Key == false) containers = g.ToList();
                else moves = g.Select(l => Regex.Match(l, @"move (\d+) from (\d+) to (\d+)"))
                        .Select(m => (q: int.Parse(m.Groups[1].Value), from: int.Parse(m.Groups[2].Value) - 1, to: int.Parse(m.Groups[3].Value) - 1)).ToList();
            });

        public override string SolvePart1() => DoContainerMoves(true);

        public override string SolvePart2() => DoContainerMoves(false);
       
        private string DoContainerMoves(bool isPartOne)
        {
            foreach (var move in moves)
            {
                var cargo = containers[move.from][..move.q];
                
                if (isPartOne)
                    cargo = new string(cargo.Reverse().ToArray());

                containers[move.from] = containers[move.from][move.q..];
                containers[move.to] = containers[move.to].Insert(0, cargo);
            }

            return new string(containers.Select(c => c.First()).ToArray());
        }
    }
}