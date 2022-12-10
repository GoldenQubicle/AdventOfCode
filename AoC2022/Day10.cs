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


        public override string SolvePart1() => DoCycles((c, x) =>
        {
            c++;

            if (cyclesToCheck.Contains(c))
                signals.Add(c * x);

            return c;
        }, 
            () => signals.Sum().ToString());

        public override string SolvePart2() => DoCycles((c, x) =>
        {
            if (c % 40 == 0)
                crt.Append(Environment.NewLine);

            crt.Append(GetSpritePosition(x).Contains(c % 40) ? "#" : ".");

            return ++c;
        }, 
            () => crt.ToString());


        private string DoCycles(Func<int, int, int> doCycle, Func<string> result)
        {
            var x = 1;
            var cycle = 0;

            foreach (var (op, v) in instructions)
            {
                cycle = doCycle(cycle, x);

                if (op.Equals("noop"))
                    continue;

                cycle = doCycle(cycle, x);

                x += v;
            }

            return result();
        }

        private static List<int> GetSpritePosition(int idx) => new() { idx - 1, idx, idx + 1 };
    }
}