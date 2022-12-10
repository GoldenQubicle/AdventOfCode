namespace AoC2022
{
    public class Day10 : Solution
    {
        private readonly IEnumerable<(string, long)> instructions;

        public Day10(string file) : base(file) => instructions = Input.Select(l => 
            l.StartsWith("noop") ? ("noop", 0) : (l.Split(" ")[0], long.Parse(l.Split(" ")[1])));


        public override string SolvePart1()
        {
            var x = 1L;
            var cycle = 0;
            var signals = new List<long>();
            var cyclesToCheck = new List<int> { 20, 60, 100, 140, 180, 220 };
            foreach (var (op, v) in instructions)
            {
                cycle++;

                if (cyclesToCheck.Contains(cycle))
                    signals.Add(cycle * x);

                if (op.Equals("noop"))
                    continue;

                cycle++;

                if (cyclesToCheck.Contains(cycle))
                    signals.Add(cycle * x);

                x += v;

            }

            return signals.Sum().ToString();
        }

        public override string SolvePart2()
        {
            var x = 1L;
            var cycle = 0;
            var crt = new StringBuilder();

            List<long> GetSpritePosition(long idx) => new() { idx - 1, idx, idx + 1 };

            foreach (var (op, v) in instructions)
            {
                if(cycle % 40 == 0) crt.Append(Environment.NewLine);
                crt.Append(GetSpritePosition(x).Contains(cycle % 40) ? "#" : ".");
                cycle++;
                
                if (op.Equals("noop"))
                    continue;

                if (cycle % 40 == 0) crt.Append(Environment.NewLine);
                crt.Append(GetSpritePosition(x).Contains(cycle % 40) ? "#" : ".");
                cycle++;

                x += v;
            }

            return crt.ToString();
        }
    }
}