namespace AoC2022
{
    public class Day06 : Solution
    {
        public Day06(string input) : base(input) { }
        public Day06(List<string> input) : base(input) { }

        public override string SolvePart1() => FindFirstMarker(4);
        public override string SolvePart2() => FindFirstMarker(14);

        private string FindFirstMarker(int markerLength) => Enumerable.Range(0, Input[0].Length - markerLength)
            .Aggregate(new List<int>(), (ints, i) =>
                    Input[0][i..(i + markerLength)].Distinct().Count() == markerLength
                        ? ints.Expand(i + markerLength) 
                        : ints).First().ToString();
    }
}