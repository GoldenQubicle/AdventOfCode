namespace AoC2022
{
    public class Day01 : Solution
    {
        private Dictionary<int, long> elfs;
        public Day01(string file) : base(file) => elfs =
            File.ReadAllLines(Path.Combine(Path.GetDirectoryName(GetType().Assembly.Location), $"data/{file}.txt"))
             .Aggregate(new Dictionary<int, long> { { 1, 0 } }, (result, line) => line switch
             {
                 "" => result.AddNew(result.Count + 1, 0L),
                 _ => result.AddTo(result.Count, v => v += long.Parse(line))
             });

        public override string SolvePart1() => elfs.Values.Max().ToString();

        public override string SolvePart2() => elfs.Values.OrderByDescending(e => e).Take(3).Sum().ToString();

    }
}