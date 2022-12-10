namespace AoC2022
{
    public class Day10 : Solution
    {
        private readonly IEnumerable<(string, int)> instructions;

        private readonly List<int> cyclesToCheck = new() { 20, 60, 100, 140, 180, 220 };
        private readonly List<int> signals = new();
        private readonly StringBuilder crt = new();
        public Day10(string file) : base(file) => instructions = Input.Select(l =>
            l.StartsWith("noop") ? ("noop", 0) : (l.Split(" ")[0], int.Parse(l.Split(" ")[1])));

        public override string SolvePart1() => DoCycles(isPart1: true);

        public override string SolvePart2() => DoCycles(isPart1: false);

        private string DoCycles(bool isPart1)
        {
            var x = 1;
            var cycle = 0;

            foreach (var (op, v) in instructions)
            {
                cycle = isPart1 ? DoCyclePart1(cycle, x) : DoCyclePart2(cycle, x);

                if (op.Equals("noop"))
                    continue;

                cycle = isPart1 ? DoCyclePart1(cycle, x) : DoCyclePart2(cycle, x);

                x += v;
            }

            return isPart1 ? signals.Sum().ToString() : crt.ToString();
        }

        private int DoCyclePart1(int cycle, int x)
        {
            cycle++;

            if (cyclesToCheck.Contains(cycle))
                signals.Add(cycle * x);

            return cycle;
        }

        private int DoCyclePart2(int cycle, int x)
        {
            if (cycle % 40 == 0)
                crt.Append(Environment.NewLine);

            crt.Append(GetSpritePosition(x).Contains(cycle % 40) ? "#" : ".");

            return ++cycle;
        }

        private static List<long> GetSpritePosition(long idx) => new() { idx - 1, idx, idx + 1 };

    }
}