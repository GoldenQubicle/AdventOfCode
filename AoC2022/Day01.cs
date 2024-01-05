namespace AoC2022
{
    public class Day01 : Solution
    {
        private Dictionary<int, long> elfs;
        public Day01(string file) : base(file, doRemoveEmptyLines: false) => elfs = Input
             .Aggregate(new Dictionary<int, long> { { 1, 0 } }, (result, line) => line switch
             {
                 "" => result.AddNew(result.Count + 1, 0L),
                 _ => result.AddTo(result.Count, v => v += long.Parse(line))
             });

        public override async Task<string> SolvePart1() => elfs.Values.Max().ToString();

        public override async Task<string> SolvePart2() => elfs.Values.OrderByDescending(e => e).Take(3).Sum().ToString();

    }
}