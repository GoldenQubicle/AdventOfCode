namespace AoC2016
{
    public class Day18 : Solution
    {
        private const char Safe = '.';
        private const char Trap = '^';
        private readonly List<string> tiles = new();
        public int TotalRows { get; set; } = 40;
        public Day18(string file) : base(file, "\n") => tiles.Add(Input[0]);

        public override async Task<string> SolvePart1( ) => GetSafeTiles();

        public override async Task<string> SolvePart2()
        {
            TotalRows = 400000;
            return GetSafeTiles();
        }

        private string GetSafeTiles()
        {
            for (var r = 1; r < TotalRows; r++)
            {
                var lastRow = tiles.Last();

                var newRow = new string(lastRow.Select((c, i) =>
                        i - 1 < 0 ? (Safe, c, lastRow[i + 1]) :
                        i + 1 >= lastRow.Length ? (lastRow[i - 1], c, Safe) : 
                        (lastRow[i - 1], c, lastRow[i + 1]))
                    .Select(above => above switch
                    {
                        (Trap, Trap, Safe) or (Safe, Trap, Trap) or (Trap, Safe, Safe) or (Safe, Safe, Trap) => true,
                        _ => false
                    } ? Trap : Safe).ToArray());

                tiles.Add(newRow);
            }
            return tiles.Sum(row => row.Count(tile => tile == Safe)).ToString();
        }
    }
}