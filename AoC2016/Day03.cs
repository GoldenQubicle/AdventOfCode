namespace AoC2016
{
    public class Day03 : Solution
    {
        private readonly List<int[]> triangles;
        public Day03(string file) : base(file, "\n")
        {
            triangles = Input
                .Select(line => line.Split(" ").Where(s => !string.IsNullOrEmpty(s)).Select(s => s.Trim()).Select(int.Parse).ToArray()).ToList();
        }
        
        public override async Task<string> SolvePart1() => triangles.Count(IsValidTriangle).ToString();

        public override async Task<string> SolvePart2()
        {
            var validCount = 0;
            for (var i = 0; i < triangles.Count / 3; i++)
            {
                var group = triangles.Skip(i * 3).Take(3).ToList();
                for (var j = 0; j < 3; j++)
                {
                    if (IsValidTriangle(group.Select(g => g[j]).ToArray()))
                        validCount++;
                }
            }
            return validCount.ToString();
        }


        private bool IsValidTriangle(int[] sides) =>
            sides[0] + sides[1] > sides[2] &&
            sides[0] + sides[2] > sides[1] &&
            sides[1] + sides[2] > sides[0];

    }
}