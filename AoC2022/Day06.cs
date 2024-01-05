namespace AoC2022
{
    public class Day06 : Solution
    {
        public Day06(string input) : base(input) { }
        public Day06(List<string> input) : base(input) { }

        public override async Task<string> SolvePart1() => FindFirstMarker(4);
        public override async Task<string> SolvePart2() => FindFirstMarker(14);

        private string FindFirstMarker(int markerLength) => Enumerable.Range(0, Input[0].Length - markerLength)
            .Aggregate(new List<int>(), (markers, idx) =>
                    Input[0][idx..(idx + markerLength)].Distinct().Count() == markerLength
                        ? markers.Expand(idx + markerLength) 
                        : markers).First().ToString();
    }
}