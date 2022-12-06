namespace AoC2022
{
    public class Day06 : Solution
    {
        public Day06(string input) : base(input) { }
        public Day06(List<string> input) : base(input) { }
        
        public override string SolvePart1() => FindMarker(4);
        public override string SolvePart2() => FindMarker(14);

        private string FindMarker(int markerLength)
        {

            for (var i = 0; i < Input[0].Length; i++)
            {
                if (Input[0].Substring(i, markerLength).Distinct().Count() == markerLength)
                {
                    return (i + markerLength).ToString();
                }
            }

            return string.Empty;
        }
    }
}