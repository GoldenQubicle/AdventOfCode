namespace AoC2022
{
    public class Day03 : Solution
    {
        public Day03(string file) : base(file) { }

        public override async Task<string> SolvePart1() => Input
            .Select(l => (l[..(l.Length / 2)], l[(l.Length / 2)..]))
            .Select(t => t.Item1.Intersect(t.Item2).First())
            .Sum(Priority).ToString();


        public override async Task<string> SolvePart2() => Input.Chunk(3)
            .Select(c => c[0].Intersect(c[1]).Intersect(c[2]).First())
            .Sum(Priority).ToString();

        private static int Priority(char c) => char.IsLower(c) ? c - 96 : c - 38;

    }
}