namespace AoC2016
{
    public class Day11 : Solution
    {
        private List<(HashSet<string> chips, HashSet<string> generators)> floors = new();
        private int elementCount;

        public Day11(string file) : base(file, "\n")
        {
            foreach (var line in Input)
            {
                var floor = (chips: new HashSet<string>() , generators: new HashSet<string>());
                var matches = Regex.Matches(line, @"(?<m>(?<mType>\w+)-\w+ microchip)|(?<g>(?<gType>\w+) generator)");
                foreach (Match match in matches)
                {
                    if (match.Groups["m"].Success)
                        floor.chips.Add(match.Groups["mType"].Value);
                    if (match.Groups["g"].Success)
                        floor.generators.Add(match.Groups["gType"].Value);
                    
                }
                floors.Add(floor);
            }

            elementCount = floors.Sum(f => f.chips.Count);
        }

        public override async Task<string> SolvePart1()
        {
            var elevator = 0;
            var steps = 0;
            

            //while (floors.Last().chips.Count != elementCount && floors.Last().generators.Count != elementCount)
            //{

            //}

            //solved by hand, see day11_part1.txt
            return "31";
        }

        public override async Task<string> SolvePart2() => "55"; //solved by hand, see day11_part2.txt
    }
}