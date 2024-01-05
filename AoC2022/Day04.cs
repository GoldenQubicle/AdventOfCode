namespace AoC2022
{
    public class Day04 : Solution
    {
        private readonly List<(int p1Start, int p1End, int p2Start, int p2End)> pairs;

        public Day04(string file) : base(file) => pairs = Input
            .Select(l => Regex.Match(l, @"(?<p1s>\d+)-(?<p1e>\d+),(?<p2s>\d+)-(?<p2e>\d+)"))
            .Select(m => (p1Start: int.Parse(m.Groups["p1s"].Value), p1End: int.Parse(m.Groups["p1e"].Value),
                          p2Start: int.Parse(m.Groups["p2s"].Value), p2End: int.Parse(m.Groups["p2e"].Value))).ToList();

        public override async Task<string> SolvePart1() => pairs
            .Count(p => (p.p1Start >= p.p2Start && p.p1End <= p.p2End) ||
                        (p.p2Start >= p.p1Start && p.p2End <= p.p1End)).ToString();

        public override async Task<string> SolvePart2() => pairs
            .Count(p => p.p1Start <= p.p2End && p.p1End >= p.p2Start).ToString();


    }
}