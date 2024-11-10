namespace AoC2016
{
    public class Day09 : Solution
    {
        private Dictionary<int, (int take, int repeat, int length)> markers;
        public Day09(string file) : base(file) => InitializeMarkers();
        public Day09(List<string> input) : base(input) => InitializeMarkers();
        private void InitializeMarkers( ) => markers = Input
                .Select(line => Regex.Matches(line, @"(?<marker>\((?<take>\d+)x(?<repeat>\d+)\))"))
                .SelectMany(m => m.Where(g => g.Groups["marker"].Success))
                .ToDictionary(m => m.Groups["marker"].Index,
                    m => (take: int.Parse(m.Groups["take"].Value), repeat: int.Parse(m.Groups["repeat"].Value),
                        length: m.Length));

        public override async Task<string> SolvePart1( )
        {
            var line = Input[0];
            var idx = 0;
            var count = 0;

            while(idx < line.Length)
            {
                if(line[idx] == '(')
                {
                    count += (markers[idx].take * markers[idx].repeat);
                    idx += markers[idx].length + markers[idx].take;
                }
                else
                {
                    idx++;
                    count++;
                }
            }

            return count.ToString();
        }


        public override async Task<string> SolvePart2( )
        {
            var markerEnds = new Dictionary<int, int>();

            markers.ForEach(kvp =>
            {
                var end = kvp.Key + kvp.Value.length + kvp.Value.take;
                if(!markerEnds.ContainsKey(end))
                    markerEnds.Add(end, kvp.Value.repeat);
                else
                    markerEnds[end] *= kvp.Value.repeat;
            });

            var line = Input[0];
            var idx = 0;
            long count = 0;
            var multiplier = 1;

            while(idx < line.Length)
            {
                if(markerEnds.ContainsKey(idx))
                    multiplier /= markerEnds[idx];

                if(line[idx] == '(')
                {
                    multiplier *= markers[idx].repeat;
                    idx += markers[idx].length;
                }
                else
                {
                    count += multiplier;
                    idx++;
                }
            }

            return count.ToString();
        }
    }
}